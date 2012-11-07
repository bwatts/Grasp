﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit.Extensions;

namespace Grasp.Analysis
{
	public class Names
	{
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
		public void IsName(string name)
		{
			Variable.IsName(name).Should().BeTrue();
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
		public void IsNotName(string nonName)
		{
			Variable.IsName(nonName).Should().BeFalse();
		}
	}
}