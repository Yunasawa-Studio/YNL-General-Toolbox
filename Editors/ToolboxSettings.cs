#if UNITY_EDITOR
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using YNL.GeneralToolbox.Windows;

namespace YNL.GeneralToolbox.Settings
{
    public class ToolboxSettings : ScriptableObject
    {
        public WindowType CurrentWindow;

        public void CreateSettings()
        {
        }
        private void ApplySettings()
        {
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
            EditorPrefs.SetInt("Current Toolbox Window", (int)CurrentWindow);
        }
        private void GetSettings()
        {
            CurrentWindow = (WindowType)EditorPrefs.GetInt("Current Toolbox Window", 0);
        }
    }
}
#endif