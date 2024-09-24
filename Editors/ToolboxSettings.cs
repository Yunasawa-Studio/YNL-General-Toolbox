#if UNITY_EDITOR
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using YNL.Extensions.Methods;
using YNL.GeneralToolbox.Windows;

namespace YNL.GeneralToolbox.Settings
{
    public class ToolboxSettings : ScriptableObject
    {
        public WindowType CurrentWindow;

        public AnimationRepathingSettings AnimationRepathing = new();
        public string AnimationRepathingData = "";

        public void CreateSettings()
        {
            AnimationRepathingData = JsonConvert.SerializeObject(AnimationRepathing);
        }
        private void ApplySettings()
        {
            AnimationRepathing = JsonConvert.DeserializeObject<AnimationRepathingSettings>(AnimationRepathingData);
        }
        public void LoadSettings()
        {
            GetSettings();
            ApplySettings();
        }
        public void SaveSettings()
        {
            CreateSettings();
            SetSettings();
        }
        private void SetSettings()
        {
            EditorPrefs.SetString("Animation Repathing", AnimationRepathingData);
        }
        private void GetSettings()
        {
            AnimationRepathingData = EditorPrefs.GetString("Animation Repathing", JsonConvert.SerializeObject(AnimationRepathing));
        }
    }
}
#endif