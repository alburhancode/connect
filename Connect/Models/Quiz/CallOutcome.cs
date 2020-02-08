using System.ComponentModel.DataAnnotations;

namespace Connect.Models.Quiz
{
    public enum CallOutcome
    {
        None,
        Attended,
        Busy,
        Off,
        Wrong
    }

    public enum CallerKefiyat
    {
        None,
        [Display(Name = "Not Continue")]
        NotContinue,
        Regular,
        Irregular,
        [Display(Name = "Will join next week")]
        WillJoinNextWeek
    }
}