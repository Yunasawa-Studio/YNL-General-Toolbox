#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Toolbars;
using YNL.Editors.Extensions;
using YNL.Extensions.Methods;

namespace YNL.GeneralToolbox.Windows.AnimationRepathing
{
    [InitializeOnLoad]
    public static class Toolbar
    {
        private const string _styleSheet = "Style Sheets/Animation Repathing/Toolbar";

        public static EditorToolbarButton Button;

        static Toolbar()
        {
            EditorApplication.update += OnEditorUpdate;

            Variable.OnModeChanged -= UpdateButton;
            Variable.OnModeChanged += UpdateButton;
        }

        private static void OnEditorUpdate()
        {
            EditorApplication.update -= OnEditorUpdate;

            var rightToolbar = EEditor.Editor.GetToolbarZoneRight().AddStyle(_styleSheet);

            Button = new EditorToolbarButton(EnableAOR);
            Button.AddClass("Button").SetBackgroundImageTintColor(Variable.IsAutomaticOn ? "#52ffa8" : "#ff5252"); ;

            rightToolbar.Add(Button);
            
            void EnableAOR()
            {
                Variable.IsAutomaticOn = !Variable.IsAutomaticOn;
                Variable.OnModeChanged?.Invoke();

                Visual.UpdateAutomatic(false);

                UpdateButton();
            }
        }

        private static void UpdateButton()
        {
            Button.SetTooltip(Variable.IsAutomaticOn ? "Animation Object Renamer: On" : "Animation Object Renamer: Off");
            Button.SetBackgroundImageTintColor(Variable.IsAutomaticOn ? "#52ffa8" : "#ff5252");
        }
    }
}
#endif