using Connect.Classes.Enums;

namespace Connect.Classes.DataModels
{
    public interface IGradingLog
    {
        string Email { get; }
        AdmissionGrade Grade { get; }
        string Comments { get; }
    }
}