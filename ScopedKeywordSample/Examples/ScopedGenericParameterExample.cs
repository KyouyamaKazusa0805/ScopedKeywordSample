namespace ScopedKeywordSample.Examples;

/// <summary>
/// 表示泛型参数使用的例子。
/// </summary>
public readonly ref struct ScopedGenericParameterExample : IExample
{
	/// <inheritdoc/>
	public static string Description => "泛型参数使用的例子";


	/// <inheritdoc/>
	public void ShowExample()
	{
		var span = (stackalloc[] { 1, 2, 3, 4 });
		refStructTypeTest(span);
		refStructTypeTest2(span);


		// 报错
		static T refStructTypeTest<T>(scoped T instance) where T : allows ref struct => instance;

		// 照样报错
		static T refStructTypeTest2<T>(scoped T instance) where T : allows ref struct => refStructTypeTestPassed(instance);

		static T refStructTypeTestPassed<T>(T instance) => instance;
	}
}
