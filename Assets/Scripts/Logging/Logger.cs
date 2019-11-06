using System.Collections.Generic;
using Control;

namespace Logging
{
    public static class Logger
    {
        private static readonly List<ActionEventInfo> LoggedEvents = new List<ActionEventInfo>();

        public static void LogAction(int originId, int targetId, Action action)
        {
            LoggedEvents.Add(new ActionEventInfo(originId, targetId, action.ToString(), action.GetValue()));
        }

        public static void ClearLog()
        {
            LoggedEvents.Clear();
        }
        
        public static List<ActionEventInfo> GetLoggedEvents()
        {
            return LoggedEvents;
        }
    }
}