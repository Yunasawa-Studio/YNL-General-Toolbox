#if UNITY_EDITOR && YNL_UTILITIES
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Editors.Extensions;
using YNL.Editors.Visuals;
using YNL.Extensions.Addons;
using YNL.Extensions.Methods;
namespace YNL.GeneralToolbox.Settings
{
    public static class ToolboxSettingsProvider
    {
        public static ToolboxSettings Settings;

        [SettingsProvider]
        public static SettingsProvider CreateMySettingsProvider()
        {
            var provider = new SettingsProvider("Preferences/▶ Yunasawa の Library/General Toolbox", SettingsScope.User)
            {
                label = "General Toolbox",
                activateHandler = (searchContext, rootElement) =>
                {
                    if (Settings == null)
                    {
                        Settings = ScriptableObject.CreateInstance<ToolboxSettings>();
                        Settings.LoadSettings();
                    }

                    SerializedObject obj = new(Settings);
                    SerializedProperty animaionRepathing = obj.FindProperty("AnimationRepathingData");
                    SerializedProperty currentWindow = obj.FindProperty("CurrentWindow");

                    RepaintedTextField animationRepathingField = new(animaionRepathing);
                    animationRepathingField.Input.SetBorderRadius(0);
                    animationRepathingField.Field.RegisterValueChangedCallback(evt => Settings.SaveSettings());

                    RepaintedEnumField currentWindowField = new(currentWindow, "YNL.GeneralToolbox.Windows.WindowType");
                    currentWindowField.Enum.SetBorderRadius(0);
                    currentWindowField.Field.SetEnabled(false);

                    rootElement.SetPadding(5)
                        .AddElements(new StyledComponentHeader()
                        .SetGlobalColor("#FFFFFF")
                        .AddIcon("Textures/General/Editor Icon", MAddressType.Resources)
                        .AddTitle("General Toolbox (Pro)")
                        .AddDocumentation("")
                        .AddBottomSpace(10))
                        .AddElements(animationRepathingField, currentWindowField);
                },
                keywords = new[] { "General", "Toolbox" }
            };

            return provider;
        }
    }
}
#endif