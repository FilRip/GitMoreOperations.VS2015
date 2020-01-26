using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.PlatformUI;

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
    }
}
