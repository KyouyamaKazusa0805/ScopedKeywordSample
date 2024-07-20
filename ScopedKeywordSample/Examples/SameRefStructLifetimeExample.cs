namespace ScopedKeywordSample.Examples;

/// <summary>
/// 使用 <see langword="ref struct"/> 生命周期的例子。
/// </summary>
public readonly ref struct SameRefStructLifetimeExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "使用 ref struct 返回对象的例子";


	/// <inheritdoc/>
	public void ShowExample()
	{
		var span = (stackalloc[] { 1, 2, 3 });
		returnSpan(span);
		returnSpanScoped(span);


		// 允许
		static Span<int> returnSpan(Span<int> span) => span;

		// 也是允许的
		static Span<int> returnSpanScoped(Span<int> span)
		{
			return returnSpanScopedPassed(span);


			static Span<int> returnSpanScopedPassed(Span<int> span) => span;
		}
	}
}
