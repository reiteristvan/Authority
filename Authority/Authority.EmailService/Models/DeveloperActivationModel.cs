﻿namespace Authority.EmailService.Models
{
    [EmailTemplateName("DeveloperActivation")]
    public sealed class DeveloperActivationModel
    {
        public string ActivationUrl { get; set; }
    }
}
