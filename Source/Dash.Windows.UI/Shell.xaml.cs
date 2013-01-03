using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows;
using Dash.Windows.UI.Presentation;

namespace Dash.Windows.UI
{
	public partial class Shell : Window
	{
		public Shell(BoardModel boardModel)
		{
			Contract.Requires(boardModel != null);

			InitializeComponent();

			DataContext = boardModel;
		}
	}
}