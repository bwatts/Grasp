using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xbehave;

namespace Grasp.Analysis
{
	public class SinglePartNamespaces
	{
		[Scenario]
		[Example("A")]
		[Example("A0")]
		[Example("A0A")]
		[Example("_")]
		[Example("_A")]
		[Example("A_")]
		[Example("A_A")]
		[Example("_A0")]
		[Example("_0A")]
		[Example("A_0")]
		[Example("A0_")]
		[Example("_A0A")]
		public void IsSinglePartNamespace(string @namespace)
		{
			"Given {0}".Given(() => { });

			"It is a single-part variable namespace".Then(() => Variable.IsNamespace(@namespace).Should().BeTrue());
		}

		[Scenario]
		[Example("")]
		[Example(" ")]
		[Example(" A")]
		[Example("A ")]
		[Example(" A ")]
		[Example("0")]
		[Example("0A")]
		[Example("-")]
		[Example("-A")]
		[Example("A-")]
		[Example("%")]
		[Example("%A")]
		[Example("A%")]
		public void IsNotSinglePartNamespace(string nonNamespace)
		{
			"Given {0}".Given(() => { });

			"It is not a single-part variable namespace".Then(() => Variable.IsNamespace(nonNamespace).Should().BeFalse());
		}
	}
}