#if UNITY_EDITOR && !YNL_CREATOR
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace YNL.GeneralToolbox.Setups
{
    public class Setups : AssetPostprocessor
    {
        private static ListRequest _request;

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var inPackages = importedAssets.Any(path => path.StartsWith("Packages/")) ||
                deletedAssets.Any(path => path.StartsWith("Packages/")) ||
                movedAssets.Any(path => path.StartsWith("Packages/")) ||
                movedFromAssetPaths.Any(path => path.StartsWith("Packages/"));

            if (inPackages)
            {
                InitializeOnLoad();
            }
        }

        [InitializeOnLoadMethod]
        private static void InitializeOnLoad()
        {
            _request = Client.List();
            EditorApplication.update += OnEditorApplicationUpdate;
            EditorDefineSymbols.AddSymbols("YNL_GENERALTOOLBOX");
        }

        private static void OnEditorApplicationUpdate()
        {
            EditorApplication.update -= OnEditorApplicationUpdate;

            TryInstallPackage(Client.List().Result, "com.yunasawa.ynl.editor", "https://github.com/Yunasawa-Studio/YNL-Editor.git", "2.0.5");
        }

        private static void TryInstallPackage(PackageCollection packages, string name, string url, string version)
        {
            foreach (var package in packages)
            {
                if (package.name == name)
                {
                    if (IsNewerThan(version, package.version))
                    {
                        Client.Add($"{url}#{version}");
                        return;
                    }
                    else return;
                }
            }

            Client.Add($"{url}#{version}");
        }
        public static bool IsNewerThan(string currentVersion, string newVersion)
        {
            var currentParts = currentVersion.Split('.');
            var newParts = newVersion.Split('.');

            for (int i = 0; i < Math.Max(currentParts.Length, newParts.Length); i++)
            {
                int currentPart = i < currentParts.Length ? int.Parse(currentParts[i]) : 0;
                int newPart = i < newParts.Length ? int.Parse(newParts[i]) : 0;

                if (newPart > currentPart) return true;
                else if (newPart < currentPart) return false;
            }

            return false;
        }
    }
}
#endif