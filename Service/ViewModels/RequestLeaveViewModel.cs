using Common.Enums;

namespace Service.ViewModels
{
    public class RequestLeaveViewModel : CreateRequestLeaveViewModel
    {
        public int Id { set; get; }
        public RequestLeaveStatus Status { get; set; }
    }
}