using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Grasp.Checks
{
	public class ThatChecks
	{
		[Fact] public void Create()
		{
			var target = 1;

			var thatCheck = Check.That(target);

			thatCheck.Should().NotBeNull();
			thatCheck.Target.Should().Be(target);
			thatCheck.TargetType.Should().Be(target.GetType());
		}

		[Fact] public void Apply()
		{
			var thatCheck = Check.That(1);

			var result = thatCheck.Apply();

			result.Should().BeTrue();
		}

		[Fact] public void ApplyWithDefault()
		{
			var defaultResult = false;
			var thatCheck = Check.That(1, defaultResult);

			var result = thatCheck.Apply();

			result.Should().Be(defaultResult);
		}

		[Fact] public void ImplicitlyConvertToBoolean()
		{
			var check = new TestCheck();

			bool result = check;

			result.Should().BeFalse();
		}

		private sealed class TestCheck : Check<int>
		{
			internal TestCheck() : base(0)
			{}

			public override bool Apply()
			{
				return false;
			}
		}
	}
}