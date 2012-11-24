using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grasp.Checks.Rules
{
	/// <summary>
	/// Represents the application of a check method to the target data
	/// </summary>
	public sealed class CheckRule : Rule
	{
		public static readonly Field<MethodInfo> MethodField = Grasp.Field.On<CheckRule>.For(x => x.Method);
		public static readonly Field<ManyInOrder<object>> CheckArgumentsField = Grasp.Field.On<CheckRule>.For(x => x.CheckArguments);

		internal CheckRule(MethodInfo method, IEnumerable<object> checkArguments) : base(RuleType.Check)
		{
			Method = method;
			CheckArguments = checkArguments.ToManyInOrder();
		}

		/// <summary>
		/// Gets the check method
		/// </summary>
		public new MethodInfo Method { get { return GetValue(MethodField); } private set { SetValue(MethodField, value); } }

		/// <summary>
		/// Gets the arguments to the check method (excluding the base check argument)
		/// </summary>
		public ManyInOrder<object> CheckArguments { get { return GetValue(CheckArgumentsField); } private set { SetValue(CheckArgumentsField, value); } }
	}
}