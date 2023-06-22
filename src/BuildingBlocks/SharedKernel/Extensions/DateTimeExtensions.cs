namespace Fintranet.BuildingBlocks.Common.SharedKernel.Extensions;

public static class DateTimeExtensions
{
    public static DateTime ToDateTime(this DateOnly date) => new (date.Year, date.Month, date.Day);
    public static DateOnly ToDateOnly(this DateTime dateTime) => new (dateTime.Year, dateTime.Month, dateTime.Day);

    public static Dictionary<int, List<DateTime>> ClusterByDay(this DateTime[] dates)
    {
        int[] days = new int[dates.Length];
        for (int i = 0; i < dates.Length; i++)
        {
            days[i] = dates[i].Day;
        }

        Dictionary<int, List<DateTime>> clusters = new Dictionary<int, List<DateTime>>();

        for (int i = 0; i < dates.Length; i++)
        {
            int day = days[i];

            if (!clusters.ContainsKey(day))
            {
                clusters[day] = new List<DateTime>();
            }

            clusters[day].Add(dates[i]);
        }

        return clusters;
    }
}