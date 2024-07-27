namespace Nexus.Common.Extensions;

public static class DateExtensions
{
	public static bool IsEqualOrOlderThan(this DateTime date, int age) => date <= DateTime.Today.AddYears(-age);

	public static bool IsEqualOrOlderThan(this DateOnly date, int age)
		=> date.ToDateTime(TimeOnly.MinValue).IsEqualOrOlderThan(age);
}