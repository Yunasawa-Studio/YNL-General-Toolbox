#if UNITY_EDITOR && YNL_UTILITIES
using UnityEngine.UIElements;
using YNL.Editors.Visuals;
using YNL.Editors.Extensions;

namespace YNL.GeneralToolbox.Windows.ImageResizing
{
    public class EResizeExecutePanel : Button
    {
        private const string _styleSheet = "Style Sheets/Image Resizing/EResizeExecutePanel";

        public Button Execute;
        public StyledSwitch ReplaceOldImageSwitch;
        public Image ReplaceOldImagePanel;
        public StyledSwitch SaveAsACopySwitch;
        public Image SaveAsACopyPanel;

        public bool IsReplaceOldImage = true;

        public EResizeExecutePanel() : base()
        {
            this.AddStyle(_styleSheet, ESheet.Font).AddClass("Main");

            ReplaceOldImageSwitch = new StyledSwitch(false).AddClass("ReplaceOldImageSwitch");
            ReplaceOldImageSwitch.OnSwitch += (enable) => SwitchSaveType(enable);
            ReplaceOldImagePanel = new Image().AddClass("ReplaceOldImagePanel").AddElements(new Label("Replace old image").AddClass("Label"), ReplaceOldImageSwitch);

            SaveAsACopySwitch = new StyledSwitch(true).AddClass("SaveAsACopySwitch");
            SaveAsACopySwitch.OnSwitch += (enable) => SwitchSaveType(!enable);
            SaveAsACopyPanel = new Image().AddClass("SaveAsACopyPanel").AddElements(new Label("Save as a copy").AddClass("Label"), SaveAsACopySwitch);

            Execute = new Button().AddClass("Execute").SetText("Resize");

            this.AddElements(ReplaceOldImagePanel, SaveAsACopyPanel, Execute);

            SwitchSaveType(true);
        }

        public void SwitchSaveType(bool enable)
        {
            IsReplaceOldImage = enable;

            ReplaceOldImageSwitch.SetEnable(IsReplaceOldImage);
            SaveAsACopySwitch.SetEnable(!IsReplaceOldImage);
        }
    }
}
#endif