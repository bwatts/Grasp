using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Dash.Windows.Presentation;

namespace Dash.Windows.UI.Views
{
	public partial class ShellWindow : Window
	{
		public ShellWindow(DashModel dash)
		{
			Contract.Requires(dash != null);

			InitializeComponent();

			DataContext = dash;
		}
	}
}