namespace ScopedKeywordSample.Examples;

/// <summary>
/// 错误传递引用以拓展引用的生命周期的例子。
/// </summary>
public readonly ref struct ExtendLocalLifetimeByRefReturningExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "直接传递拓展生命周期";


	/// <inheritdoc/>
	public void ShowExample()
	{
		sample();


		static ref int sample()
		{
			var local = 42;

			// 临时变量具有临时变量的生命周期，而 returnDirect 函数直接返回结果，所以等于临时变量直接返回
			// 生命周期：临时变量（小）-> 返回（大）
			return returnDirect(ref local);
		}
		static ref int returnDirect(ref int value) => ref value;
	}
}
