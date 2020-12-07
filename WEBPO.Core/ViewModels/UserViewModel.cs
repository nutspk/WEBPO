using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WEBPO.Core.Persistances;
using WEBPO.Domain.Entities;

namespace WEBPO.Core.ViewModels
{
    public class UserViewModel {
        // criteria
        [DisplayName("Vendor Code")]
        public string VendorCode { get; set; }

        [DisplayName("User ID")]
        public string UserId { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        public SelectList VendorList { get; set; }
        public SelectList UserTypeList { get; set; }
        public List<UserCreateViewModel> Users { get; set; } = new List<UserCreateViewModel>(); 
    }


    public class UserCreateViewModel
    {
        [DisplayName("User ID")]
        [Required(ErrorMessage = "Input User ID")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "User ID should be between 1 and 10 characters")]
        public string UserID { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "Input User Name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "User Name should be between 1 and 100 characters")]
        public string UserName { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Input Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DisplayName("Vendor Code")]
        [Required(ErrorMessage = "Input Vendor Code")]
        public string VendorCode { get; set; }

        [DisplayName("Vendor")]
        public MS_VS Vendor { get; set; } = new MS_VS();

        [DisplayName("Send Mail")]
        public string SendMailFlag { get; set; }

        [DisplayName("User Type")]
        [Required(ErrorMessage = "Input User Type")]
        public string UserType { get; set; }

        public IEnumerable<SelectListItem> VendorList { get; set; }
        public IEnumerable<SelectListItem> UserTypeList { get; set; }

        [DisplayName("Section Code")]
        [JsonIgnore]
        public string SectionCode { get; set; }

        [DisplayName("Language")]
        [JsonIgnore]
        public string Language { get; set; }

        [DisplayName("Reset Pin")]
        [JsonIgnore]
        public string ResetPin { get; set; }


    }
}
