using System;
using System.Collections.Generic;
using System.IO;
using U3DMobile;
using UnityEditor;

namespace U3DMobileEditor
{
    internal static class BuildKeys
    {
        internal static string APKJKSFile      (string name) { return $"keystores/android/{name}.jks"        ; }
        internal static string APKJKSPassFile  (string name) { return $"keystores/android/{name}.jkspass.txt"; }
        internal static string APKKeyFile      (string name) { return $"keystores/android/{name}.key.txt"    ; }
        internal static string APKKeyPassFile  (string name) { return $"keystores/android/{name}.keypass.txt"; }

        internal static string IPAPrivKeyFile  (string name) { return $"keystores/ios/{name}.privkey.txt"    ; }
        internal static string IPAProvisionFile(string name) { return $"keystores/ios/{name}.mobileprovision"; }
    }

    internal class BuildArguments
    {
        //platform.
        public string targetPlatform;
        public string targetProduct ;

        //keys.
        public string apkKeystore   ;
        public string ipaProvision  ;

        //application information.
        public string appPackageId  ;
        public string appVersionStr ;
        public int    appVersionNum ;

        //game setting.
        public int    packageSerial ;
        public string firstLanguage ;
        public string storeChannel  ;
        public string channelGateway;
        public string assetURL      ;
        public string patchURL      ;

        public HashSet<string>            assetFlavors;
        public Dictionary<string, object> userFlags;

        //build settings.
        public int    bundleSerial  ;
        public bool   forceRebuild  ;
        public bool   usePastBundle ;
        public string currentCarry  ;
    }

    internal static class BuildEnvironment
    {
        internal static BuildArguments ParseEnvironment()
        {
            var args = new BuildArguments();

            args.targetPlatform = GetEnvString("_target_platform" , "");
            args.targetProduct  = GetEnvString("_target_product"  , "");

            args.apkKeystore    = GetEnvString("_apk_keystore"    , "");
            args.ipaProvision   = GetEnvString("_ipa_provision"   , "");

            args.appPackageId   = GetEnvString("_app_package_id"  , "com.enterprise.game");
            args.appVersionStr  = GetEnvString("_app_verison_str" , "1.0.0");
            args.appVersionNum  = GetEnvInt   ("_app_version_num" , 1);

            args.packageSerial  = GetEnvInt   ("_package_serial"  , 0     );
            args.firstLanguage  = GetEnvString("_first_language"  , ""    );
            args.storeChannel   = GetEnvString("_store_channel"   , ""    );
            args.channelGateway = GetEnvString("_channel_gateway" , ""    );
            args.assetURL       = GetEnvString("_asset_url"       , "none");
            args.patchURL       = GetEnvString("_patch_url"       , "none");
            args.assetFlavors   = GetStringSet("_asset_flavors"   );
            args.userFlags      = GetObjDict  ("_user_flags"      );

            args.bundleSerial   = GetEnvInt   ("_bundle_serial"   , 0    );
            args.forceRebuild   = GetEnvBool  ("_force_rebuild"   , false);
            args.usePastBundle  = GetEnvBool  ("_use_past_bundle" , true );
            args.currentCarry   = GetEnvString("_current_carry"   , ""   );

            Log.Group(() =>
            {
                Log.Info("_target_platform    : {0}", args.targetPlatform);
                Log.Info("_target_product     : {0}", args.targetProduct );
                Log.Info("_apk_keystore       : {0}", args.apkKeystore   );
                Log.Info("_ipa_provision      : {0}", args.ipaProvision  );
                Log.Info("_app_package_id     : {0}", args.appPackageId  );
                Log.Info("_app_verison_str    : {0}", args.appVersionStr );
                Log.Info("_app_version_num    : {0}", args.appVersionNum );
                Log.Info("_package_serial     : {0}", args.packageSerial );
                Log.Info("_first_language     : {0}", args.firstLanguage );
                Log.Info("_store_channel      : {0}", args.storeChannel  );
                Log.Info("_channel_gateway    : {0}", args.channelGateway);
                Log.Info("_asset_url          : {0}", args.assetURL      );
                Log.Info("_patch_url          : {0}", args.patchURL      );

                int flavorCount = args.assetFlavors.Count;
                int flavorIndex = 0;
                Log.Info("_asset_flavors count: {0}", flavorCount);
                foreach (string item in args.assetFlavors)
                {
                    Log.Info("_asset_flavors ({0}/{1}): {2}", ++flavorIndex, flavorCount, item);
                }

                int flagCount = args.userFlags.Count;
                int flagIndex = 0;
                Log.Info("_user_flags count   : {0}", flagCount);
                foreach (KeyValuePair<string, object> entry in args.userFlags)
                {
                    Log.Info("_user_flags ({0}/{1})   : {2}: {3}", ++flagIndex, flagCount, entry.Key, entry.Value);
                }

                Log.Info("_bundle_serial      : {0}", args.bundleSerial );
                Log.Info("_force_rebuild      : {0}", args.forceRebuild );
                Log.Info("_use_past_bundle    : {0}", args.usePastBundle);
                Log.Info("_current_carry      : {0}", args.currentCarry );
            });

            return args;
        }

