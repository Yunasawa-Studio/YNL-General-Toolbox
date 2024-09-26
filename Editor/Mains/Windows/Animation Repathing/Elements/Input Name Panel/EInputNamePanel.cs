#if UNITY_EDITOR
using UnityEngine.UIElements;
using YNL.Editors.Extensions;
using YNL.Editors.Visuals;

namespace YNL.GeneralToolbox.Windows.AnimationRepathing
{
    public class EInputNamePanel : Button
    {
        private const string USS_StyleSheet = "Style Sheets/Animation Repathing/EInputNamePanel";

        public Label OriginLabel;
        public TextField OriginField;
        public Label NewLabel;
        public TextField NewField;

        public Image TitleBackground;
        public Image TagIcon;
        public FlexibleLine Line;
        public Label Title;

        public FlexibleInteractButton SwapButton;
        public Image SwapIcon;

        public EInputNamePanel() : base()
        {
            this.AddStyle(USS_StyleSheet, ESheet.Font).SetName("Root");

            TagIcon = new Image().SetName("TagIcon");
            Line = new FlexibleLine(FlexibleLine.Line.Vertical).AddClass("Line");
            Title = new Label("Rename/Swap [Original Root] with [New Root]").SetName("Label");
            TitleBackground = new Image().SetName("TitleBackground").AddElements(TagIcon, Line, Title);

            OriginLabel = new Label("Original Root").SetName("OriginLabel");
            NewLabel = new Label("New Root").SetName("NewLabel");
            OriginField = new TextField().AddClass("GeneralField").SetName("OriginField");
            NewField = new TextField().AddClass("GeneralField").SetName("NewField");

            SwapIcon = new Image().AddClass("SwapIcon");
            SwapButton = new FlexibleInteractButton().AddClass("SwapButton").AddElements(SwapIcon);
            SwapButton.OnPointerEnter += () => HoverOnSwapButton(true);
            SwapButton.OnPointerExit += () => HoverOnSwapButton(false);

            this.AddElements(TitleBackground, OriginLabel, NewLabel, OriginField, NewField, SwapButton);

            OriginField.RegisterValueChangedCallback(OnTextChange);
        }

        private void OnTextChange(ChangeEvent<string> evt)
        {

        }

        private void HoverOnSwapButton(bool isHover)
        {
            SwapButton.EnableClass(isHover, "SwapButton".EHover());
            SwapIcon.EnableClass(isHover, "SwapIcon".EHover());
        }
    }
}
#endif