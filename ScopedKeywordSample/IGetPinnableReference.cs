namespace ScopedKeywordSample;

/// <summary>
/// 提供一个临时使用的接口类型，类型演示的是在没有实例的情况下，提前 <see cref="UnscopedRefAttribute"/> 标记的情况。
/// </summary>
/// <typeparam name="TSelf">类型自身。</typeparam>
/// <typeparam name="T">类型内部的元素。</typeparam>
public interface IGetPinnableReference<TSelf, T> where TSelf : allows ref struct where T : allows ref struct
{
	// 这里，基接口成员也需要标记 UnscopedRefAttribute，即使你不知道派生类型要不要用。
	[UnscopedRef]
	public abstract ref readonly T GetPinnableReference();
}
