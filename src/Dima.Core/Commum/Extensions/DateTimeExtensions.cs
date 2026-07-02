namespace Dima.Core.Commum.Extensions;

public static class DateTimeExtensions
{
    public static DateTime GetFirstDayOfMonth(this DateTime date, int? month = null, int? year = null)
        => new (year ?? date.Year,month ?? date.Month, 1);
    
    public static DateTime GetLastDayOfMonth(this DateTime date, int? month = null, int? year = null)
        => new DateTime(year ?? date.Year,month ?? date.Month, 1)
            .AddMonths(1)
            .AddDays(-1);
}