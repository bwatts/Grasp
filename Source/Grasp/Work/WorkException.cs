using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Grasp.Work
{
	/// <summary>
	/// Indicates an error during work in a Grasp system
	/// </summary>
	[Serializable]
	public class WorkException : GraspException
	{
		/// <summary>
		/// Initializes an exception with no message
		/// </summary>
		public WorkException()
		{}

		/// <summary>
		/// Initializes an exception with the specified message
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		public WorkException(string message) : base(message)
		{}

		/// <summary>
		/// Initializes an exception with the specified message and inner exception
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		/// <param name="inner">The exception that is the cause of this exception</param>
		public WorkException(string message, Exception inner) : base(message, inner)
		{}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		protected WorkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{}
	}
}