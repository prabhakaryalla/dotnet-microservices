namespace Shopping.Api.Contracts.Interfaces;

public interface IClientConfiguration
{
    public string PartnerName { get; set; }
    public Guid RequestId { get; set; }
    public string ClientType { get; set; }
}
