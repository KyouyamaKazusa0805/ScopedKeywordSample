namespace ScopedKeywordSample.Examples;

/// <summary>
/// 表示 <see cref="UnscopedRefAttribute"/> 特性用于方法、索引器等类方法的成员上的例子。
/// </summary>
public readonly ref struct UnscopedRefAttributeToMethodLikeMemberExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "UnscopedRefAttribute 用于类方法成员上的例子";


	/// <inheritdoc/>
	public void ShowExample()
	{
		var singleValueArray = new SingleValueArray<int>(10);
		ref readonly var reference2 = ref singleValueArray[0];
		Console.WriteLine(reference2);

		var inlineArray = new InlineArray();
		ref readonly var charRef = ref inlineArray.GetCharacterReferenceAt(3, false);
		Console.WriteLine(charRef);
	}
}

// 方法
[InlineArray(10)]
file struct InlineArray
{
	private char _firstChar;


	public InlineArray()
	{
		const string testString = "Hello,you!";
		for (var i = 0; i < 10; i++)
		{
			this[i] = testString[i];
		}
	}


	[UnscopedRef]
	public readonly ref readonly char GetCharacterReferenceAt(int index, bool isFromEnd)
		=> ref isFromEnd ? ref this[index] : ref this[10 - index];

	[UnscopedRef]
	public readonly ref readonly char GetPinnableReference() => ref this[0];
}

// 索引器
file readonly ref struct SingleValueArray<T>(T first)
{
	private readonly T _first = first;

	public int Length { get; } = 1;

	// 字段非引用但需要返回他的引用，需要 UnscopedRefAttribute
	// 因为这里用的 _first 相当于 this._first，会把 this 的实例也带出去
	[UnscopedRef]
	public ref readonly T this[int index] => ref _first;
}
