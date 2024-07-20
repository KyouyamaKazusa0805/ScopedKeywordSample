namespace ScopedKeywordSample.Examples;

/// <summary>
/// 表示 <see langword="scoped"/> 关键字用于临时变量上的例子。
/// </summary>
public readonly ref struct ScopedLocalsExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "scoped 用于临时变量的例子";


	/// <inheritdoc/>
	public void ShowExample()
	{
		returnSpan<int>();
		returnSpanScoped<int>();
		usingScopedLocals().Wait();
		forEachScopedLocals();


		static Span<T> returnSpan<T>()
		{
			scoped var local = new Span<T>();

			// 因为 returnSpan2 函数的参数允许返回参数（return），所以返回临时变量的行为会在这个方法检测到
			return returnSpanPassed(local);
		}

		static Span<T> returnSpanScoped<T>()
		{
			scoped var local = new Span<T>();

			// 因为 returnSpan3 函数有 scoped 修饰，他暗示的是返回值跟参数无关（参数不可用于返回），所以这里就允许了
			return returnSpanPassedScoped(local);
		}

		static Span<T> returnSpanPassed<T>(Span<T> span) => span;

		static Span<T> returnSpanPassedScoped<T>(scoped Span<T> span) => span; // 相反地，这里会产生报错

		static async Task usingScopedLocals()
		{
			using scoped var disposable1 = new DisposableRefStruct(); // 注意 scoped 必须放 using 后面
			await using scoped var disposable2 = new DisposableRefStruct();

			var span = (stackalloc[] { 1, 2, 3 });
			scoped ref var ref1 = ref span[0];
			scoped ref readonly var ref2 = ref span[1];
		}

		static void forEachScopedLocals()
		{
			foreach (scoped ref readonly var elementRef in new DisposableRefStruct())
			{
				Console.WriteLine(elementRef);
			}
		}
	}
}

file readonly ref struct DisposableRefStruct : IAsyncDisposable, IDisposable
{
	public void Dispose() { }

	public ValueTask DisposeAsync() => default;

	public Enumerator GetEnumerator() => new([]);
}

public ref struct Enumerator(int[] array) : IEnumerator<int>
{
	private Span<int>.Enumerator _enumerator = array.AsSpan().GetEnumerator();


	public readonly ref readonly int Current => ref _enumerator.Current;

	readonly int IEnumerator<int>.Current => Current;

	readonly object IEnumerator.Current => Current;


	public readonly void Dispose() { }

	public bool MoveNext() => _enumerator.MoveNext();

	public void Reset() => throw new NotImplementedException();
}
