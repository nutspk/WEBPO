using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using WEBPO.Domain.Entities;

namespace WEBPO.Core.ViewModels
{
    class VendorViewModel
    {
        public SelectList VendorList { get; set; }

        public IEnumerable<MS_VS> Vendors { get; set; }

    }
}
