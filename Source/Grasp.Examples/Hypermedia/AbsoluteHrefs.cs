using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Grasp.Hypermedia
{
	public class AbsoluteHrefs
	{
		[Fact] public void Text()
		{
			var absoluteUri = Href.Root.Then("text").ToAbsoluteUri(new Uri("http://localhost"));

			absoluteUri.Should().Be(new Uri("http://localhost/text"));
		}

		[Fact] public void Parameter()
		{
			var absoluteUri = Href.Root.ThenParameter("a").ToAbsoluteUri(new Uri("http://localhost"), allowTemplate: true);

			absoluteUri.Should().Be(new Uri("http://localhost/{a}"));
		}

		[Fact] public void Object()
		{
			var absoluteUri = Href.Root.Then(1).ToAbsoluteUri(new Uri("http://localhost"));

			absoluteUri.Should().Be(new Uri("http://localhost/1"));
		}

		[Fact] public void FormattedObject()
		{
			var formatInfo = (NumberFormatInfo) NumberFormatInfo.CurrentInfo.Clone();
			formatInfo.NegativeSign = "NEGATIVE";

			var absoluteUri = Href.Root.Then(-1, formatInfo).ToAbsoluteUri(new Uri("http://localhost"));

			absoluteUri.Should().Be(new Uri("http://localhost/NEGATIVE1"));
		}

		[Fact] public void Nested()
		{
			var absoluteUri = Href.Root.Then("a").ThenParameter("b").Then(1).ToAbsoluteUri(new Uri("http://localhost"), allowTemplate: true);

			absoluteUri.Should().Be(new Uri("http://localhost/a/{b}/1"));
		}
	}
}