using System;

namespace TheatreUZ
{
    public class PageInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages
        {
            get
            {
                return PageSize == 0 ? 1 : (int)Math.Ceiling((decimal)TotalItems / PageSize);
            }
        }
    }
}