namespace IdentityServer.DomainModel
{
    public sealed class Claim : EntityBase
    {
        public string Issuer { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}
