using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Core.ViewModels
{
    public class DataTablesRequest
    {
        public int draw { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 20;
    }
}
