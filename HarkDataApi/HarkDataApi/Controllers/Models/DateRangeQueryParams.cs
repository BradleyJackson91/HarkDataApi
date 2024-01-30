﻿namespace HarkDataApi.Controllers.Models
{
    public class DateRangeQueryParams
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
