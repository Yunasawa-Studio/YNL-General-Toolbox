#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using YNL.GeneralToolbox.Windows;

namespace YNL.GeneralToolbox.Settings
{
    public class ToolboxSettings : ScriptableObject
    {
        public WindowType CurrentWindow = WindowType.ImageResizing;

        public virtual void CreateSettings()
        {
        }
        public virtual void ApplySettings()
        {
        }
        public virtual void LoadSettings()
        {
            GetSettings();
            ApplySettings();
        }
        public virtual void SaveSettings()
        {
            CreateSettings();
            SetSettings();
        }
        public virtual void SetSettings()
        {
            EditorPrefs.SetInt("Current Toolbox Window", (int)CurrentWindow);
        }
        public virtual void GetSettings()
        {
            CurrentWindow = (WindowType)EditorPrefs.GetInt("Current Toolbox Window", 0);
        }
    }
}
#endif