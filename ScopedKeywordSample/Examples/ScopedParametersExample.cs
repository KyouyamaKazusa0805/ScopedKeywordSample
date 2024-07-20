namespace ScopedKeywordSample.Examples;

/// <summary>
/// 表示 <see langword="scoped"/> 关键字用于参数上的例子。
/// </summary>
public readonly ref struct ScopedParametersExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "scoped 用于参数的例子";


	/// <inheritdoc/>
	public void ShowExample()
	{
		anyNonRefStructInstance(0);
		anyInstance(0);
		anyInstance(stackalloc[] { 0 });
		anyInstanceScoped(stackalloc[] { 0 });
		directScopedKeywordScoped([0]);
		directScopedKeywordParamsScoped(0, 0, 0);
		directScopedKeywordUnscoped([0]);
		spanBufferTest();
		refBufferTest();


		// 直接返回，为默认允许的情况
		static T anyNonRefStructInstance<T>(T value) => value;

		// 不带 scoped，可以返回
		static T anyInstance<T>(T value) where T : allows ref struct => value;

		// 带 scoped 不能直接返回
		static T anyInstanceScoped<T>(scoped T value) where T : allows ref struct => value;

		// 带 scoped 不能直接返回
		static ReadOnlySpan<T> directScopedKeywordScoped<T>(scoped ReadOnlySpan<T> values) => values;

		// params 关键字对于 ref struct 类型的对象来说，自带 scoped 效果
		static ReadOnlySpan<T> directScopedKeywordParamsScoped<T>(params ReadOnlySpan<T> values) => values;

		// 不用 params 和 scoped 修饰了，这个时候参数就允许返回了
		static ReadOnlySpan<T> directScopedKeywordUnscoped<T>(ReadOnlySpan<T> values) => values;

		static void spanBufferTest()
		{
			var instance = new SpanBuffer<char>();

			var stackMemoryValuesToCopy = (stackalloc char[10]);
			var stackMemoryValuesToCopyScoped = (stackalloc char[10]);
			var arrayValuesToCopy = new char[10];
			instance.Append(stackMemoryValuesToCopy); // stackalloc 不允许
			instance.Append(arrayValuesToCopy); // new 允许
			instance.AppendScoped(stackMemoryValuesToCopy); // stackalloc 允许
			instance.AppendScoped(arrayValuesToCopy); // new 允许
		}

		static void refBufferTest()
		{
			const int length = 10;
			var charSpan = (stackalloc char[length]);
			var instance = new RefBuffer<char>(ref charSpan[0]);

			var stackMemoryValuesToCopy = (stackalloc char[length]);
			var stackMemoryValuesToCopyScoped = (stackalloc char[length]);
			var arrayValuesToCopy = new char[length];
			instance.Append(ref stackMemoryValuesToCopy[0], length); // stackalloc 允许
			instance.Append(ref arrayValuesToCopy[0], length); // new 允许
			instance.AppendScoped(ref stackMemoryValuesToCopy[0], length); // stackalloc 允许
			instance.AppendScoped(ref arrayValuesToCopy[0], length); // new 允许
		}
	}
}

file ref struct SpanBuffer<T>(int length)
{
	private Span<T> _buffer = new T[length];

	private int _pos = 0;


	public void Append(ReadOnlySpan<T> value) // 拷贝函数
	{
		if (value.TryCopyTo(_buffer[_pos..]))
		{
			_pos += value.Length;
		}
	}

	public void AppendScoped(scoped ReadOnlySpan<T> value) // 拷贝函数，带 scoped 修饰符
	{
		if (value.TryCopyTo(_buffer[_pos..]))
		{
			_pos += value.Length;
		}
	}
}

file unsafe ref struct RefBuffer<T>(ref T reference)
{
	private ref T _buffer = ref reference;

	private int _pos = 0;


	public void Append(ref T value, int length) // 拷贝函数
	{
		var buffer = new Span<T>(Unsafe.AsPointer(ref _buffer), length);
		var source = new Span<T>(Unsafe.AsPointer(ref value), length);
		if (source.TryCopyTo(buffer[_pos..]))
		{
			_pos += source.Length;
		}
	}

	public void AppendScoped(scoped ref T value, int length) // 拷贝函数，带 scoped 修饰符
	{
		var buffer = new Span<T>(Unsafe.AsPointer(ref _buffer), length);
		var source = new Span<T>(Unsafe.AsPointer(ref value), length);
		if (source.TryCopyTo(buffer[_pos..]))
		{
			_pos += source.Length;
		}
	}
}
