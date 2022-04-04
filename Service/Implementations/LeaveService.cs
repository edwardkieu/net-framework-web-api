using AutoMapper;
using Common.Enums;
using Data.Interfaces;
using Domain.Entities;
using Service.Interfaces;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class LeaveService : ILeaveService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public LeaveService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AllLocateLeaveByDepartment(int departmentId, int period)
        {
            // get all users by request leave
            var employees = await _unitOfWork.AppUserRepository.GetUserByDepartmentIdAsync(departmentId);
            var leaveTypes = await _unitOfWork.LeaveTypeRepository.GetAllQuery(true).OrderBy(x => x.Name).ToListAsync();
            var employeeIds = employees.Select(e => e.Id).ToList();
            var leaveLocations = await _unitOfWork
                .LeaveAllocationRepository
                .GetAllQuery(true)
                .Where(x => x.EmployeeId != null && employeeIds.Contains(x.EmployeeId))
                .ToListAsync();

            var listLeaveAllocation = new List<LeaveAllocation>();

            foreach (var leaveType in leaveTypes)
            {
                foreach (var employee in employees)
                {
                    if (leaveLocations.Any(x => x.EmployeeId == employee.Id && x.Period == period && x.LeaveTypeId == leaveType.Id))
                        continue;
                    // set
                    listLeaveAllocation.Add(new LeaveAllocation
                    {
                        EmployeeId = employee.Id,
                        Period = period,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays
                    });
                }
            }

            if (listLeaveAllocation.Any())
            {
                _unitOfWork.LeaveAllocationRepository.AddRange(listLeaveAllocation);
                return _unitOfWork.SaveChanges() > 0;
            }

            return false;
        }

        public async Task<bool> ChangeStatusAsync(int id, RequestLeaveStatus status)
        {
            var requestLeave = await _unitOfWork.RequestLeaveRepository.FindByIdAsync(id);
            if (requestLeave == null)
                return false;

            requestLeave.Status = status;
            if (status == RequestLeaveStatus.Approved)
            {
                var allocation = await _unitOfWork
                    .LeaveAllocationRepository
                    .GetSingle(q => q.EmployeeId == requestLeave.RequestedId && q.Period == DateTime.Now.Year && q.LeaveTypeId == requestLeave.LeaveTypeId);

                if (allocation == null)
                {
                    throw new Exception("You Have No Days Left");
                }

                int daysRequested = (int)(requestLeave.EndDate - requestLeave.StartDate).TotalDays;
                allocation.NumberOfDays -= daysRequested;
            }

            _unitOfWork.RequestLeaveRepository.Update(requestLeave);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<RequestLeaveViewModel> CreateRequestLeaveAsync(CreateRequestLeaveViewModel vm)
        {
            var startDate = Convert.ToDateTime(vm.StartDate);
            var endDate = Convert.ToDateTime(vm.EndDate);
            var period = DateTime.Now.Year;
            var allocation = await _unitOfWork.LeaveAllocationRepository.GetSingle(q => q.EmployeeId == vm.RequestedId
                                                    && q.Period == period
                                                    && q.LeaveTypeId == vm.LeaveTypeId);
            int daysRequested = (int)(endDate - startDate).TotalDays;

            if (allocation == null)
            {
                throw new Exception("You Have No Days Left");
            }

            if (DateTime.Compare(startDate, endDate) > 1)
            {
                throw new Exception("Start Date cannot be further in the future than the End Date");
            }

            if (daysRequested > allocation.NumberOfDays)
            {
                throw new Exception("You Do Not Sufficient Days For This Request");
            }

            var requestLeave = new RequestLeave
            {
                RequestedId = vm.RequestedId,
                StartDate = startDate,
                EndDate = endDate,
                LeaveTypeId = vm.LeaveTypeId,
                ApprovedId = vm.ApprovedId,
                Comments = new List<RequestLeaveComment> { new RequestLeaveComment { Comment = vm.Comment } },
                Status = RequestLeaveStatus.Pending
            };

            _unitOfWork.RequestLeaveRepository.Add(requestLeave);
            _unitOfWork.SaveChanges();

            return _mapper.Map<RequestLeaveViewModel>(requestLeave);
        }

        public async Task<IEnumerable<RequestLeaveViewModel>> GetByDepartmentIdAsync(int departmentId)
        {
            // Sub query on memory
            //var users = await _unitOfWork.AppUserRepository.GetUserByDepartmentIdAsync(departmentId);
            //var userIds = users.Select(u => u.Id).ToList();
            //var requestLeaves = await _unitOfWork
            //    .RequestLeaveRepository
            //    .GetAllQuery(true, x => x.Comments)
            //    .Where(x => x.RequestedId != null && userIds.Contains(x.RequestedId))
            //    .ToListAsync();

            //combine in one query
            var requestLeaves = await (from u in _unitOfWork.AppUserRepository.GetQuery().Where(x => x.Departments.Any(d => d.Id == departmentId))
                                       join r in _unitOfWork.RequestLeaveRepository.GetAllQuery(false, x => x.Comments) on u.Id equals r.RequestedId
                                       select new RequestLeaveViewModel
                                       {
                                           Id = r.Id,
                                           StartDate = r.StartDate,
                                           EndDate = r.EndDate,
                                           LeaveTypeId = r.LeaveTypeId,
                                           ApprovedId = r.ApprovedId,
                                           Comment = r.Comments.FirstOrDefault().Comment ?? string.Empty,
                                           Status = r.Status,
                                           RequestedId = r.RequestedId
                                       })
                                       .Distinct()
                                       .AsNoTracking()
                                       .ToListAsync();

            return requestLeaves; //_mapper.Map<IEnumerable<RequestLeaveViewModel>>(requestLeaves);
        }

        public async Task<RequestLeaveViewModel> GetByIdAsync(int id)
        {
            var requestLeave = await _unitOfWork.RequestLeaveRepository.GetAllQuery(true, x => x.Comments).FirstOrDefaultAsync();
            if (requestLeave == null)
                throw new Exception($"Id {id} not found.");

            return _mapper.Map<RequestLeaveViewModel>(requestLeave);
        }
    }
}