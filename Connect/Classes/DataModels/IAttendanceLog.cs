using System;

namespace Connect.Classes.DataModels
{
    public interface IAttendanceLog
    {
        string Course { get; }
        string Batch { get; }
        string Section { get; }
        string Semester { get; }
        string Subject { get; }
        string Email { get; }
        string CreatedAt { get; }
        string RollNumber { get; }
        string Status { get; }
    }
}