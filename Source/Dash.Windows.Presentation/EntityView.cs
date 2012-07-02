using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Cloak;
using Cloak.Wpf.Mvvm;
using Dash.Infrastructure;
using Grasp.Knowledge;
using Grasp.Semantics;

namespace Dash.Windows.Presentation
{
	public class EntityView : TypeView
	{
		private readonly EntityModel _entity;

		public EntityView(EntityModel entity, IBoardContext dashContext, Func<TypeModel, TypeExplorerView> typeExplorerFactory)
			: base(entity, dashContext, typeExplorerFactory)
		{
			_entity = entity;
		}

		public int FieldCount
		{
			get { return _entity.Fields.Count(); }
		}
	}
}