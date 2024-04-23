namespace RCRP.Common.Helpers;

public class DateTimeHelpers
{
    public static (DateTime From, DateTime To) BuildMonthRange(int month, int year)
    {
        var from = new DateTime(year, month, 1);
        var to = from.AddMonths(1).AddSeconds(-1);

        return (from, to);
    }
}
