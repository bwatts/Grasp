using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Knowledge.Runtime.Compilation
{
	internal sealed class DependencyGraph
	{
		private readonly Dictionary<CalculationSchema, DependencyNode> _nodes;
		private readonly Schema _schema;

		internal DependencyGraph(IEnumerable<DependencyNode> nodes, Schema schema)
		{
			_nodes = nodes.ToDictionary(node => node.Calculation);
			_schema = schema;
		}

		internal IEnumerable<CalculationSchema> OrderCalculations()
		{
			return new TopologicalSort(this).SortNodes().Select(node => node.Calculation);
		}

		private IEnumerable<DependencyNode> GetNodes()
		{
			return _nodes.Values;
		}

		private IEnumerable<DependencyNode> GetDependencyNodes(DependencyNode node)
		{
			return node.Dependencies.Select(dependency => _nodes[dependency]);
		}

		// http://en.wikipedia.org/wiki/Topological_sorting

		private sealed class TopologicalSort
		{
			private readonly List<DependencyNode> _sortedNodes = new List<DependencyNode>();
			private readonly DependencyGraph _graph;
			private readonly VisitHistory _visitHistory;

			internal TopologicalSort(DependencyGraph graph)
			{
				_graph = graph;

				_visitHistory = new VisitHistory(_graph._schema);
			}

			internal IEnumerable<DependencyNode> SortNodes()
			{
				foreach(var rootNode in _graph.GetNodes())
				{
					_visitHistory.OnVisitingRootNode();

					VisitNode(rootNode);
				}

				return _sortedNodes;
			}

			private void VisitNode(DependencyNode node)
			{
				var firstVisit = _visitHistory.OnVisitingNode(node);

				if(firstVisit)
				{
					foreach(var dependencyNode in _graph.GetDependencyNodes(node))
					{
						VisitNode(dependencyNode);

						_visitHistory.OnVisitedDependencyNode();
					}

					_sortedNodes.Add(node);
				}
			}
		}

		private sealed class VisitHistory
		{
			private readonly HashSet<DependencyNode> _visitedNodes = new HashSet<DependencyNode>();
			private readonly Schema _schema;
			private Stack<DependencyNode> _context;

			internal VisitHistory(Schema schema)
			{
				_schema = schema;
			}

			internal void OnVisitingRootNode()
			{
				_context = new Stack<DependencyNode>();
			}

			internal void OnVisitedDependencyNode()
			{
				if(_context.Any())
				{
					_context.Pop();
				}
			}

			internal bool OnVisitingNode(DependencyNode node)
			{
				if(_context.Contains(node))
				{
					ThrowCalculationCycleException(node.Calculation.Source);
				}

				var firstVisit = !_visitedNodes.Contains(node);

				if(firstVisit)
				{
					_visitedNodes.Add(node);

					_context.Push(node);
				}

				return firstVisit;
			}

			private void ThrowCalculationCycleException(Calculation repeatedCalculation)
			{
				var context = _context.Reverse().Select(visitedNode => visitedNode.Calculation.Source).ToList();

				throw new CalculationCycleException(
					_schema,
					context,
					repeatedCalculation,
					Resources.CalculationHasCycle.FormatInvariant(GetCycleText(context, repeatedCalculation)));
			}

			private static string GetCycleText(IEnumerable<Calculation> context, Calculation repeatedCalculation)
			{
				var text = new StringBuilder(Environment.NewLine);

				var wroteFirst = false;

				foreach(var calculation in context)
				{
					if(wroteFirst)
					{
						text.AppendLine().Append(Resources.CalculationCycleReference.FormatInvariant(calculation.OutputVariable));
					}
					else
					{
						wroteFirst = true;

						text.Append(calculation.OutputVariable);
					}
				}

				text.AppendLine().Append(Resources.CalculationCycleRepeatedReference.FormatInvariant(repeatedCalculation.OutputVariable));

				return text.ToString();
			}
		}
	}
}