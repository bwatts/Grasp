using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Grasp.Checks.Annotation
{
	/// <summary>
	/// Indicates an error in the Grasp check annotation system
	/// </summary>
	[Serializable]
	public class AnnotationException : CheckException
	{
		/// <summary>
		/// Initializes an exception with no message
		/// </summary>
		public AnnotationException()
		{}

		/// <summary>
		/// Initializes an exception with the specified message
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		public AnnotationException(string message) : base(message)
		{}

		/// <summary>
		/// Initializes an exception with the specified message and inner exception
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		/// <param name="inner">The exception that is the cause of this exception</param>
		public AnnotationException(string message, Exception inner) : base(message, inner)
		{}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		protected AnnotationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{}
	}
}