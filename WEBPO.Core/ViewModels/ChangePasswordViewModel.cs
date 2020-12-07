using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WEBPO.Core.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }

        [DisplayName("Current Password")]
        [Required(ErrorMessage = "Please input current password")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please input New Password")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Confirm password doesn't match, Type again !")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ConfirmPassword { get; set; }

        [DisplayName("Pin Code")]
        public string PinCode { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
