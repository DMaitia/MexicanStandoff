using System;

namespace UnityScripts
{
    interface ICountdown
    {
        bool IsPaused { get; set; }
    
        DateTime StartDateTime { get; set; }
    
        DateTime StopDateTime { get; set; }
    
        TimeSpan TimeRemaining { get; set; }
    
        int SecondsBetweenActions { get; set; }
        void SetPause(bool paused);
    }
}