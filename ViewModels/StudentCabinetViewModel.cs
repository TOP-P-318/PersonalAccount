namespace ДЗ_на_25_мая_Тимур_Жуков.ViewModels;

public class StudentCabinetViewModel
{
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public bool IsEmailConfirmed { get; set; }
}