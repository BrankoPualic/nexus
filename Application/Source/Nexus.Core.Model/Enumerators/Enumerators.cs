namespace Nexus.Core.Model.Enumerators;

public enum eRole
{
	[Description("Administrator")]
	Admin = 1,

	[Description("User Admin")]
	UserAdmin = 2,

	[Description("Moderator")]
	Moderator = 3,

	[Description("Member")]
	Member = 4,
}

public enum eAction
{
	Create = 1,
	Update = 2,
	Delete = 3,
}