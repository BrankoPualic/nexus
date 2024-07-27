using System.Linq.Expressions;

namespace Nexus.Infrastructure.Data.Extensions;

public static class QueryableExtensions
{
	public static IQueryable<TModel> IncludeProperties<TModel>(this IQueryable<TModel> query, params Expression<Func<TModel, object>>[] includeProperties) where TModel : class
		=> query = includeProperties.Aggregate(query, (current, property) => current.IncludeConvert(property));

	public static IQueryable<TModel> IncludeConvert<TModel>(this IQueryable<TModel> query, Expression<Func<TModel, object>> includeProperty) where TModel : class
		=> TryParsePath(includeProperty.Body, out var path)
			? query.Include(path)
			: throw new ArgumentException($"Invalid include parameter: {includeProperty}");

	public static bool TryParsePath(Expression expression, out string path)
	{
		path = null;
		var withoutConvert = expression.RemoveConvert();

		if (withoutConvert is MemberExpression memeberExpression)
		{
			var thisPart = memeberExpression.Member.Name;
			//if (!TryParsePath(memeberExpression.Expression, out var parentPart))
			//	return false;
			TryParsePath(memeberExpression.Expression, out var parentPart);
			path = parentPart == null ? thisPart : $"{parentPart}.{thisPart}";
			return true;
		}
		else if (withoutConvert is MethodCallExpression callExpression)
		{
			if (callExpression.Method.Name == "Select" && callExpression.Arguments.Count == 2)
			{
				if (!TryParsePath(callExpression.Arguments[0], out var parentPart))
					return false;

				if (parentPart != null)
					if (callExpression.Arguments[1] is LambdaExpression subExpression)
					{
						if (!TryParsePath(subExpression.Body, out var thisPart))
							return false;
						if (thisPart != null)
						{
							path = $"{parentPart}.{thisPart}";
							return true;
						}
					}
			}
			return false;
		}
		return false;
	}

	public static Expression RemoveConvert(this Expression expression)
	{
		while (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked)
			expression = ((UnaryExpression)expression).Operand;
		return expression;
	}
}