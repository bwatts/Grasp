using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using Xbehave;

namespace Grasp.Checks
{
	public class ThatChecks
	{
		[Scenario]
		public void Create(int target, ICheckable<int> thatCheck)
		{
			"Given a piece of target data".Given(() => target = 1);

			"When creating a 'That' check".When(() => thatCheck = Check.That(target));

			"The check is not null".Then(() => thatCheck.Should().NotBeNull());
			"The check has the specified target".Then(() => thatCheck.Target.Should().Be(target));
			"The check has the specified target type".Then(() => thatCheck.TargetType.Should().Be(target.GetType()));
		}

		[Scenario]
		public void Apply(ICheckable<int> thatCheck, bool result)
		{
			"Given a 'That' check".Given(() => thatCheck = Check.That(1));

			"When applying the check".When(() => result = thatCheck.Apply());

			"The result is true".Then(() => result.Should().BeTrue());
		}

		[Scenario]
		public void ApplyWithDefault(ICheckable<int> thatCheck, bool defaultResult, bool result)
		{
			"Given a default result".Given(() => defaultResult = false);
			"And a 'That' check with the default result".Given(() => thatCheck = Check.That(1, defaultResult));

			"When applying the check".When(() => result = thatCheck.Apply());

			"The result is the default result".Then(() => result.Should().Be(defaultResult));
		}

		[Scenario]
		public void ImplicitlyConvertToBoolean(Check<int> thatCheck, bool result)
		{
			"Given a check written for this example".Given(() => thatCheck = new ImplicitConversionCheck());

			"When applying the check implicitly".When(() => result = thatCheck);

			"The result is expected".Then(() => result.Should().BeFalse());
		}

		private sealed class ImplicitConversionCheck : Check<int>
		{
			internal ImplicitConversionCheck() : base(0)
			{}

			public override bool Apply()
			{
				return false;
			}
		}
	}
}