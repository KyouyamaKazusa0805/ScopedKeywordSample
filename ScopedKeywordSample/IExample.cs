namespace ScopedKeywordSample;

/// <summary>
/// 提供所有例子的抽象。
/// </summary>
public interface IExample
{
	/// <summary>
	/// 表示例子的描述信息。
	/// </summary>
	public static abstract string Description { get; }

	/// <summary>
	/// 参考链接。
	/// </summary>
	public static virtual string? Link => null;


	/// <summary>
	/// 为当前例子展示测试用例和编译器警告信息。
	/// </summary>
	public abstract void ShowExample();
}
