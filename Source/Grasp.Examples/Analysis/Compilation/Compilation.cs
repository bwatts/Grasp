using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cloak;
using FakeItEasy;
using FluentAssertions;
using Xbehave;

namespace Grasp.Analysis.Compilation
{
	public class Compilation
	{
		[Scenario]
		public void ImplicitVariable(Variable variable, GraspSchema schema, GraspExecutable executable)
		{
			"Given a variable".Given(() => variable = new Variable<int>("X"));
			"And a schema with that variable and a calculation involving it".And(() => schema = new GraspSchema(
				calculations: Params.Of(
					new Calculation(variable, Expression.Constant(1)),
					new Calculation(new Variable<int>("Grasp.B"), Variable.Expression(variable)))));

			"When compiling an executable from the schema".When(() => executable = schema.Compile());

			"It compiles successfully".Then(() => executable.Should().NotBeNull());
		}

		[Scenario]
		public void UnknownVariable(Variable variable, GraspSchema schema, CompilationException exception)
		{
			"Given a variable".Given(() => variable = new Variable<int>("X"));
			"And a schema without that variable but a calculation involving it".And(() => schema = new GraspSchema(
				calculations: Params.Of(new Calculation(new Variable<int>("A"), Variable.Expression(variable)))));

			"When compiling an executable from the schema".When(() =>
			{
				try
				{
					schema.Compile();
				}
				catch(CompilationException ex)
				{
					exception = ex;
				}
			});

			"It throws an exception".Then(() => exception.Should().NotBeNull());
		}

		[Scenario]
		public void UnassignableType(GraspSchema schema, CompilationException exception)
		{
			"Given a schema with a calculation that resolves to an unassignable type".And(() => schema = new GraspSchema(
				calculations: Params.Of(new Calculation(new Variable<int>("A"), Expression.Constant("")))));

			"When compiling an executable from the schema".When(() =>
			{
				try
				{
					schema.Compile();
				}
				catch(CompilationException ex)
				{
					exception = ex;
				}
			});

			"It throws an exception".Then(() => exception.Should().NotBeNull());
		}

		[Scenario]
		public void UnknownExpression(GraspSchema schema, CompilationException exception)
		{
			"Given a schema with a calculation that has an invalid expression".And(() => schema = new GraspSchema(
				calculations: Params.Of(new Calculation(new Variable<int>("A"), new UnknownExpressionNode()))));

			"When compiling an executable from the schema".When(() =>
			{
				try
				{
					schema.Compile();
				}
				catch(CompilationException ex)
				{
					exception = ex;
				}
			});

			"It throws an exception".Then(() => exception.Should().NotBeNull());
		}

		[Scenario]
		public void DerivedOutputType(GraspSchema schema, GraspExecutable executable)
		{
			"Given a schema with a calculation that outputs a value of a derived type".And(() => schema = new GraspSchema(
				calculations: Params.Of(new Calculation(new Variable<Base>("A"), Expression.Constant(new Derived())))));

			"When compiling an executable from the schema".When(() => executable = schema.Compile());

			"It compiles successfuly".Then(() => executable.Should().NotBeNull());
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