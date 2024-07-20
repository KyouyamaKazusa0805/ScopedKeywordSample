namespace ScopedKeywordSample.Examples;

/// <summary>
/// 表示 <see langword="out"/> 参数的使用域的展示例子。
/// </summary>
public readonly ref struct OutParametersExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "out 参数的使用域例子";


	/// <inheritdoc/>
	public void ShowExample()
	{
		passToParamOut(out _);
		passToParamUnscopedOut(out _);
		passToParamUnscopedOut2(out _);
		outParamIsRefStruct<Span<int>>(out var span1); // 可以，和 scoped var 是等价的声明
		outParamIsRefStruct<Span<int>>(out scoped var span2); // 也是可以的


		static ref int passToParamOut(out int value)
		{
			var local = 42;
			value = local; // 可以
			value = ref local; // 也可以，但必须具有初始赋值后才能这么用（此时相当于普通的变量传引用篡改）
			return ref value; // 不可以，不能把 out 变量的引用当返回值
		}

		static ref int passToParamUnscopedOut([UnscopedRef] out int value)
		{
			var local = 42;
			value = local; // 可以
			return ref value; // 这样是可以的，因为 out 参数自带 scoped 效果
		}

		static ref int passToParamUnscopedOut2([UnscopedRef] out int value)
		{
			var local = 42;
			value = local;

			value = ref local; // 不可以了，因为临时变量不能把引用传出去
			return ref value; // 可以，因为他不再是隐式带有 scoped 修饰的了（因为指向改了）。
		}

		static void outParamIsRefStruct<T>(out T result) where T : struct, allows ref struct => result = default;
	}
}
