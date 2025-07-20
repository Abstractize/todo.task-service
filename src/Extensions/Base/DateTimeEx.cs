namespace Extensions.Base;

public static class DateTimeEx
{
    public static bool IsOnWeek(this DateTime date, DateTime weekStartDateUtc)
        => date >= weekStartDateUtc && date < weekStartDateUtc.AddDays(7);

    public static bool IsOnWeek(this DateTime? date, DateTime weekStartDateUtc)
    {
        if (date == null)
            return false;

        return date >= weekStartDateUtc && date < weekStartDateUtc.AddDays(7);
    }
}