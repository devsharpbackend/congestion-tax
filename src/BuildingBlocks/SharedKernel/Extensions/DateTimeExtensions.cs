namespace Fintranet.BuildingBlocks.Common.SharedKernel.Extensions;

public static class DateTimeExtensions
{
    public static DateTime ToDateTime(this DateOnly date) => new (date.Year, date.Month, date.Day);
    public static DateOnly ToDateOnly(this DateTime dateTime) => new (dateTime.Year, dateTime.Month, dateTime.Day);
}