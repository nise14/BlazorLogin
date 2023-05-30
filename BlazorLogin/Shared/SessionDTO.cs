namespace BlazorLogin.Shared;

public class SessionDTO
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}