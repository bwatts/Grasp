﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Grasp
{
	/// <summary>
	/// Indicates an error in a Grasp system
	/// </summary>
	[Serializable]
	public class GraspException : Exception
	{
		/// <summary>
		/// Initializes an exception with no message
		/// </summary>
		public GraspException()
		{}

		/// <summary>
		/// Initializes an exception with the specified message
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		public GraspException(string message) : base(message)
		{}

		/// <summary>
		/// Initializes an exception with the specified message and inner exception
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		/// <param name="inner">The exception that is the cause of this exception</param>
		public GraspException(string message, Exception inner) : base(message, inner)
		{}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		protected GraspException(SerializationInfo info, StreamingContext context) : base(info, context)
		{}
	}
}