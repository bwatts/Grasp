﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace Grasp.Knowledge
{
	public class FullNames
	{
		[Fact] public void CreateWithoutNamespace()
		{
			var value = "X";

			var fullName = new FullName(value);

			((object) fullName.Namespace).Should().Be(Namespace.Root);
			fullName.Identifier.Should().Be(new Identifier(value));
			fullName.Value.Should().Be(value);
		}

		[Fact] public void CreateWithNamespace()
		{
			var fullName = new FullName("Grasp.X");

			((object) fullName.Namespace).Should().Be(new Namespace("Grasp"));
			fullName.Identifier.Should().Be(new Identifier("X"));
			fullName.Value.Should().Be("Grasp.X");
		}

		[Fact] public void CreateWithMultiPartNamespace()
		{
			var fullName = new FullName("Grasp.Knowledge.X");

			((object) fullName.Namespace).Should().Be(new Namespace("Grasp.Knowledge"));
			fullName.Identifier.Should().Be(new Identifier("X"));
			fullName.Value.Should().Be("Grasp.Knowledge.X");
		}

		[Fact] public void CreateWithNonIdentifier()
		{
			Assert.Throws<FormatException>(() => new FullName("0"));
		}

		[Fact] public void CreateWithNonNamespace()
		{
			Assert.Throws<FormatException>(() => new FullName("0.X"));
		}

		[Fact] public void CreateWithNamespaceAndNonIdentifier()
		{
			Assert.Throws<FormatException>(() => new FullName("Grasp.0"));
		}

		[Fact] public void CreateWithMultiPartNamespaceAndNonIdentifier()
		{
			Assert.Throws<FormatException>(() => new FullName("Grasp.Knowledge.0"));
		}

		[Fact] public void GetIdentifiers()
		{
			var fullName = new Namespace("X.Y.Z");

			((IEnumerable<Identifier>) fullName).Should().Equal(new Identifier("X"), new Identifier("Y"), new Identifier("Z"));
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
		public void IdentifierIsFullName(string value)
		{
			FullName.IsFullName(value).Should().BeTrue();
		}

		[Theory]
		[InlineData("A.A")]
		[InlineData("A0.A")]
		[InlineData("A.A0")]
		[InlineData("A0A.A")]
		[InlineData("A.A0A")]
		[InlineData("_.A")]
		[InlineData("A._")]
		[InlineData("_A.A")]
		[InlineData("A._A")]
		[InlineData("A_.A")]
		[InlineData("A.A_")]
		[InlineData("A_A.A")]
		[InlineData("A.A_A")]
		[InlineData("_A0.A")]
		[InlineData("A._A0")]
		[InlineData("_0A.A")]
		[InlineData("A._0A")]
		[InlineData("A_0.A")]
		[InlineData("A.A_0")]
		[InlineData("A0_.A")]
		[InlineData("A.A0_")]
		[InlineData("_A0A.A")]
		[InlineData("A._A0A")]
		public void NamespaceAndIdentifierIsFullName(string value)
		{
			FullName.IsFullName(value).Should().BeTrue();
		}

		[Theory]
		[InlineData("A.A.A")]
		[InlineData("A0.A.A")]
		[InlineData("A.A0.A")]
		[InlineData("A.A.A0")]
		[InlineData("A0A.A.A")]
		[InlineData("A.A0A.A")]
		[InlineData("A.A.A0A")]
		[InlineData("_.A.A")]
		[InlineData("A._.A")]
		[InlineData("A.A._")]
		[InlineData("_A.A.A")]
		[InlineData("A._A.A")]
		[InlineData("A.A._A")]
		[InlineData("A_.A.A")]
		[InlineData("A.A_.A")]
		[InlineData("A.A.A_")]
		[InlineData("A_A.A.A")]
		[InlineData("A.A_A.A")]
		[InlineData("A.A.A_A")]
		[InlineData("_A0.A.A")]
		[InlineData("A._A0.A")]
		[InlineData("A.A._A0")]
		[InlineData("_0A.A.A")]
		[InlineData("A._0A.A")]
		[InlineData("A.A._0A")]
		[InlineData("A_0.A.A")]
		[InlineData("A.A_0.A")]
		[InlineData("A.A.A_0")]
		[InlineData("A0_.A.A")]
		[InlineData("A.A0_.A")]
		[InlineData("A.A.A0_")]
		[InlineData("_A0A.A.A")]
		[InlineData("A._A0A.A")]
		[InlineData("A.A._A0A")]
		public void MultiPartNamespaceAndIdentifierIsFullName(string value)
		{
			FullName.IsFullName(value).Should().BeTrue();
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
		public void IdentifierIsNotFullName(string value)
		{
			FullName.IsFullName(value).Should().BeFalse();
		}

		[Theory]
		[InlineData(".")]
		[InlineData("A.")]
		[InlineData(".A")]
		[InlineData("A. ")]
		[InlineData(" .A")]
		[InlineData("A.0")]
		[InlineData("0.A")]
		[InlineData("A.-")]
		[InlineData("-.A")]
		[InlineData("A.%")]
		[InlineData("%.A")]
		public void NamespaceAndIdentifierIsNotFullName(string value)
		{
			FullName.IsFullName(value).Should().BeFalse();
		}

		[Theory]
		[InlineData("..")]
		[InlineData("A..")]
		[InlineData("A.B.")]
		[InlineData("A..B")]
		[InlineData(".A.B")]
		[InlineData(".A.")]
		[InlineData("..A")]
		[InlineData(" .A.A")]
		[InlineData("A. .A")]
		[InlineData("A.A. ")]
		[InlineData("0.A.A")]
		[InlineData("A.0.A")]
		[InlineData("A.A.0")]
		[InlineData("0A.A.A")]
		[InlineData("A.0A.A")]
		[InlineData("A.A.0A")]
		[InlineData("-.A.A")]
		[InlineData("A.-.A")]
		[InlineData("A.A.-")]
		[InlineData("-A.A.A")]
		[InlineData("A.-A.A")]
		[InlineData("A.A.-A")]
		[InlineData("A-.A.A")]
		[InlineData("A.A-.A")]
		[InlineData("A.A.A-")]
		[InlineData("%.A.A")]
		[InlineData("A.%.A")]
		[InlineData("A.A.%")]
		[InlineData("%A.A.A")]
		[InlineData("A.%A.A")]
		[InlineData("A.A.%A")]
		[InlineData("A%.A.A")]
		[InlineData("A.A%.A")]
		[InlineData("A.A.A%")]
		public void MultiPartNamespaceAndIdentifierIsNotFullName(string value)
		{
			FullName.IsFullName(value).Should().BeFalse();
		}
	}
}