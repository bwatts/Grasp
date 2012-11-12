using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Grasp.Work
{
	/// <summary>
	/// Indicates a conflict in simultaeous work within an aggregate
	/// </summary>
	[Serializable]
	public class ConcurrencyException : Exception
	{
		/// <summary>
		/// Initializes an exception with no message
		/// </summary>
		public ConcurrencyException()
		{}

		/// <summary>
		/// Initializes an exception with the specified message
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		public ConcurrencyException(string message) : base(message)
		{}

		/// <summary>
		/// Initializes an exception with the specified message and inner exception
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		/// <param name="inner">The exception that is the cause of this exception</param>
		public ConcurrencyException(string message, Exception inner) : base(message, inner)
		{}

		/// <summary>
		/// Deserialization constructor
		/// </summary>
		protected ConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{}

		/// <summary>
		/// Gets the type of the aggregate with the concurrency issue
		/// </summary>
		public Type AggregateType { get; set; }

		/// <summary>
		/// Gets the ID of the aggregate with the concurrency issue
		/// </summary>
		public Guid AggregateId { get; set; }

		/// <summary>
		/// Gets the revision ID expected to be loaded
		/// </summary>
		public Guid ExpectedRevisionId { get; set; }

		/// <summary>
		/// Gets the revision ID actually loaded
		/// </summary>
		public Guid ActualRevisionId { get; set; }
	}
}