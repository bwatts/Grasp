using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit.Extensions;

namespace Grasp.Analysis
{
	public class MultiPartNamespaces
	{
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
		public void IsMultiPartNamespace(string @namespace)
		{
			Variable.IsNamespace(@namespace).Should().BeTrue();
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
		public void IsNotMultiPartNamespace(string nonNamespace)
		{
			Variable.IsNamespace(nonNamespace).Should().BeFalse();
		}

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
		public void IsSeveralPartNamespace(string @namespace)
		{
			Variable.IsNamespace(@namespace).Should().BeTrue();
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
		public void IsNotSeveralPartNamespace(string nonNamespace)
		{
			Variable.IsNamespace(nonNamespace).Should().BeFalse();
		}
	}
}