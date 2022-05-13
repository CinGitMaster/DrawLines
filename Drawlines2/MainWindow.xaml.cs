using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.WindowManagement;

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using WinRT;
using static Drawlines2.App;
using System.Threading;
using Drawlines2.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using Drawlines2.Services;
using ScreenKeyboard;
using Drawlines2.Models;
using Windows.UI.ViewManagement;




// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Drawlines2 {
	/// <summary>
	/// An empty window that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainWindow : Window {



		[ComImport, Guid("4ce576fa-83dc-4F88-951c-9d0782b4e376")]
		class UIHostNoLaunch {
		}

		[ComImport, Guid("37c994e7-432b-4834-a2f7-dce1f13b834b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		interface ITipInvocation {
			void Toggle(IntPtr hwnd);
		}

		[DllImport("user32.dll", SetLastError = false)]
		static extern IntPtr GetDesktopWindow();

		private ServicesCalculateRemainder servicesCalculateRemainder;
		private ServiceGenerate_GCodeDrawLines serviceGenerate_GCodeDrawLines;
		
		public MainWindow() {
			this.InitializeComponent();
			// SubClassing(); bepaalde techniek, dit kan weg

			Title = "Drawlines2";
			ExtendsContentIntoTitleBar = true;
			SetTitleBar(TitleBar);

			var btnDummy = new Button();
			btnDummy.Tag = "1";
			var events = new RoutedEventArgs();
			btnShowTab_Click(btnDummy, events);
			btnDummy = null;

			if(DefaultBrushColor == null) {
				DefaultBrushColor = MessageLineStackComponent.Background;
			}

			PlateVM = Ioc.Default.GetService<PlateTotalSizeViewModel>();
			PiecesVM = Ioc.Default.GetService<PiecesFromPlateViewModel>();
			SingleLineVM = Ioc.Default.GetService<SinglelineViewModel>();
			MessageVM = Ioc.Default.GetService<MessageViewModel>();
			MessageLineComponent.Text = "Dummy tekst, opgeslagen in D:'/Bla";


			servicesCalculateRemainder = Ioc.Default.GetService<ServicesCalculateRemainder>();
			serviceGenerate_GCodeDrawLines = Ioc.Default.GetService<ServiceGenerate_GCodeDrawLines>();
		}

		public PlateTotalSizeViewModel PlateVM { get; set; }
		public PiecesFromPlateViewModel PiecesVM { get; set; }
		public SinglelineViewModel SingleLineVM { get; set; }
		public MessageViewModel MessageVM { get; set; }
		


		private delegate IntPtr WinProc(IntPtr hWnd, PInvoke.User32.WindowMessage Msg, IntPtr wParam, IntPtr lParam);
		private WinProc newWndProc = null;
		private IntPtr oldWndProc = IntPtr.Zero;
		[DllImport("user32")]
		private static extern IntPtr SetWindowLong(IntPtr hWnd, PInvoke.User32.WindowLongIndexFlags nIndex, WinProc newProc);
		[DllImport("user32.dll")]
		static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, PInvoke.User32.WindowMessage Msg, IntPtr wParam, IntPtr lParam);


		private Brush DefaultBrushColor = null;
		private void ClearMessageLine() {
			MessageLineComponent.Text = $"";
			MessageLineStackComponent.Background = DefaultBrushColor;
		}

		SolidColorBrush brushYellow = new SolidColorBrush(Colors.Yellow);
		SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);





		private void btnGenerateDinFileMultipleLines_click(object sender, RoutedEventArgs e) {
			
			var exportDirDinFile = "C:/ProgramData/Cutting/prg";
			var servremainder = servicesCalculateRemainder;

			var resultFile = serviceGenerate_GCodeDrawLines.GenerateoMultiplePieces_DinFile(pathFolder: exportDirDinFile, widthMillimeters: servremainder.PlateTotalSize.PlateWidth, yDirectionMillimeters: servremainder.PlateTotalSize.PlateYLength, servremainder.Pieces.ListPieces).GetAwaiter().GetResult();
			if (!resultFile.Item1) {
				MessageLineStackComponent.Background = brushRed;
				MessageLineComponent.Text = $"Fout bij aanmaken van dinfFile: {resultFile.Item2}";
			}
			else {
				MessageLineStackComponent.Background = brushYellow;
				MessageLineComponent.Text = $"Succesvol aangemaakt: {resultFile.Item2}";
			}
		}

		private void btnGenerateDinFile_Singleline_YLength_click(object sender, RoutedEventArgs e) {
			
			var exportDirDinFile = "C:/ProgramData/Cutting/prg";
			var SingleLine = this.SingleLineVM.SingleLine;
			var resultFile = serviceGenerate_GCodeDrawLines.Generate_Singleline_YLength_DinFile(pathFolder: exportDirDinFile, YDirectionMillimeters: SingleLine.YLength).GetAwaiter().GetResult();
			if (!resultFile.Item1) {
				MessageLineStackComponent.Background = brushRed;
				MessageLineComponent.Text = $"Fout bij aanmaken van dinfFile: {resultFile.Item2}";
			}
			else {
				MessageLineStackComponent.Background = brushYellow;
				MessageLineComponent.Text = $"Succesvol aangemaakt: {resultFile.Item2}";
			}
		}

		private void btnGenerateDinFile_Singleline_XLength_click(object sender, RoutedEventArgs e) {
			
			var exportDirDinFile = "C:/ProgramData/Cutting2/prg";
			var SingleLine = this.SingleLineVM.SingleLine;
			var resultFile = serviceGenerate_GCodeDrawLines.Generate_Singleline_XLength_DinFile(pathFolder: exportDirDinFile, XDirectionMillimeters: SingleLine.XLength).GetAwaiter().GetResult();
			if (!resultFile.Item1) {
				MessageLineStackComponent.Background = brushRed;				
				MessageLineComponent.Text = $"Fout bij aanmaken van dinfFile: {resultFile.Item2}";
			}
			else {
				MessageLineStackComponent.Background = brushYellow;
				MessageLineComponent.Text = $"Succesvol aangemaakt: {resultFile.Item2}";
			}
		}



		[StructLayout(LayoutKind.Sequential)]
		struct MINMAXINFO {
			public PInvoke.POINT ptReserved;
			public PInvoke.POINT ptMaxSize;
			public PInvoke.POINT ptMaxPosition;
			public PInvoke.POINT ptMinTrackSize;
			public PInvoke.POINT ptMaxTrackSize;
		}

		private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs args) {
			WindowUIHelper.SetWindowBounds(0, 0, 440, 800, AppContainer.main_windowHandle);
		}

		/*
		private int MinWidth = 500;
		private int MinHeight = 800;
		private IntPtr NewWindowProc(IntPtr hWnd, PInvoke.User32.WindowMessage Msg, IntPtr wParam, IntPtr lParam) {
			switch (Msg) {
				case PInvoke.User32.WindowMessage.WM_GETMINMAXINFO:
					var dpi = PInvoke.User32.GetDpiForWindow(hWnd);
					float scalingFactor = (float)dpi / 96;

					MINMAXINFO minMaxInfo = Marshal.PtrToStructure<MINMAXINFO>(lParam);
					minMaxInfo.ptMinTrackSize.x = (int)(MinWidth * scalingFactor);
					minMaxInfo.ptMinTrackSize.y = (int)(MinHeight * scalingFactor);
					Marshal.StructureToPtr(minMaxInfo, lParam, true);
					break;

			}
			return CallWindowProc(oldWndProc, hWnd, Msg, wParam, lParam);
		}
		
		// How to set minimum from https://github.com/microsoft/microsoft-ui-xaml/issues/2945
		private void SubClassing() {
			//Get the Window's HWND
			var hwnd = this.As<IWindowNative>().WindowHandle;
			newWndProc = new WinProc(NewWindowProc);
			oldWndProc = SetWindowLong(hwnd, PInvoke.User32.WindowLongIndexFlags.GWL_WNDPROC, newWndProc);
		}

		 */



		private bool TabTipIsDisplayed_bit = false;

		private void ToggleTabTipKeyboard() {
			try {
				var uiHostNoLaunch = new UIHostNoLaunch();
				var tipInvocation = (ITipInvocation)uiHostNoLaunch;
				tipInvocation.Toggle(GetDesktopWindow());
				Marshal.ReleaseComObject(uiHostNoLaunch);
			}
			catch {
			}
		}

		private void ShowTabTipKeyboard() {
			if (!TabTipIsDisplayed_bit) {
				ScreenKeyboard.ScreenKeyboard.ToggleVisibility();
				TabTipIsDisplayed_bit = true;
			}
		}

		private void HideTabTipKeyboard() {
			if (TabTipIsDisplayed_bit) {
				ScreenKeyboard.ScreenKeyboard.ToggleVisibility();
				TabTipIsDisplayed_bit = false;
			}
		}

		private Control ControlLastFocus = null;
		private void textbox_GotFocus(object sender, RoutedEventArgs e) {
			var textbox = sender as TextBox;
			var tagNr = 0;
			if (textbox != null && textbox.TabIndex > 0) {
				tagNr = textbox.TabIndex;
				ControlLastFocus = textbox;

				switch (tagNr) {
					case 0:
						break;
					default:
						break;
				}
			}
		}


	

		private void btnShowTab_Click(object sender, RoutedEventArgs e) {
			var btn = sender as Button;
			if (btn.Tag != null) {
				var selTag = Convert.ToInt32(btn.Tag);
				 
				switch (selTag) {
					case 1:
						page1_PlatesizeInput.Visibility = Visibility.Visible;
						page2_MultipleLines.Visibility = Visibility.Collapsed;
						page3_SingleLine.Visibility = Visibility.Collapsed;
						ClearMessageLine();
						break;
					case 2:
						page1_PlatesizeInput.Visibility = Visibility.Collapsed;
						page2_MultipleLines.Visibility = Visibility.Visible;
						page3_SingleLine.Visibility = Visibility.Collapsed;
						ClearMessageLine();
						break;
					case 3:
						page1_PlatesizeInput.Visibility = Visibility.Collapsed;
						page2_MultipleLines.Visibility = Visibility.Collapsed;
						page3_SingleLine.Visibility = Visibility.Visible;
						ClearMessageLine();
						break;
					case 4:
						ScreenKeyboard.ScreenKeyboard.ToggleVisibility();
						if (ControlLastFocus != null) {
							ControlLastFocus.Focus(FocusState.Programmatic); 
						}
						ClearMessageLine();
						break;

				}
			}
		}



	


	}
}
