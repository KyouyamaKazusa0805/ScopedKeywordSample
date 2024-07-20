// 特性：
//   C# 11 scoped 关键字
//
// 主要目的：
//   为了在编译期间控制变量的生命周期。
//   因为 ref struct 类型只能在栈内存分配，而且不能以任何形式进入堆内存，
//   而栈内存是随着方法（函数）的开始执行和退出而产生内存分配和释放，是自动的行为
//   而 ref struct 类型和普通数据类型差不多，都可以定义字段和自动属性这种数据成员，
//   这样就会造成这个类型在使用的过程之中，内存底层都释放了还不知道的问题。
//   为了避免基于生命周期而产生的错误使用，scoped 关键字就可以限制这种行为。

#pragma warning disable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

unsafe
{
	ref var nullRef = ref *(Span<int>*)0;
	ref var spanNullRef = ref Unsafe.NullRef<Span<int>>();
}
