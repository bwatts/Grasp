using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xbehave;

namespace Grasp.Analysis
{
	public class MultiPartNamespaces
	{
		[Scenario]
		[Example("A.A")]
		[Example("A0.A")]
		[Example("A.A0")]
		[Example("A0A.A")]
		[Example("A.A0A")]
		[Example("_.A")]
		[Example("A._")]
		[Example("_A.A")]
		[Example("A._A")]
		[Example("A_.A")]
		[Example("A.A_")]
		[Example("A_A.A")]
		[Example("A.A_A")]
		[Example("_A0.A")]
		[Example("A._A0")]
		[Example("_0A.A")]
		[Example("A._0A")]
		[Example("A_0.A")]
		[Example("A.A_0")]
		[Example("A0_.A")]
		[Example("A.A0_")]
		[Example("_A0A.A")]
		[Example("A._A0A")]
		public void IsMultiPartNamespace(string @namespace)
		{
			"Given {0}".Given(() => { });

			"It is a multi-part variable namespace".Then(() => Variable.IsNamespace(@namespace).Should().BeTrue());
		}

		[Scenario]
		[Example(".")]
		[Example("A.")]
		[Example(".A")]
		[Example("A. ")]
		[Example(" .A")]
		[Example("A.0")]
		[Example("0.A")]
		[Example("A.-")]
		[Example("-.A")]
		[Example("A.%")]
		[Example("%.A")]
		public void IsNotMultiPartNamespace(string nonNamespace)
		{
			"Given {0}".Given(() => { });

			"It is not a multi-part variable namespace".Then(() => Variable.IsNamespace(nonNamespace).Should().BeFalse());
		}

		[Example("A.A.A")]
		[Example("A0.A.A")]
		[Example("A.A0.A")]
		[Example("A.A.A0")]
		[Example("A0A.A.A")]
		[Example("A.A0A.A")]
		[Example("A.A.A0A")]
		[Example("_.A.A")]
		[Example("A._.A")]
		[Example("A.A._")]
		[Example("_A.A.A")]
		[Example("A._A.A")]
		[Example("A.A._A")]
		[Example("A_.A.A")]
		[Example("A.A_.A")]
		[Example("A.A.A_")]
		[Example("A_A.A.A")]
		[Example("A.A_A.A")]
		[Example("A.A.A_A")]
		[Example("_A0.A.A")]
		[Example("A._A0.A")]
		[Example("A.A._A0")]
		[Example("_0A.A.A")]
		[Example("A._0A.A")]
		[Example("A.A._0A")]
		[Example("A_0.A.A")]
		[Example("A.A_0.A")]
		[Example("A.A.A_0")]
		[Example("A0_.A.A")]
		[Example("A.A0_.A")]
		[Example("A.A.A0_")]
		[Example("_A0A.A.A")]
		[Example("A._A0A.A")]
		[Example("A.A._A0A")]
		public void IsSeveralPartNamespace(string @namespace)
		{
			"Given {0}".Given(() => { });

			"It is a several-part variable namespace".Then(() => Variable.IsNamespace(@namespace).Should().BeTrue());
		}

		[Scenario]
		[Example("..")]
		[Example("A..")]
		[Example("A.B.")]
		[Example("A..B")]
		[Example(".A.B")]
		[Example(".A.")]
		[Example("..A")]
		[Example(" .A.A")]
		[Example("A. .A")]
		[Example("A.A. ")]
		[Example("0.A.A")]
		[Example("A.0.A")]
		[Example("A.A.0")]
		[Example("0A.A.A")]
		[Example("A.0A.A")]
		[Example("A.A.0A")]
		[Example("-.A.A")]
		[Example("A.-.A")]
		[Example("A.A.-")]
		[Example("-A.A.A")]
		[Example("A.-A.A")]
		[Example("A.A.-A")]
		[Example("A-.A.A")]
		[Example("A.A-.A")]
		[Example("A.A.A-")]
		[Example("%.A.A")]
		[Example("A.%.A")]
		[Example("A.A.%")]
		[Example("%A.A.A")]
		[Example("A.%A.A")]
		[Example("A.A.%A")]
		[Example("A%.A.A")]
		[Example("A.A%.A")]
		[Example("A.A.A%")]
		public void IsNotSeveralPartNamespace(string nonNamespace)
		{
			"Given {0}".Given(() => { });

			"It is not a several-part variable namespace".Then(() => Variable.IsNamespace(nonNamespace).Should().BeFalse());
		}
	}
}