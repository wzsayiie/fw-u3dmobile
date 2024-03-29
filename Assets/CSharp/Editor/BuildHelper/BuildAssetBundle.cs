using System.Collections.Generic;
using System.IO;

namespace U3DMobile.Edit
{
    internal static class BuildAssetBundle
    {
        internal static void SwitchAssetFlavors(HashSet<string> flavors, List<string> errors)
        {
            var directories = new List<string>();
            if (flavors != null)
            {
                foreach (string flavor in flavors)
                {
                    directories.Add($"{BuildPath.assetFlavorDirectory}/{flavor}");
                }
            }
            if (directories.Count == 0)
            {
                errors.Add(I18N.NoAssetFlavor);
                return;
            }

            foreach (string directory in directories)
            {
                if (!Directory.Exists(directory))
                {
                    errors.Add($"{I18N.NoFlavorDirectory}: {directory}");
                }
            }
            if (errors.Count > 0)
            {
                return;
            }
        }

        internal static void PackForAndroid(List<string> errors)
        {
        }

        internal static void PackForIOS(List<string> errors)
        {
        }
    }
}
