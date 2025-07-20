namespace GainFlow.Api.Infrastructure.Data.Entities;

public sealed class User : AuditableEntity
{
    public string Id { get; set; }
    public string Email { get; set; }
    public float? Weight { get; set; }
    public float? Height { get; set; }

    public string IdentityId { get; set; }
}
