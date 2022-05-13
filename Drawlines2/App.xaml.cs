﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WinRT;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Drawlines2.ViewModels;
using Drawlines2.Services;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Drawlines2
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			this.InitializeComponent();

			var calculateRemainderService = new ServicesCalculateRemainder();
			calculateRemainderService.Init();

			Ioc.Default.ConfigureServices(new ServiceCollection()
				.AddSingleton<PlateTotalSizeViewModel>()
				.AddSingleton<PiecesFromPlateViewModel>()
				.AddSingleton<MessageViewModel>()
				.AddSingleton<SinglelineViewModel>()
 			  .AddSingleton<ServicesCalculateRemainder>(calculateRemainderService)
				.AddSingleton<ServiceGenerate_GCodeDrawLines>()
				.BuildServiceProvider());
		}



		[ComImport]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("EECDBF0E-BAE9-4CB6-A68E-9598E1CB57BB")]
		internal interface IWindowNative
		{
			IntPtr WindowHandle { get; }
		}

		public IntPtr WindowHandle { get { return AppContainer.main_windowHandle; } }

		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used such as when the application is launched to open a specific file.
		/// </summary>
		/// <param name="args">Details about the launch request and process.</param>
		protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
		{
			Window m_window;
			IntPtr m_windowHandle;


			m_window = new MainWindow();
			m_window.Activate();


			// SetWindowsBoiunds from https://stackoverflow.com/questions/67169712/winui-3-0-reunion-0-5-window-size
			//Get the Window's HWND
			var windowNative = m_window.As<IWindowNative>();
			m_windowHandle = windowNative.WindowHandle;
			AppContainer.main_windowHandle = m_windowHandle;
			AppContainer.main_window = m_window;
			m_window.Activate();

			// Notabene, onderstaande functioneerd Plus Window_SizeChanged() zorgt voor vergrendeld Redizing
			WindowUIHelper.SetWindowBounds(0, 0, 440, 800, AppContainer.main_windowHandle);
			//System.Windows.Automation.AutomationElement.FromHandle(m_window.);

		}





	}
}