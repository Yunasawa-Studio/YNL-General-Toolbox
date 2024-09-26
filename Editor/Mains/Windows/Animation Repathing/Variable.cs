using System;

namespace YNL.GeneralToolbox.Windows.AnimationRepathing
{
    public class Variable
    {
        public static Action<Visual, Handler> OnVisualCreated;

        public static Action OnPropertyPanelExpanded;
        public static Action OnPropertyPanelCollapsed;

        public static Action OnTagPanelExpanded;
        public static Action OnTagPanelCollapsed;
    }
}