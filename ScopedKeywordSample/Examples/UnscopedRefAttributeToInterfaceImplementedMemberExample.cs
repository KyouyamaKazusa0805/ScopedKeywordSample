namespace ScopedKeywordSample.Examples;

/// <summary>
/// 表示 <see cref="UnscopedRefAttribute"/> 特性从接口和派生类型里同时使用的例子。
/// </summary>
public readonly ref struct UnscopedRefAttributeToInterfaceImplementedMemberExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "基接口和派生类型 UnscopedRefAttribute 保持一致的例子";


	/// <inheritdoc/>
	public void ShowExample()
	{
		var inlineArray = new InlineArray<char>();
		ref readonly var firstRef = ref inlineArray.GetPinnableReference();
		Console.WriteLine(firstRef.ToString());
	}
}

[InlineArray(10)]
file struct InlineArray<T> : IGetPinnableReference<InlineArray<T>, T>
{
	private T _field;


	// 这里标记的 UnscopedRefAttribute 必须和实现的基接口类型标记保持一致：基接口里也得有 UnscopedRefAttribute 才行。
	[UnscopedRef]
	public readonly ref readonly T GetPinnableReference() => ref this[0];
}
