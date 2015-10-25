﻿using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Web.Models
{
    public sealed class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}