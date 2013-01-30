﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grasp.Checks.Methods;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Checks that the target <see cref="System.Char"/> is a symbol
	/// </summary>
	public sealed class CheckIsSymbolAttribute : CheckAttribute
	{
		/// <summary>
		/// Gets an instance of <see cref="IsSymbolMethod"/>
		/// </summary>
		/// <returns>An instance of <see cref="IsSymbolMethod"/></returns>
		public override ICheckMethod GetCheckMethod()
		{
			return new IsSymbolMethod();
		}
	}
}