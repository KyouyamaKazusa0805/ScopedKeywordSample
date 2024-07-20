#region 注释
// 特性：
//   C# 11 scoped 关键字
//
// 相关特性：
//   C# 11 接口内静态成员的默认实现
//   C# 13 ref struct 实现接口
//   C# 13 泛型参数 allows ref struct 约束
//   C# 13 params 集合
//   C# 13 用于异步和迭代器函数内的不安全代码（指针、引用）和 ref struct
//
// 描述：
//   为 ref struct 类型以及引用支持生命周期限定（C# 11），
//   并支持对泛型参数的生命周期限定（C# 13）。
//
// 主要内容：
//   1）了解 ref struct 实例和传引用的实例的参数和返回值出入规则。
//   2）了解同时存在同类型参数时，参数的不同使用域的规则。
//   3）scoped 关键字和 UnscopedRefAttribute 的使用方式：
//     a. scoped 关键字：告知编译器该参数只在这个方法范围内使用，
//        或是方法里调用的一些函数使用，绝对不拓展使用域。
//     b. UnscopedRefAttribute 特性：告知编译器我要把 this 等实例向上一层返回。
//   4）其他 scoped 的“意外”用途（如 Unsafe 类型里的一些方法）。
//
// 注意事项：
//   请安装 .NET 9 SDK，并且配置项目 <LangVersion>preview</LangVersion>。
#endregion

//
// 第一部分：介绍生命周期（变量可用范围，使用域）的概念
//
ShowExample<SameRefStructLifetimeExample>();
ShowExample<ExtendLocalLifetimeByRefReturningExample>();
ShowExample<ExtendLocalLifetimeByRefFieldExample>();
ShowExample<MiscellaneousLifetimeExample>();
ShowExample<ReturnOnlyExample>();
ShowExample<OutParametersExample>();

//
// 第二部分：scoped 和 UnscopedRefAttribute 的用法
//
ShowExample<ScopedParametersExample>();
ShowExample<ScopedLocalsExample>();
ShowExample<UnscopedRefAttributeToParameterExample>();
ShowExample<UnscopedRefAttributeToMethodLikeMemberExample>();
ShowExample<UnscopedRefAttributeToInterfaceImplementedMemberExample>();

//
// 第三部分：其他 scoped 关键字的用法
//
ShowExample<ScopedGenericParameterExample>();
ShowExample<SpecialScopedKeywordExample>();


/// <summary>
/// 项目的主入口点。
/// </summary>
file static partial class Program
{
	/// <summary>
	/// 展示样例的执行内容。
	/// </summary>
	/// <typeparam name="TExample">实例的类型。</typeparam>
	private static void ShowExample<TExample>()
		where TExample : IExample, new(), allows ref struct
	{
		var instance = new TExample();
		Console.WriteLine($"展示例子：{TExample.Description}{(TExample.Link is not null ? $"\r\n请参考链接：{TExample.Link}" : null)}");
		Console.WriteLine("---");
		Console.WriteLine();
		instance.ShowExample();
	}
}
