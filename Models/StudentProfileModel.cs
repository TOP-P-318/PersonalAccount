namespace ДЗ_на_25_мая_Тимур_Жуков.Models
{
    public class StudentProfileModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
    }
}
