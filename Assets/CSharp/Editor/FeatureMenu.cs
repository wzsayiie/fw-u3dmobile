using UnityEditor;

namespace U3DMobile.Edit
{
    internal static class FunctionMenu
    {
        private const int GameProfileMenu   = 100;
        private const int BuildProfileMenu  = 200;
        private const int ExportPackageMenu = 300;

        [MenuItem(I18N.U3DMobile + I18N.GameProfile, false, GameProfileMenu)]
        internal static void ShowGameProfile()
        {
            UIHelper.PingPath<GameProfile>(GameProfile.SavedPath);
        }

        [MenuItem(I18N.U3DMobile + I18N.GameProfileOpt, false, GameProfileMenu)]
        internal static void ShowGameProfileOpt()
        {
            UIHelper.PingPath<GameProfileOpt>(GameProfileOpt.SavedPath);
        }
        
        [MenuItem(I18N.U3DMobile + I18N.SwitchAssetFlavors, false, GameProfileMenu)]
        internal static void SwitchAssetFlavors()
        {
            BuildHelper.SwitchAssetFlavors();
        }

        [MenuItem(I18N.U3DMobile + I18N.BuildProfile, false, BuildProfileMenu)]
        internal static void ShowBuildProfile()
        {
            UIHelper.PingPath<BuildProfile>(BuildProfile.SavedPath);
        }

        [MenuItem(I18N.U3DMobile + I18N.PackBundleForAndroid, false, BuildProfileMenu)]
        internal static void PackBundleForAndroid()
        {
            //显示二次确认的对话框, 防止误触导致耗时操作.
            bool yes = EditorUtility.DisplayDialog(
                I18N.Tips,
                I18N.PackBundleForAndroid,
                I18N.Yes,
                I18N.No
            );

            if (yes)
            {
                BuildHelper.PackBundlesForAndroid();
            }
        }

        [MenuItem(I18N.U3DMobile + I18N.PackBundleForIOS, false, BuildProfileMenu)]
        internal static void PackBundleForIOS()
        {
            bool yes = EditorUtility.DisplayDialog(
                I18N.Tips,
                I18N.PackBundleForIOS,
                I18N.Yes,
                I18N.No
            );

            if (yes)
            {
                BuildHelper.PackBundlesForIOS();
            }
        }

        [MenuItem(I18N.U3DMobile + I18N.CopyPatch, false, BuildProfileMenu)]
        internal static void CopyPatches()
        {
            BuildHelper.CopyPatches();
        }

        [MenuItem(I18N.U3DMobile + I18N.ExportAndroidAAB, false, ExportPackageMenu)]
        internal static void ExportAndroidAAB()
        {
            bool yes = EditorUtility.DisplayDialog(
                I18N.Tips,
                I18N.ExportAndroidAAB,
                I18N.Yes,
                I18N.No
            );

            if (yes)
            {
                BuildHelper.ExportAndroidAAB("master");
            }
        }

        [MenuItem(I18N.U3DMobile + I18N.ExportAndroidAPK, false, ExportPackageMenu)]
        internal static void ExportAndroidAPK()
        {
            bool yes = EditorUtility.DisplayDialog(
                I18N.Tips,
                I18N.ExportAndroidAPK,
                I18N.Yes,
                I18N.No
            );

            if (yes)
            {
                BuildHelper.ExportAndroidAPK("master");
            }
        }

        [MenuItem(I18N.U3DMobile + I18N.ExportIOSProject, false, ExportPackageMenu)]
        internal static void ExportIOSProject()
        {
            bool yes = EditorUtility.DisplayDialog(
                I18N.Tips,
                I18N.ExportIOSProject,
                I18N.Yes,
                I18N.No
            );

            if (yes)
            {
                BuildHelper.ExportIOSProject("master");
            }
        }
    }
}
