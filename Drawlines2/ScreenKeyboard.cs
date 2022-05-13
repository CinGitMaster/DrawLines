using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScreenKeyboard
{
  public class ScreenKeyboard {

    public const UInt32 WS_VISIBLE = 0X94000000;
    public const int GWL_STYLE = -16;

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(String sClassName, String sAppName);

    [DllImport("user32.dll", SetLastError = true)]
    static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

    /// <summary>
    /// Gets the window handler for the virtual keyboard.
    /// </summary>
    /// <returns>The handle.</returns>
    public static IntPtr GetKeyboardWindowHandle() {
      return FindWindow("IPTip_Main_Window", null);
    }

    /// <summary>
    /// Checks to see if the virtual keyboard is visible.
    /// </summary>
    /// <returns>True if visible.</returns>
    public static bool IsKeyboardVisible() {
      IntPtr keyboardHandle = GetKeyboardWindowHandle();

      bool visible = false;

      if (keyboardHandle != IntPtr.Zero) {
        UInt32 style = GetWindowLong(keyboardHandle, GWL_STYLE);
        visible = (style == WS_VISIBLE);
      }

      return visible;
    }


    private static void StartTabTip() {
      var p = Process.Start(@"C:\Program Files\Common Files\Microsoft Shared\ink\TabTip.exe");
      int handle = 0;
      while ((handle = NativeMethods.FindWindow("IPTip_Main_Window", "")) <= 0) {
        Thread.Sleep(100);
      }
    }

    public static void ToggleVisibility() {
			try {
        var type = Type.GetTypeFromCLSID(Guid.Parse("4ce576fa-83dc-4F88-951c-9d0782b4e376"));
        var instance = (ITipInvocation)Activator.CreateInstance(type);
        instance.Toggle(NativeMethods.GetDesktopWindow());
        Marshal.ReleaseComObject(instance);
      }
      catch (Exception ex) {

			}
    }

    public static void Show() {
      int handle = NativeMethods.FindWindow("IPTip_Main_Window", "");  // IPTip_Main_Window
      if (handle <= 0) // nothing found
      {
        Console.WriteLine("Show function: StartTabTip because is no handle");
        StartTabTip();
        Thread.Sleep(100);
      }
      // on some devices starting TabTip don't show keyboard, on some does  ¯\_(ツ)_/¯
      if (!IsOpen()) {
        Console.WriteLine("Show function: Toggled because is NOT Open");
        ToggleVisibility();
      }
    }

    public static void Hide() {
      if (IsOpen()) {
        Console.WriteLine("Hide function: Toggled because is Open");
        ToggleVisibility();
      }
    }


    public static bool Close() {
      // find it
      int handle = NativeMethods.FindWindow("IPTIP_Main_Window", "");
      bool active = handle > 0;
      if (active) {
        // don't check style - just close
        NativeMethods.SendMessage(handle, NativeMethods.WM_SYSCOMMAND, NativeMethods.SC_CLOSE, 0);
      }
      return active;
    }

    public static bool IsOpen() {
      var retVal = false;
      retVal = IsKeyboardVisible();
      return retVal;

      var isOpen = GetIsOpen1709();
      if(isOpen != null) {
        retVal = (bool)isOpen ? true : false;
      }
			else{
        retVal = GetIsOpenLegacy();
      }

      //?? GetIsOpenLegacy();
      return retVal;
    }


    [DllImport("user32.dll", SetLastError = false)]
    private static extern IntPtr FindWindowEx(IntPtr parent, IntPtr after, string className, string title = null);

    private static bool? GetIsOpen1709() {
      // if there is a top-level window - the keyboard is closed
      var wnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, WindowClass1709, WindowCaption1709);
      if (wnd != IntPtr.Zero)
        return false;

      var parent = IntPtr.Zero;
      for (; ; )
      {
        parent = FindWindowEx(IntPtr.Zero, parent, WindowParentClass1709);
        if (parent == IntPtr.Zero)
          return null; // no more windows, keyboard state is unknown

        // if it's a child of a WindowParentClass1709 window - the keyboard is open
        wnd = FindWindowEx(parent, IntPtr.Zero, WindowClass1709, WindowCaption1709);
        if (wnd != IntPtr.Zero)
          return true;
      }
    }

    private static bool GetIsOpenLegacy() {
      var wnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, WindowClass);
      if (wnd == IntPtr.Zero)
        return false;

      var style = GetWindowStyle(wnd);
      return style.HasFlag(WindowStyle.Visible)
          && !style.HasFlag(WindowStyle.Disabled);
    }

    private const string WindowClass = "IPTip_Main_Window";
    private const string WindowParentClass1709 = "ApplicationFrameWindow";
    private const string WindowClass1709 = "Windows.UI.Core.CoreWindow";
    private const string WindowCaption1709 = "Microsoft Text Input Application";

    private enum WindowStyle : uint {
      Disabled = 0x08000000,
      Visible = 0x10000000,
    }

    private static WindowStyle GetWindowStyle(IntPtr wnd) {
      return (WindowStyle)GetWindowLong(wnd, -16);
    }

  }


  [ComImport]
  [Guid("37c994e7-432b-4834-a2f7-dce1f13b834b")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  interface ITipInvocation {
    void Toggle(IntPtr hwnd);
  }




  internal static class NativeMethods {
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    internal static extern int FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", EntryPoint = "SendMessage")]
    internal static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

    [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", SetLastError = false)]
    internal static extern IntPtr GetDesktopWindow();

    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    internal static extern int GetWindowLong(int hWnd, int nIndex);

    internal const int GWL_STYLE = -16;
    internal const int GWL_EXSTYLE = -20;
    internal const int WM_SYSCOMMAND = 0x0112;
    internal const int SC_CLOSE = 0xF060;

    internal const int WS_DISABLED = 0x08000000;

    internal const int WS_VISIBLE = 0x10000000;

  }
}
