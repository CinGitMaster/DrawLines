using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace Drawlines2.ViewModels {

	[ObservableObject]
	public partial class MessageViewModel {

		[ObservableProperty]
		private string _MessageLine;
	}
}
