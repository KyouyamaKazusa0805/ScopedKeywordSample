namespace ScopedKeywordSample.Examples;

/// <summary>
/// 表示 <see cref="UnscopedRefAttribute"/> 特性用于参数上的例子。
/// </summary>
public readonly ref struct UnscopedRefAttributeToParameterExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "UnscopedRefAttribute 特性用于参数上的例子";


	/// <inheritdoc/>
	public void ShowExample()
	{
		var v = 42;
		var spanSample = new ReadOnlySpan<int>(in v);
		returnEnumerator();
		returnEnumeratorRefReadOnly(ref v);
		returnEnumeratorSpanUnscoped(spanSample);
		returnEnumeratorParamsUnscoped(spanSample);


		static void returnEnumerator() => _ = new SpanEnumerator<int>(1, 2, 3);

		static SpanEnumerator<T> returnEnumeratorRefReadOnly<T>(ref readonly T r) => new(new(in r));

		static SpanEnumerator<T> returnEnumeratorSpanUnscoped<T>(ReadOnlySpan<T> span) => new(span);

		static SpanEnumerator<T> returnEnumeratorParamsUnscoped<T>([UnscopedRef] params ReadOnlySpan<T> span) => new(span);
	}
}

file ref struct SpanEnumerator<T>([UnscopedRef] params ReadOnlySpan<T> span) : IEnumerator<T>
{
	private ReadOnlySpan<T>.Enumerator _enumerator = span.GetEnumerator();


	public readonly ref readonly T Current => ref _enumerator.Current;

	readonly object? IEnumerator.Current => Current;

	readonly T IEnumerator<T>.Current => Current;


	public bool MoveNext() => _enumerator.MoveNext();

	readonly void IDisposable.Dispose() { }

	readonly void IEnumerator.Reset() => throw new NotSupportedException();
}
