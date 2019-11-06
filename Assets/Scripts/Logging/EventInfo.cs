using System;

namespace Logging
{
    public abstract class EventInfo
    {
    }
    
    public class ActionEventInfo : EventInfo
    {
        public int originId;
        public int targetId;
        public String action;
        public int value;
        
        public ActionEventInfo(int originId, int targetId, String action, int value) : base() 
        {
            this.originId = originId;
            this.targetId = targetId;
            this.action = action;
            this.value = value;
        }
    }
}