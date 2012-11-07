using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Grasp.Hypermedia
{
	/// <summary>
	/// Indicates an error in the Grasp hypermedia system
	/// </summary>
	[Serializable]
	public class HypermediaException : GraspException
	{
		/// <summary>
		/// Initializes an exception with no message
		/// </summary>
		public HypermediaException()
		{}

		/// <summary>
		/// Initializes an exception with the specified message
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		public HypermediaException(string message) : base(message)
		{}

		/// <summary>
		/// Initializes an exception with the specified message and inner exception
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		/// <param name="inner">The exception that is the cause of this exception</param>
		public HypermediaException(string message, Exception inner) : base(message, inner)
		{}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		protected HypermediaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{}
	}
}