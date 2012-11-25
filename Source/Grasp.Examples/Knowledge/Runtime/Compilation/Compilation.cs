using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Grasp.Knowledge.Runtime.Compilation
{
	public class Compilation
	{
		[Fact] public void ImplicitVariable()
		{
			var variable = new Variable<int>("X");
			var schema = new Schema(calculations: Params.Of(
				new Calculation(variable, Expression.Constant(1)),
				new Calculation(new Variable<int>("Y"), variable.ToExpression())));

			var executable = schema.Compile();

			executable.Should().NotBeNull();
		}

		[Fact] public void UnknownVariable()
		{
			var variable = new Variable<int>("X");
			var schema = new Schema(calculations: Params.Of(new Calculation(new Variable<int>("A"), variable.ToExpression())));

			Assert.Throws<CompilationException>(() => schema.Compile());
		}

		[Fact] public void UnassignableType()
		{
			var schema = new Schema(calculations: Params.Of(new Calculation(new Variable<int>("A"), Expression.Constant(""))));

			Assert.Throws<CompilationException>(() => schema.Compile());
		}

		[Fact] public void UnknownExpression()
		{
			var schema = new Schema(calculations: Params.Of(new Calculation(new Variable<int>("A"), new UnknownExpressionNode())));

			Assert.Throws<CompilationException>(() => schema.Compile());
		}

		[Fact] public void DerivedOutputType()
		{
			var schema = new Schema(calculations: Params.Of(new Calculation(new Variable<Base>("A"), Expression.Constant(new Derived()))));

			var executable = schema.Compile();

			executable.Should().NotBeNull();
		}

		private sealed class UnknownExpressionNode : Expression
		{
			public override ExpressionType NodeType
			{
				get { return (ExpressionType) (-1); }
			}
		}

		private class Base
		{}

		private class Derived : Base
		{}
	}
}