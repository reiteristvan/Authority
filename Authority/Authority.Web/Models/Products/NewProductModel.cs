﻿using System.ComponentModel.DataAnnotations;

namespace Authority.Web.Models.Products
{
    public sealed class NewProductModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SiteUrl { get; set; }

        [Required]
        public string NotificationEmail { get; set; }

        [Required]
        public string ActivationUrl { get; set; }
    }
}