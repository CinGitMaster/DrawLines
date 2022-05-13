using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawlines2
{
    public class WindowUIHelper {

		public static void SetWindowBounds(int x, int y, int width, int height, IntPtr m_windowHandle) {
			var dpi = PInvoke.User32.GetDpiForWindow(m_windowHandle);
			float scalingFactor = (float)dpi / 96;
			width = (int)(width * scalingFactor);
			height = (int)(height * scalingFactor);

			PInvoke.User32.SetWindowPos(m_windowHandle, PInvoke.User32.SpecialWindowHandles.HWND_TOP,
																	x, y, width, height,
																	PInvoke.User32.SetWindowPosFlags.SWP_NOMOVE);
		}
	}
}
