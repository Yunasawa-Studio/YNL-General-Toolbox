#if !YNL_CREATOR
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;

namespace YNL.GeneralToolbox.Setups
{
    [InitializeOnLoad]
    public class Setups
    {
        public const string DependenciesKey = "YNL - General Toolbox | dependencies";

        private static ListRequest _request;
        public static (bool editor, bool utilities) Dependencies;

        static Setups()
        {
            EditorApplication.update += OnEditorApplicationUpdate;
        }

        private static void OnEditorApplicationUpdate()
        {
            EditorApplication.update -= OnEditorApplicationUpdate;

            _request = Client.List();
            while (!_request.IsCompleted) { }

            if (_request.Status == StatusCode.Success)
            {
                Dependencies = (false, false);

                IsPackageInstalled(_request.Result, "com.yunasawa.ynl.editor", ref Dependencies.editor);
                IsPackageInstalled(_request.Result, "com.yunasawa.ynl.utilities", ref Dependencies.utilities);
            }

            bool dependenciesResolver = EditorPrefs.GetBool(DependenciesKey);

            if ((!Dependencies.editor || !Dependencies.utilities) && !dependenciesResolver) Packages.ShowWindow();

            EditorPrefs.SetBool(DependenciesKey, true);

            EditorDefineSymbols.AddSymbols("YNL_GENERALTOOLBOX");
        }

        private static void IsPackageInstalled(PackageCollection packages, string name, ref bool checker)
        {
            if (packages == null) return;

            foreach (var package in packages)
            {
                if (package.name == name) checker = true;
            }
        }
    }
}
#endif