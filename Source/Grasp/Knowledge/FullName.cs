using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge
{
	/// <summary>
	/// An identifier qualified with its position in a namespace hierarchy
	/// </summary>
	public sealed class FullName : ComparableValue<FullName, string>, IEnumerable<Identifier>
	{
		#region Naming
		/// <summary>
		/// Determines if the specified text is a valid full name
		/// </summary>
		/// <param name="value">The text to check if it is a full name</param>
		/// <returns>Whether the specified text is a full name</returns>
		[Pure]
		public static bool IsFullName(string value)
		{
			Contract.Requires(value != null);

			var separatorIndex = value.LastIndexOf('.');

			if(separatorIndex == -1)
			{
				return Identifier.IsIdentifier(value);
			}
			else
			{
				var left = value.Substring(0, separatorIndex);
				var right = separatorIndex == value.Length - 1 ? "" : value.Substring(separatorIndex + 1);

				return Namespace.IsNamespace(left) && Identifier.IsIdentifier(right);
			}
		}

		/// <summary>
		/// Attempts to get the namespace and identifier of the specified full name
		/// </summary>
		/// <param name="value">The full name containing a namespace and identifier</param>
		/// <param name="namespace">Is set to the namespace (if any) in the specified full name</param>
		/// <param name="identifier">Is set to the identifier (if any) in the specified full name</param>
		/// <returns>true if the specified full name is an identifier with or without a namespace; false otherwise</returns>
		[Pure]
		public static bool TryGetNamespaceAndIdentifier(string value, out Namespace @namespace, out Identifier identifier)
		{
			Contract.Requires(value != null);

			var isFullName = IsFullName(value);

			if(!isFullName)
			{
				@namespace = null;
				identifier = null;
			}
			else
			{
				var separatorIndex = value.LastIndexOf('.');

				if(separatorIndex == -1)
				{
					@namespace = Namespace.Root;
					identifier = new Identifier(value);
				}
				else
				{
					@namespace = new Namespace(value.Substring(0, separatorIndex));
					identifier = new Identifier(value.Substring(separatorIndex + 1));
				}
			}

			return isFullName;
		}
		#endregion

		public static readonly Field<Namespace> NamespaceField = Field.On<FullName>.For(x => x.Namespace);
		public static readonly Field<Identifier> IdentifierField = Field.On<FullName>.For(x => x.Identifier);

		/// <summary>
		/// Initializes a full name with the specified namespace and identifier
		/// </summary>
		/// <param name="namespace">The namespace portion of the full name</param>
		/// <param name="identifier">The identifier portion of the full name</param>
		public FullName(Namespace @namespace, Identifier identifier) : base(Resources.FullName.FormatInvariant(@namespace, identifier))
		{
			Contract.Requires(@namespace != null);
			Contract.Requires(identifier != null);

			Namespace = @namespace;
			Identifier = identifier;
		}

		/// <summary>
		/// Initializes a full name with the specified value
		/// </summary>
		/// <param name="value">The value of the full name</param>
		public FullName(string value) : base(value)
		{
			Namespace @namespace;
			Identifier identifier;

			if(!TryGetNamespaceAndIdentifier(value, out @namespace, out identifier))
			{
				throw new FormatException(Resources.NotFullName.FormatInvariant(value));
			}

			Namespace = @namespace;
			Identifier = identifier;
		}

		/// <summary>
		/// Gets the namespace of this full name
		/// </summary>
		public Namespace Namespace { get { return GetValue(NamespaceField); } private set { SetValue(NamespaceField, value); } }

		/// <summary>
		/// Gets the identifier of this full name
		/// </summary>
		public Identifier Identifier { get { return GetValue(IdentifierField); } private set { SetValue(IdentifierField, value); } }

		/// <summary>
		/// Gets an enumerator for the identifiers in this full name
		/// </summary>
		public IEnumerator<Identifier> GetEnumerator()
		{
			foreach(var namespaceIdentifier in Namespace)
			{
				yield return namespaceIdentifier;
			}

			yield return Identifier;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}