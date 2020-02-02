using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.PlatformUI;
using System.Windows.Media;
using System;

namespace GitMoreOperations.VS2015.UI
{
    public static class CouleursThemeVS
    {
        public static ThemeResourceKey MainBackground
        {
            get { return EnvironmentColors.ToolWindowBackgroundBrushKey; }
        }

        public static ThemeResourceKey MainForeground
        {
            get { return EnvironmentColors.ToolWindowTextBrushKey; }
        }

        public static int ToInt32(this Color color)
        {
            return BitConverter.ToInt32(new byte[] { color.B, color.G, color.R, color.A }, 0);
        }
    }
}
