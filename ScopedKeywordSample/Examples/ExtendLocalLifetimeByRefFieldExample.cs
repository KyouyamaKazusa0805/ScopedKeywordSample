namespace ScopedKeywordSample.Examples;

/// <summary>
/// 错误传递引用以拓展 <see langword="ref struct"/> 生命周期的例子。
/// </summary>
public readonly ref struct ExtendLocalLifetimeByRefFieldExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "使用引用字段拓展生命周期的例子";


	/// <inheritdoc/>
	public void ShowExample()
	{
		createSpanOfInt();


		static Span<int> createSpanOfInt()
		{
			var local = 42;

			// 报错，因为 Span<int> 会拿 local 的引用，而他会返回出去，所以等于使用范围越来越大，这是不允许的
			// 生命周期：临时变量（小）-> 引用（大）
			return new(ref local);
		}
	}
}
