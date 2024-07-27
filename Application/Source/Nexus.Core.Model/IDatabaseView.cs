namespace Nexus.Core.Model;

public interface IDatabaseView
{
	string Script { get; }

	string DropScript { get; }
}