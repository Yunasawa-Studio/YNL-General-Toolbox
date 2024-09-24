using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace YNL.GeneralToolbox.Settings
{
    public class AnimationRepathingSettings
    {
        public enum Event { RenameSingle, RenameMultiple, MoveSucceed, MoveConflict, MoveOutbound, Destroy }
        public struct AutomaticLog
        {
            public Event Event;
            public bool IsSucceeded;
            public string CurrentTime;
            public string[] OldNames;
            public string[] NewNames;

            public AutomaticLog(Event @event, bool isSucceeded, string[] oldNames, string[] newNames)
            {
                Event = @event;
                IsSucceeded = isSucceeded;
                string original = DateTime.Now.ToString();
                int spaceIndex = original.IndexOf(' ');
                CurrentTime = $"<color=#96ffdc>{original.Substring(0, spaceIndex)}</color>" + "\n" + original.Substring(spaceIndex + 1);
                OldNames = oldNames;
                NewNames = newNames;
            }
        }

        public bool IsAutomaticOn = false;
        public List<AutomaticLog> AutomaticLogs = new();
    }
}