namespace ScopedKeywordSample.Examples;

/// <summary>
/// 表示 <see langword="scoped"/> 关键字的一些其他地方的特殊用法。
/// </summary>
public readonly ref struct SpecialScopedKeywordExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "scoped 关键字其他地方的特殊用法";

	/// <inheritdoc/>
	public static string Link => "https://github.com/dotnet/csharplang/discussions/8135";


	/// <inheritdoc/>
	public void ShowExample()
	{
		var span = (stackalloc[] { 1, 2, 3 });
		ref var e1 = ref span[0];
		ref var e2 = ref span[1];
		ref readonly var e3 = ref span[^1];
		ref var e4 = ref Unsafe.AsRef(in e3); // scoped ref readonly T
	}
}
