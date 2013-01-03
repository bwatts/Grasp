using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak;

namespace Grasp.Knowledge
{
	/// <summary>
	/// The unit of knowledge in a schema
	/// </summary>
	public class Variable : NamedNotion
	{
		public static readonly Field<Type> TypeField = Field.On<Variable>.For(x => x.Type);

		/// <summary>
		/// Initializes a variable with the specified type and name
		/// </summary>
		/// <param name="type">The type of value represented by the variable</param>
		/// <param name="name">The name of the variable</param>
		public Variable(Type type, FullName name) : base(name)
		{
			Contract.Requires(type != null);
			Contract.Requires(name != null);

			Type = type;
		}

		/// <summary>
		/// Initializes a variable with the specified type and name
		/// </summary>
		/// <param name="type">The type of value represented by the variable</param>
		/// <param name="name">The name of the variable</param>
		public Variable(Type type, string name) : this(type, new FullName(name))
		{}

		/// <summary>
		/// Gets the type of value represented by this variable
		/// </summary>
		public Type Type { get { return GetValue(TypeField); } private set { SetValue(TypeField, value); } }

		/// <summary>
		/// Gets the fully-qualified name of this variable
		/// </summary>
		/// <returns>The fully-qualified name of this variable</returns>
		public override string ToString()
		{
			return Name.ToString();
		}

		/// <summary>
		/// Creates an expression tree node which represents the specified variable
		/// </summary>
		/// <returns>An expression tree node which represents the specified variable</returns>
		public VariableExpression ToExpression()
		{
			return new VariableExpression(this);
		}
	}

	/// <summary>
	/// The unit of knowledge in a schema
	/// </summary>
	/// <typeparam name="T">The type of value represented by the variable</typeparam>
	public class Variable<T> : Variable
	{
		/// <summary>
		/// Initializes a variable of type <typeparamref name="T"/> with the specified name
		/// </summary>
		/// <param name="name">The name of the variable</param>
		public Variable(FullName name) : base(typeof(T), name)
		{}

		/// <summary>
		/// Initializes a variable of type <typeparamref name="T"/> with the specified name
		/// </summary>
		/// <param name="name">The name of the variable</param>
		public Variable(string name) : this(new FullName(name))
		{}
	}
}