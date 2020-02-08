using System.ComponentModel.DataAnnotations;

namespace Connect.Classes.Enums
{
    public enum MaritalStatus
    {
        Single,
        Married
    }

    public enum SectionsForMales
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K
    }

    public enum SectionsForFemales
    {
        A,
        B
    }

    public enum GradingAuthority
    {
        Ceo,
        Panel
    }

    public enum AdmissionGrade
    {
        [Display(Name = "None")]
        None,
        C,
        B,
        [Display(Name = "B+")]
        BPlus,
        [Display(Name = "A-")]
        AMinus,
        A,
        [Display(Name = "A+")]
        APlus,
        [Display(Name = "Listener")]
        L,
        [Display(Name = "Not Graded")]
        NG
    }
}