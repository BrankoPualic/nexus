using Humanizer;
using System.Text.RegularExpressions;

namespace Nexus.Common.Extensions;

public static class StringExtensions
{
	public static string FormatString(this string format, params string[] args)
		=> string.Format(format, args.Select(_ => _.Humanize(LetterCasing.Title)).ToArray());

	public static bool HasValue(this string value) => value.IsNotNullOrEmpty() && value.IsNotNullOrWhiteSpace();

	public static bool In(this string value, params string[] args) => args.Any(value.Equals);

	public static bool IsValidFormat(this string value, Regex regex) => Regex.IsMatch(value, regex.ToString());
}