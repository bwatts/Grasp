using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloak;

namespace Grasp.Compilation
{
	internal sealed class DependencyGraph
	{
		private readonly Dictionary<CalculationSchema, DependencyNode> _nodes;

		internal DependencyGraph(IEnumerable<DependencyNode> nodes)
		{
			_nodes = nodes.ToDictionary(node => node.Calculation);
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

		// http://www.patrickdewane.com/2009/03/topological-sort.html

		private sealed class TopologicalSort
		{
			private readonly VisitHistory _visitHistory = new VisitHistory();
			private readonly List<DependencyNode> _sortedNodes = new List<DependencyNode>();
			private readonly DependencyGraph _graph;

			internal TopologicalSort(DependencyGraph graph)
			{
				_graph = graph;			}

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
					}

					_sortedNodes.Add(node);
				}
			}
		}

		private sealed class VisitHistory
		{
			private readonly HashSet<DependencyNode> _visitedNodes = new HashSet<DependencyNode>();
			private HashSet<DependencyNode> _visitedNodesFromRoot;
			private List<DependencyNode> _visitedNodesFromRootInOrder;

			internal void OnVisitingRootNode()
			{
				_visitedNodesFromRoot = new HashSet<DependencyNode>();
				_visitedNodesFromRootInOrder = new List<DependencyNode>();
			}

			internal bool OnVisitingNode(DependencyNode node)
			{
				if(_visitedNodesFromRoot.Contains(node))
				{
					ThrowCalculationCycleException(node.Calculation.Source);
				}

				var firstVisit = !_visitedNodes.Contains(node);

				if(firstVisit)
				{
					_visitedNodes.Add(node);

					_visitedNodesFromRoot.Add(node);
					_visitedNodesFromRootInOrder.Add(node);
				}

				return firstVisit;
			}

			private void ThrowCalculationCycleException(Calculation repeatedCalculation)
			{
				var calculations = _visitedNodesFromRootInOrder.Select(visitedNode => visitedNode.Calculation.Source).ToList();

				throw new CalculationCycleException(
					calculations,
					repeatedCalculation,
					Resources.CalculationHasCycle.FormatInvariant(GetCycleText(calculations, repeatedCalculation)));
			}

			private static string GetCycleText(IEnumerable<Calculation> calculations, Calculation repeatedCalculation)
			{
				var text = new StringBuilder(Environment.NewLine);

				var wroteFirst = false;

				foreach(var calculation in calculations)
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