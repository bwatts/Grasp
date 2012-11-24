using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Grasp.Checks.Conditions
{
	/// <summary>
	/// A source which aggregates the conditions specified by multiple sources
	/// </summary>
	public sealed class CompositeConditionSource : Notion, IConditionSource
	{
		public static readonly Field<IEnumerable<IConditionSource>> _sourcesField = Field.On<CompositeConditionSource>.For(x => x._sources);

		private IEnumerable<IConditionSource> _sources { get { return GetValue(_sourcesField); } set { SetValue(_sourcesField, value); } }

		/// <summary>
		/// Initializes a composite source with the specified sources
		/// </summary>
		/// <param name="sources">The sources whose conditions are aggregated by the composite source</param>
		public CompositeConditionSource(IEnumerable<IConditionSource> sources)
		{
			Contract.Requires(sources != null);

			_sources = sources;
		}

		/// <summary>
		/// Initializes a composite source with the specified sources
		/// </summary>
		/// <param name="sources">The sources whose conditions are aggregated by the composite source</param>
		public CompositeConditionSource(params IConditionSource[] sources) : this(sources as IEnumerable<IConditionSource>)
		{}

		/// <summary>
		/// Gets all conditions specified by the sub-sources
		/// </summary>
		/// <returns>All conditions specified by the sub-sources</returns>
		public IEnumerable<Condition> GetConditions()
		{
			return _sources.SelectMany(source => source.GetConditions());
		}
	}
}