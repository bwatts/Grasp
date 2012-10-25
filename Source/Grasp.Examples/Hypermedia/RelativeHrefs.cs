using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Grasp.Hypermedia
{
	public class RelativeHrefs
	{
		[Fact] public void Text()
		{
			var relativeUri = Href.Root.Then("text").ToUri();

			relativeUri.Should().Be(new Uri("text", UriKind.Relative));
		}

		[Fact] public void ParameterAllowTemplate()
		{
			var relativeUri = Href.Root.ThenParameter("a").ToUri(allowTemplate: true);

			relativeUri.Should().Be(new Uri("{a}", UriKind.Relative));
		}

		[Fact] public void ParameterDenyTemplate()
		{
			var baseHref = Href.Root.ThenParameter("a");

			Assert.Throws<InvalidOperationException>(() => baseHref.ToUri(allowTemplate: false));
		}

		[Fact] public void Object()
		{
			var relativeUri = Href.Root.Then(1).ToUri();

			relativeUri.Should().Be(new Uri("1", UriKind.Relative));
		}

		[Fact] public void FormattedObject()
		{
			var formatInfo = (NumberFormatInfo) NumberFormatInfo.CurrentInfo.Clone();
			formatInfo.NegativeSign = "NEGATIVE";

			var relativeUri = Href.Root.Then(-1, formatInfo).ToUri();

			relativeUri.Should().Be(new Uri("NEGATIVE1", UriKind.Relative));
		}

		[Fact] public void Multiple()
		{
			var formatInfo = (NumberFormatInfo) NumberFormatInfo.CurrentInfo.Clone();
			formatInfo.NegativeSign = "NEGATIVE";

			var absoluteUri = Href.Root.Then("a").ThenParameter("b").Then(1).Then(-2, formatInfo).ToUri(allowTemplate: true);

			absoluteUri.Should().Be(new Uri("a/{b}/1/NEGATIVE2", UriKind.Relative));
		}
	}
}