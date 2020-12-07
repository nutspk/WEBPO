using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Core.ViewModels.UI
{
    public class SelectListViewModel
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class Select2ViewModel
    {
        public int Total { get; set; }
        public IEnumerable<Select2Result> Results { get; set; }
    }

    public class Select2Result
    {
        public string ID { get; set; }
        public string Text { get; set; }
    }
}
