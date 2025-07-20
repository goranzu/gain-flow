namespace GainFlow.Api.Features.Authentication.Register;

public sealed record RegisterCommand(string Email, string Password, string ConfirmPassword);
