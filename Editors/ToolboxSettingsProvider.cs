#if UNITY_EDITOR && YNL_UTILITIES
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Editors.Extensions;
using YNL.Editors.Visuals;
using YNL.Extensions.Addons;

namespace YNL.GeneralToolbox.Settings
{
    static class MySettingsProvider
    {
        private static ToolboxSettings settings;

        [SettingsProvider]
        public static SettingsProvider CreateMySettingsProvider()
        {
            var provider = new SettingsProvider("Preferences/▶ Yunasawa の Library/General Toolbox", SettingsScope.User)
            {
                label = "General Toolbox",
                activateHandler = (searchContext, rootElement) =>
                {
                    if (settings == null)
                    {
                        settings = ScriptableObject.CreateInstance<ToolboxSettings>();
                        LoadSettings();
                    }

                    SerializedObject obj = new(settings);
                    SerializedProperty property = obj.FindProperty("Test");

                    RepaintedTextField textField = new(property);
                    textField.Field.RegisterValueChangedCallback(evt => SaveSettings());

                    rootElement.SetPadding(5)
                        .AddElements(new StyledComponentHeader()
                        .SetGlobalColor("#FFFFFF")
                        .AddIcon("Textures/Windows/Utilities Center/Editor Icon", MAddressType.Resources)
                        .AddTitle("General Toolbox")
                        .AddDocumentation("")
                        .AddBottomSpace(10))
                        .AddElements(textField);
                },
                keywords = new[] { "General", "Toolbox" }
            };

            return provider;
        }

        private static void SaveSettings()
        {
            EditorPrefs.SetString("Test", settings.Test);
        }

        private static void LoadSettings()
        {
            settings.Test = EditorPrefs.GetString("Test", "");
        }
    }
}
#endif