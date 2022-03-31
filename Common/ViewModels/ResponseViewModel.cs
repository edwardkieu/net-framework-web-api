using System;
using System.Collections.Generic;

namespace Common.ViewModels
{
    public class ResponseViewModel<T>
    {
        public ResponseViewModel()
        {
        }

        public ResponseViewModel(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public ResponseViewModel(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public T Data { get; set; }
    }

    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => (PageNumber > 1);
        public bool HasNextPage => (PageNumber < TotalPages);
    }

    public class PagedResponseViewModel<T> : ResponseViewModel<T>
    {
        public Pagination Pagination { get; set; }

        public PagedResponseViewModel(T data, int pageNumber, int pageSize)
        {
            Pagination = new Pagination
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            Data = data;
            Message = null;
            Succeeded = true;
            Errors = null;
        }

        public PagedResponseViewModel(T data, int totalCount, int pageNumber, int pageSize)
        {
            Pagination = new Pagination
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            Data = data;
            Message = null;
            Succeeded = true;
            Errors = null;
        }
    }
}