        private static string GetEnvString(string name, string defaultValue)
        {
            string value = Environment.GetEnvironmentVariable(name);
            return !string.IsNullOrWhiteSpace(value) ? value.Trim() : defaultValue;
        }

        private static int GetEnvInt(string name, int defaultValue)
        {
            string value = Environment.GetEnvironmentVariable(name);
            try
            {
                return int.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static bool GetEnvBool(string name, bool defaultValue)
        {
            string value = Environment.GetEnvironmentVariable(name);
            try
            {
                return bool.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static HashSet<string> GetStringSet(string name)
        {
            var set = new HashSet<string>();

            string raw = Environment.GetEnvironmentVariable(name);
            if (string.IsNullOrWhiteSpace(raw))
            {
                return set;
            }

            string[] items = raw.Split(';');
            foreach (string item in items)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    set.Add(item.Trim());
                }
            }

            return set;
        }

        private static Dictionary<string, object> GetObjDict(string name)
        {
            var dict = new Dictionary<string, object>();

            string raw = Environment.GetEnvironmentVariable(name);
            if (string.IsNullOrWhiteSpace(raw))
            {
                return dict;
            }

            string[] entries = raw.Split(";");
            foreach (string entry in entries)
            {
                if (string.IsNullOrWhiteSpace(entry))
                {
                    continue;
                }

                string[] pair = entry.Split(":");
                if (pair.Length != 2                   ||
                    string.IsNullOrWhiteSpace(pair[0]) ||
                    string.IsNullOrWhiteSpace(pair[1]) )
                {
                    continue;
                }

                string key   = pair[0].Trim();
                string value = pair[1].Trim();

                if (bool.TryParse(value, out bool boolValue))
                {
                    dict[key] = boolValue;
                }
                else if (int.TryParse(value, out int intValue))
                {
                    dict[key] = intValue;
                }
                else
                {
                    dict[key] = value;
                }
            }

            return dict;
        }

        internal static void CheckEnvironment(BuildArguments args, List<string> errors)
        {
            //platform.
            if (args.targetPlatform == "android")
            {
                if (args.targetProduct != "aab"    &&
                    args.targetProduct != "apk"    &&
                    args.targetProduct != "bundle" )
                {
                    errors.Add("target product only can be 'aab', 'apk' or 'bundle' on android platform");
                }
            }
            else if (args.targetPlatform == "ios")
            {
                if (args.targetProduct != "ipa"    &&
                    args.targetProduct != "bundle" )
                {
                    errors.Add("target product only can be 'ipa' or 'bundle' on ios platform");
                }
            }
            else
            {
                errors.Add("target platform only can be 'android' or 'ios'");
            }

            //keys.
            if (args.targetProduct == "aab" || args.targetProduct == "apk")
            {
                if (!string.IsNullOrWhiteSpace(args.apkKeystore))
                {
                    string jksFile     = BuildKeys.APKJKSFile    (args.apkKeystore);
                    string jksPassFile = BuildKeys.APKJKSPassFile(args.apkKeystore);
                    string keyFile     = BuildKeys.APKKeyFile    (args.apkKeystore);
                    string keyPassFile = BuildKeys.APKKeyPassFile(args.apkKeystore);

                    if (!File.Exists(jksFile    )) { errors.Add($"not found jks file: {jksFile}"             ); }
                    if (!File.Exists(jksPassFile)) { errors.Add($"not found jks password file: {jksPassFile}"); }
                    if (!File.Exists(keyFile    )) { errors.Add($"not found key file: {keyFile}"             ); }
                    if (!File.Exists(keyPassFile)) { errors.Add($"not found key password file: {keyPassFile}"); }
                }
                else
                {
                    errors.Add("no apk keystore set");
                }
            }
            else if (args.targetProduct == "ipa")
            {
                if (!string.IsNullOrWhiteSpace(args.ipaProvision))
                {
                    string privKeyFile   = BuildKeys.IPAPrivKeyFile  (args.apkKeystore);
                    string provisionFile = BuildKeys.IPAProvisionFile(args.apkKeystore);

                    if (!File.Exists(privKeyFile  )) { errors.Add($"not found private key file: {privKeyFile}"); }
                    if (!File.Exists(provisionFile)) { errors.Add($"not found provision file: {provisionFile}"); }
                }
                else
                {
                    errors.Add("no ipa provision set");
                }
            }

            //application information.
            if (string.IsNullOrWhiteSpace(args.appPackageId))
            {
                errors.Add("application package id is empty");
            }
            if (string.IsNullOrWhiteSpace(args.appVersionStr))
            {
                errors.Add("application version string is empty");
            }
            if (args.appVersionNum <= 0)
            {
                errors.Add($"illegal application version number: {args.appVersionNum}");
            }

            //game settings:
            if (args.packageSerial <= 0)
            {
                errors.Add($"illegal package serial: {args.packageSerial}");
            }

            var gameOptions = AssetHelper.LoadScriptable<GameOptions>(GameOptions.SavedPath);

            if (!gameOptions.IsValidGameLanguage  (args.firstLanguage )) { errors.Add($"unknown first language: {args.firstLanguage}"  ); }
            if (!gameOptions.IsValidStoreChannel  (args.storeChannel  )) { errors.Add($"unknown store channel: {args.storeChannel}"    ); }
            if (!gameOptions.IsValidChannelGateway(args.channelGateway)) { errors.Add($"unknown channel gateway: {args.channelGateway}"); }
            if (!gameOptions.IsValidAssetURL      (args.assetURL      )) { errors.Add($"unknown asset url: {args.assetURL}"            ); }
            if (!gameOptions.IsValidPatchURL      (args.patchURL      )) { errors.Add($"unknown patch url: {args.patchURL}"            ); }

            if (!gameOptions.IsValidAssetFlavors(args.assetFlavors, out HashSet<string> illegalFlavors))
            {
                foreach (string item in illegalFlavors)
                {
                    errors.Add($"unknown asset flavor: {item}");
                }
            }

            var gameSetting = AssetHelper.LoadScriptable<GameSettings>(GameSettings.SavedPath);
            if (!gameSetting.IsValidUserFlags(args.userFlags, out HashSet<string> illegalFlags))
            {
                foreach (string item in illegalFlags)
                {
                    errors.Add($"illegal user flag: {item}");
                }
            }

            //build settings:
            if (args.bundleSerial <= 0)
            {
                errors.Add($"illegal bundle serial: {args.bundleSerial}");
            }

            var buildSettings = AssetHelper.LoadScriptable<BuildSettings>(BuildSettings.SavedPath);
            if (!buildSettings.IsValidCarry(args.currentCarry))
            {
                errors.Add($"illegal carry option: {args.currentCarry}");
            }
        }

        internal static void UpdateSettings(BuildArguments args)
        {
            //keys.
            if (args.targetProduct == "aab" ||
                args.targetProduct == "apk" )
            {
                string name = args.apkKeystore;

                string jksName = BuildKeys.APKJKSFile(name);
                string jksPass = File.ReadAllText(BuildKeys.APKJKSPassFile(name));
                string keyName = File.ReadAllText(BuildKeys.APKKeyFile    (name));
                string keyPass = File.ReadAllText(BuildKeys.APKKeyPassFile(name));

                PlayerSettings.Android.keystoreName = jksName ;
                PlayerSettings.Android.keystorePass = jksPass != null ? jksPass.Trim() : "";
                PlayerSettings.Android.keyaliasName = keyName != null ? keyName.Trim() : "";
                PlayerSettings.Android.keyaliasPass = keyPass != null ? keyPass.Trim() : "";
            }
            else if (args.targetProduct == "ipa")
            {
                //set signature information in the export xcode project.
            }

            //application information.
            if (args.targetPlatform == "android")
            {
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, args.appPackageId);
                PlayerSettings.Android.bundleVersionCode = args.appVersionNum;
            }
            else if (args.targetPlatform == "ios")
            {
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, args.appPackageId);
                PlayerSettings.iOS.buildNumber = string.Format("{0}", args.appVersionNum);
            }
            PlayerSettings.bundleVersion = args.appVersionStr;

            //game settings:
            var gameSettings = AssetHelper.LoadScriptable<GameSettings>(GameSettings.SavedPath);

            gameSettings.packageSerial  = args.packageSerial ;
            gameSettings.firstLanguage  = args.firstLanguage ;
            gameSettings.storeChannel   = args.storeChannel  ;
            gameSettings.channelGateway = args.channelGateway;
            gameSettings.assetURL       = args.assetURL      ;
            gameSettings.patchURL       = args.patchURL      ;

            gameSettings.SetAssetFlavors(args.assetFlavors);
            gameSettings.SetUserFlags(args.userFlags);

            EditorUtility.SetDirty(gameSettings);

            //build settings:
            var buildSettings = AssetHelper.LoadScriptable<BuildSettings>(BuildSettings.SavedPath);

            buildSettings.SetBundleSerial (args.bundleSerial );
            buildSettings.SetForceRebuild (args.forceRebuild );
            buildSettings.SetUsePastBundle(args.usePastBundle);
            buildSettings.SetCurrentCarry (args.currentCarry );

            EditorUtility.SetDirty(buildSettings);

            //NOTE: remember to save.
            AssetDatabase.SaveAssets();
        }

        internal static string GetOutputDirectory()
        {
            const string defaultPath = "BUILD/local";

            string path = GetEnvString("_output_dir", defaultPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
