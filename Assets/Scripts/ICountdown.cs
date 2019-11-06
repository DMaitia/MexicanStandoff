using System;

interface ICountdown
{
    bool IsPaused { get; set; }
    
    DateTime StartDateTime { get; set; }
    
    DateTime StopDateTime { get; set; }
    
    TimeSpan TimeRemaining { get; set; }
    
    void SetPause(bool paused);
}