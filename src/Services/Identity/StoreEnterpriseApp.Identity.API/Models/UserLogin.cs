﻿using System.ComponentModel.DataAnnotations;

namespace StoreEnterpriseApp.Identity.API.Models
{
    public class UserLogin
    {

        [Required(ErrorMessage = "The field {0} it's necessary")]
        [EmailAddress(ErrorMessage = "The Field {0} is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} it's necessary")]
        [StringLength(100, ErrorMessage = "The field {0} needs {2} or {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
