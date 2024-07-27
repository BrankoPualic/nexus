namespace Nexus.Common.Resources;

public static class ResourceValidation
{
	public static string Required => "{0} is required.";

	public static string Maximum_Characters_Exceeded => "{0} can't be more than {1} characters long.";

	public static string Minimum_Characters_Not_Reached => "{0} must be at least {1} characters long.";

	public static string Wrong_Format => "{0} is in the wrong format.";

	public static string Dont_Match => "{0} and {1} fields don't match.";

	public static string Already_Exist => "{0} already exist.";

	public static string Password => "Password must consist of at least one uppercase letter, one lowercase letter, one digit, one special character and must be at least 8 characters long.";

	public static string Minimum_Age => "Must be at least {0} years old.";

	public static string Invalid_Credentials => "The Username, Email or Password is incorrect.";

	public static string Invalid => "{0} is invalid.";
}