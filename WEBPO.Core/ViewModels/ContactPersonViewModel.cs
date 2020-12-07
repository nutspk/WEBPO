using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WEBPO.Domain.Entities;

namespace WEBPO.Core.ViewModels
{
    public class ContactPersonViewModel
    {
        [DisplayName("Vendor Code")]
        public string VendorCode { get; set; }

        [DisplayName("Contact Name")]
        public string ContactName { get; set; }

        [DisplayName("Dept")]
        public string DepartmentName { get; set; }
        public SelectList VendorList { get; set; }
        public List<ContactPersonCreateViewModel> Persons { get; set; } = new List<ContactPersonCreateViewModel>();
    }

    public class ContactPersonCreateViewModel
    {
        public string ContactId { get; set; }

        [DisplayName("Contact Name")]
        [Required(ErrorMessage = "Input Contact Name")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "User ID should be between 1 and 200 characters")]
        public string ContactName { get; set; }

        [DisplayName("Department")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Department should be between 1 and 10 characters")]
        public string DepartmentId { get; set; }

        [DisplayName("Department")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Department should be between 1 and 10 characters")]
        public string DepartmentName { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Input Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DisplayName("Vendor Code")]
        [Required(ErrorMessage = "Input Vendor Code")]
        public string VendorCode { get; set; }

        [DisplayName("Vendor")]
        public MS_VS Vendor { get; set; }

        [DisplayName("Send Mail")]
        public string SendMailFlag { get; set; }

        [DisplayName("Tel No")]
        [StringLength(50)]
        public string TelNo { get; set; }

        [DisplayName("Mobile")] 
        [StringLength(50)]
        public string MobileNo { get; set; }
        
        public IEnumerable<SelectListItem> VendorList { get; set; }
    }
}
