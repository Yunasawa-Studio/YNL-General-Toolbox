using System.Collections.Generic;
using System;
using YNL.GeneralToolbox.Settings;
using YNL.Extensions.Methods;

namespace YNL.GeneralToolbox.Windows.AnimationRepathing
{
    public class Variable
    {
        //public static ToolboxSettings ToolboxSettings => ToolboxSettingsProvider.Settings;

        //public static bool IsAutomaticPanel = false;
        //public static bool IsAutomaticOn
        //{
        //    get
        //    {
        //        if (ToolboxSettings.IsNull()) return false;
        //        else return ToolboxSettings.AnimationRepathing.IsAutomaticOn;
        //    }
        //    set
        //    {
        //        if (ToolboxSettings.IsNull()) return;
        //        ToolboxSettings.AnimationRepathing.IsAutomaticOn = value;
        //        ToolboxSettings.SaveSettings();
        //    }
        //}
        //public static List<AnimationRepathingSettings.AutomaticLog> AutomaticLogs => ToolboxSettings.AnimationRepathing?.AutomaticLogs;

        //public static Action OnModeChanged;

        //public static void SaveSettings() => ToolboxSettings?.SaveSettings();


        public static Action<Visual, Handler> OnVisualCreated;

        public static Action OnPropertyPanelExpanded;
        public static Action OnPropertyPanelCollapsed;
    }
}