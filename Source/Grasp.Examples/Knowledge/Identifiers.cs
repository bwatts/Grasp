using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace Grasp.Knowledge
{
	public class Identifiers
	{
		[Fact] public void Create()
		{
			var value = "A";

			var identifier = new Identifier(value);

			identifier.Value.Should().Be(value);
		}

		[Fact] public void CreateInvalid()
		{
			Assert.Throws<FormatException>(() => new Identifier("0"));
		}

		[Fact] public void GetAnonymous()
		{
			Identifier.Anonymous.Value.Should().BeEmpty();
		}

		[Theory]
		[InlineData("A")]
		[InlineData("A0")]
		[InlineData("A0A")]
		[InlineData("_")]
		[InlineData("_A")]
		[InlineData("A_")]
		[InlineData("A_A")]
		[InlineData("_A0")]
		[InlineData("_0A")]
		[InlineData("A_0")]
		[InlineData("A0_")]
		[InlineData("_A0A")]
		public void IsIdentifier(string value)
		{
			Identifier.IsIdentifier(value).Should().BeTrue();
		}

		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData(" A")]
		[InlineData("A ")]
		[InlineData(" A ")]
		[InlineData("0")]
		[InlineData("0A")]
		[InlineData("-")]
		[InlineData("-A")]
		[InlineData("A-")]
		[InlineData("%")]
		[InlineData("%A")]
		[InlineData("A%")]
		public void IsNotIdentifier(string value)
		{
			Identifier.IsIdentifier(value).Should().BeFalse();
		}
	}
}