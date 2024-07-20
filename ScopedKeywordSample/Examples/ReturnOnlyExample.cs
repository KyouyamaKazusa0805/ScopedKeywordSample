namespace ScopedKeywordSample.Examples;

/// <summary>
/// 介绍 return-only 的使用。
/// </summary>
public readonly ref struct ReturnOnlyExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "return-only 使用";


	/// <inheritdoc/>
	public void ShowExample()
	{
		var span = (stackalloc int[10]);
		var refValueArray = new RefValueArray<int>(in span[0], 10);
		ref readonly var reference = ref refValueArray[3];
		Console.WriteLine(reference);
	}
}

file readonly ref struct RefValueArray<T>(ref readonly T firstRef, int length)
{
	private readonly ref readonly T _firstRef = ref firstRef;

	public int Length { get; } = length;

	// 字段引用返回字段引用，属于 return-only 的传递规则（引用 -> 引用）
	public ref readonly T this[int index] => ref _firstRef;
}
