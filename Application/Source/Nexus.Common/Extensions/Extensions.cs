namespace Nexus.Common.Extensions;

public static class Extensions
{
	// Is null or empty
	public static bool IsNullOrEmpty(this string? value) => string.IsNullOrEmpty(value);

	public static bool IsNullOrEmpty<T>(this T? model) where T : class => model == null;

	public static bool IsNullOrEmpty<T>(this IEnumerable<T>? model) => model == null || !model.Any();

	// Is not null or empty

	public static bool IsNotNullOrEmpty(this string? value) => !string.IsNullOrEmpty(value);

	public static bool IsNotNullOrEmpty<T>(this T? model) where T : class => !IsNullOrEmpty(model);

	public static bool IsNotNullOrEmpty<T>(this IEnumerable<T>? model) => !IsNullOrEmpty(model);

	// Is null or white space

	public static bool IsNullOrWhiteSpace(this string? value) => string.IsNullOrWhiteSpace(value);

	// Is not null or white space

	public static bool IsNotNullOrWhiteSpace(this string? value) => !string.IsNullOrWhiteSpace(value);
}