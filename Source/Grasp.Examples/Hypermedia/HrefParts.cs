using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Grasp.Hypermedia
{
	public class HrefParts
	{
		[Fact] public void Separator()
		{
			var separator = HrefPart.Separator;

			separator.Value.Should().Be("/");
			separator.IsSeparator.Should().BeTrue();
			separator.IsParameter.Should().BeFalse();
			separator.ParameterName.Should().BeEmpty();
		}

		[Fact] public void Parameter()
		{
			var parameter = HrefPart.Parameter("a");

			parameter.Value.Should().Be("{a}");
			parameter.IsSeparator.Should().BeFalse();
			parameter.IsParameter.Should().BeTrue();
			parameter.ParameterName.Should().Be("a");
		}

		[Fact] public void ParameterFromText()
		{
			var parameter = new HrefPart("{a}");

			parameter.Value.Should().Be("{a}");
			parameter.IsSeparator.Should().BeFalse();
			parameter.IsParameter.Should().BeTrue();
			parameter.ParameterName.Should().Be("a");
		}

		[Fact] public void NonParameter()
		{
			var parameter = new HrefPart("{a}x");

			parameter.Value.Should().Be("{a}x");
			parameter.IsSeparator.Should().BeFalse();
			parameter.IsParameter.Should().BeFalse();
			parameter.ParameterName.Should().BeEmpty();
		}

		[Fact] public void SplitEmpty()
		{
			var parts = HrefPart.Split("");

			parts.Should().HaveCount(1);
			parts.Single().Value.Should().BeEmpty();
		}

		[Fact]
		public void SplitEmptyInMiddle()
		{
			var parts = HrefPart.Split("1//2");

			parts.Should().HaveCount(3);
			parts.ElementAt(0).Value.Should().Be("1");
			parts.ElementAt(1).Value.Should().Be("");
			parts.ElementAt(2).Value.Should().Be("2");
		}

		[Fact]
		public void SplitEmptyAtEnd()
		{
			var parts = HrefPart.Split("1/2/");

			parts.Should().HaveCount(3);
			parts.ElementAt(0).Value.Should().Be("1");
			parts.ElementAt(1).Value.Should().Be("2");
			parts.ElementAt(2).Value.Should().Be("");
		}

		[Fact] public void SplitSingle()
		{
			var parts = HrefPart.Split("single");

			parts.Should().HaveCount(1);
			parts.Single().Value.Should().Be("single");
		}

		[Fact] public void SplitTwo()
		{
			var parts = HrefPart.Split("1/2");

			parts.Should().HaveCount(2);
			parts.ElementAt(0).Value.Should().Be("1");
			parts.ElementAt(1).Value.Should().Be("2");
		}

		[Fact] public void SplitThree()
		{
			var parts = HrefPart.Split("1/2/3");

			parts.Should().HaveCount(3);
			parts.ElementAt(0).Value.Should().Be("1");
			parts.ElementAt(1).Value.Should().Be("2");
			parts.ElementAt(2).Value.Should().Be("3");
		}

		[Fact] public void Escape()
		{
			var escapedValue = new HrefPart("A B").Escape();

			escapedValue.Should().Be("A%20B");
		}

		[Fact] public void BindParameter()
		{
			var part = new HrefPart("{a}");

			var boundPart = part.BindParameter("a", "1");

			boundPart.Value.Should().Be("1");
		}

		[Fact] public void BindDifferentParameter()
		{
			var part = new HrefPart("{a}");

			var boundPart = part.BindParameter("b", "1");

			boundPart.Value.Should().Be("{a}");
		}

		[Fact] public void BindNonParameter()
		{
			var part = new HrefPart("a");

			var boundPart = part.BindParameter("a", "1");

			boundPart.Value.Should().Be("a");
		}
	}
}