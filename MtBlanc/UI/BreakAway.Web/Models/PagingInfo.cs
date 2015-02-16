using System;

namespace BreakAway.Models
{
    public class PagingInfo
    {
        public PagingInfo(int currentPageIndex, int totalItems, int pageSize)
        {
            CurrentPageIndex = currentPageIndex;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        }

        public int TotalItems { get; private set; }
        public int CurrentPageIndex { get; private set; }
        public int TotalPages { get; private set; }
    }
}