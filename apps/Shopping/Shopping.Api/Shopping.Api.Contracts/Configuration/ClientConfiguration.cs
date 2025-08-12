using Shopping.Api.Contracts.Interfaces;

namespace Shopping.Api.Contracts.Configuration;

public class ClientConfiguration : IClientConfiguration
{
    public string PartnerName { get; set; }
    public Guid RequestId { get; set; }
    public string ClientType { get; set; }
}
