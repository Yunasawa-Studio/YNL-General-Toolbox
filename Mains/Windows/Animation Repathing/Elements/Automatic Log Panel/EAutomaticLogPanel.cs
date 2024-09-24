#if UNITY_EDITOR
using UnityEngine.UIElements;
using YNL.Editors.Extensions;
using YNL.Editors.Visuals;

namespace YNL.GeneralToolbox.Windows.AnimationRepathing
{
    public class EAutomaticLogPanel : Button
    {
        private const string USS_StyleSheet = "Style Sheets/Animation Repathing/EAutomaticLogPanel";

        public Image TitleBackground;
        public Image TagIcon;
        public FlexibleLine Line;
        public Label Title;
        public Image ClearLog;
        public Button ClearLogButton;

        public ScrollView LogScroll;

        public EAutomaticLogPanel() : base()
        {
            this.AddStyle(USS_StyleSheet, ESheet.Font).SetName("Root");

            TagIcon = new Image().SetName("TagIcon");
            Line = new FlexibleLine(FlexibleLine.Line.Vertical).AddClass("Line");
            Title = new Label("Automatic log panel").SetName("Label");

            ClearLogButton = new Button().AddClass("ClearLogButton").SetText("Clear Logs");
            ClearLogButton.clicked += Visual.ClearLogPanel;
            ClearLog = new Image().AddClass("ClearLog").AddElements(ClearLogButton);

            TitleBackground = new Image().SetName("TitleBackground").AddElements(TagIcon, Line, Title, ClearLog);

            LogScroll = new ScrollView().SetName("LogScroll");

            this.AddElements(TitleBackground, LogScroll);
        }

        public void AddLogItem(EAutomaticLogLine line)
        {
            LogScroll.InsertElements(0, line);
            if (LogScroll.childCount > 10) LogScroll.Remove(LogScroll.ElementAt(LogScroll.childCount - 1));
        }

        public void ClearLogs() => LogScroll.Clear();

        public void ClearAllClipItem() => LogScroll.RemoveAllElements();
    }
}
#endif