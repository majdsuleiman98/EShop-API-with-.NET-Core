﻿
namespace Core.Specifications.SpecParams
{
    public abstract class BaseSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        public int _pageSize { get; set; } = 5;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        public string? Sort { get; set; }
        private string? _search;
        public string Search
        {
            get => _search ?? "";
            set => _search = value?.ToLower(); 
        }
    }
}
