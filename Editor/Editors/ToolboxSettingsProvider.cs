#if UNITY_EDITOR && YNL_UTILITIES
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Editors.Extensions;
using YNL.Editors.Visuals;
using YNL.Extensions.Addons;

namespace YNL.GeneralToolbox.Settings
{
    public static class ToolboxSettingsProvider
    {
        private static ToolboxSettings _settings;
        public static ToolboxSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = ScriptableObject.CreateInstance<ToolboxSettings>();
                    _settings.LoadSettings();
                    return _settings;
                }
                else return _settings;
            }
        }

        [SettingsProvider]
        public static SettingsProvider CreateMySettingsProvider()
        {
            var provider = new SettingsProvider("Preferences/▶ Yunasawa の Library/General Toolbox", SettingsScope.User)
            {
                label = "General Toolbox",
                activateHandler = (searchContext, rootElement) =>
                {
                    Settings.LoadSettings();

                    SerializedObject obj = new(Settings);
                    SerializedProperty currentWindow = obj.FindProperty("CurrentWindow");

                    RepaintedEnumField currentWindowField = new(currentWindow, "YNL.GeneralToolbox.Windows.WindowType");
                    currentWindowField.Enum.SetBorderRadius(0);
                    currentWindowField.Field.SetEnabled(false);

                    rootElement.SetPadding(5)
                        .AddElements(new StyledComponentHeader()
                        .SetGlobalColor("#FFFFFF")
                        .AddIcon("Textures/General/Editor Icon", MAddressType.Resources)
                        .AddTitle("General Toolbox")
                        .AddDocumentation("")
                        .AddBottomSpace(10))
                        .AddElements(currentWindowField);
                },
                keywords = new[] { "General", "Toolbox" }
            };

            return provider;
        }
    }
}
#endif