using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasp.Messaging;

namespace Grasp.Persistence
{
	/// <summary>
	/// Describes an object with events on a persistent timeline
	/// </summary>
	[ContractClass(typeof(IPersistentContract))]
	public interface IPersistent
	{
		/// <summary>
		/// Gets the identifier which uniquely identifies this object in its context
		/// </summary>
		object Id { get; }

		/// <summary>
		/// Gets the event representing the creation of this object
		/// </summary>
		Event CreatedEvent { get; }

		/// <summary>
		/// Gets the event representing the modification of this object
		/// </summary>
		Event ModifiedEvent { get; }

		/// <summary>
		/// Gets the event representing the reconstituion of this object
		/// </summary>
		Event ReconstitutedEvent { get; }
	}

	[ContractClassFor(typeof(IPersistent))]
	internal abstract class IPersistentContract : IPersistent
	{
		object IPersistent.Id
		{
			get
			{
				Contract.Ensures(Contract.Result<object>() != null);

				return null;
			}
		}

		Event IPersistent.CreatedEvent
		{
			get
			{
				Contract.Ensures(Contract.Result<Event>() != null);

				return null;
			}
		}

		Event IPersistent.ModifiedEvent
		{
			get
			{
				Contract.Ensures(Contract.Result<Event>() != null);

				return null;
			}
		}

		Event IPersistent.ReconstitutedEvent
		{
			get
			{
				Contract.Ensures(Contract.Result<Event>() != null);

				return null;
			}
		}
	}
}