namespace ScopedKeywordSample.Examples;

/// <summary>
/// 表示生命周期的一些例子。
/// </summary>
public readonly ref struct MiscellaneousLifetimeExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "生命周期的一些使用例子";


	/// <inheritdoc/>
	public void ShowExample()
	{
		var v = 42;
		passToParam(ref v);
		returnLocal();
		returnRef(ref v);
		returnSpanElementRef(new(ref v));

		var span1 = (stackalloc[] { 1, 2, 3 });
		var span2 = (stackalloc[] { 4, 5, 6 });
		sameParam(ref span1, ref span2);
		paramInternalValueReturn(span1);
		sameParameterTypeSample(ref span2);

		var localRefStruct = new LocalRefStruct();
		localRefStructSample(ref localRefStruct);


		static void passToParam(ref int value)
		{
			var local = 42;
			value = ref local; // 报错，因为 value 是临时变量，会拿到临时变量
		}

		static ref int returnLocal()
		{
			var local = 42;
			return ref local; // 报错，因为 local 是临时变量，出了函数就无法访问了，这里会导致不安全
		}

		static ref int returnRef(ref int value) => ref value; // 直接返回参数的引用（return-only 返回）

		static ref int returnSpanElementRef(Span<int> value) => ref value[0]; // 返回参数内部的引用

		static void sameParam(ref Span<int> a, ref Span<int> b)
		{
			// 这些写法此时都是没问题的，因为 a 和 b 都是走同一个函数进入，所以他俩的生命周期在这个函数执行的时候是一致的
			a = ref b;
			b = ref a;
			a = b;
			b = a;

			// 就算是内部元素也是一样
			a[0] = b[0];
			b[0] = a[0];
		}

		// 这个函数也是可以的，因为返回值是调用方传来的，返回值也是传回调用方，所以来回的两个函数是没有变动的，所以不会报错
		static ref int paramInternalValueReturn(Span<int> span) => ref span[0];

		static void sameParameterTypeSample(ref Span<int> argument)
		{
			var local = (stackalloc[] { 1, 2, 3 });
			fakeSwap(ref argument, ref local); // 引发错误，可以通过 swapSpan 把 anotherSwapSpan 赋出去。


			static void fakeSwap(ref Span<int> a, ref Span<int> b) => a = ref b;
		}

		static void localRefStructSample(ref LocalRefStruct s)
		{
			var span = (stackalloc int[1]);

			// 这会将此方法内定义的局部变量 span 被 S 的实例引用。
			// 于是，当此方法执行完成并出栈后，方法内的局部变量仍然被引用。
			s.SetSpan(span); // 引发错误，可以把栈内存分配的对象传出。
		}
	}
}

file ref struct LocalRefStruct
{
	public Span<int> Span;


	public void SetSpan(Span<int> span) => Span = span;
}
