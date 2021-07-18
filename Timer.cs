using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: Unscaled time 
public class Timer
{
    public delegate void TimerFinishDelegate();
    protected TimerFinishDelegate onFinishDelegate;
    public MonoBehaviour monoBehaviourObj;
    protected Coroutine timerRoutine;
    Timer repeatTimer;

    public float time, repeatAfter, timerProgress, timerQuantity;

    public Timer(float t, float repeat, MonoBehaviour mono, TimerFinishDelegate finishFunc) 
    { 
        time = t; 
        onFinishDelegate = finishFunc;
        monoBehaviourObj = mono;
        repeatAfter = repeat;

        if (repeatAfter > 0f) repeatTimer = new Timer(repeatAfter, 0f, monoBehaviourObj, () => Execute());
    }

    public virtual void Execute() 
    { 
        if (timerRoutine != null) monoBehaviourObj.StopCoroutine(timerRoutine);
        timerRoutine = monoBehaviourObj.StartCoroutine(StartTimer());
    }

    public virtual void StopTimer()
    {
        if (timerRoutine != null) 
        {
            monoBehaviourObj.StopCoroutine(timerRoutine);
            timerRoutine = null;
        }
    }

    public void ResetTimer()
    {
        StopTimer();
        timerProgress = 0f;
        timerQuantity = 0f;
        timerRoutine = null;
    }

    protected void TimerEnded()
    {
        ResetTimer();
        if (repeatTimer != null) repeatTimer.Execute();
        onFinishDelegate();
    }

    protected void UpdateTimerProgress()
    {
        timerQuantity += Time.deltaTime;
        timerProgress = timerQuantity / time;
    }
    
    protected virtual IEnumerator StartTimer() 
    {
        while (timerProgress < 1f)
        {
            UpdateTimerProgress();
            yield return null;
        }

        TimerEnded();
    }
}

// also useful to create a timer with timeScale = 0
public class LoudTimer : Timer
{
    public delegate void TimerUpdateDelegate();
    protected TimerUpdateDelegate updateDelegate;

    public LoudTimer(float t, float repeat, MonoBehaviour mono, TimerFinishDelegate finishFunc, TimerUpdateDelegate updateFunc) : base(t, repeat, mono, finishFunc)
    {
        updateDelegate = updateFunc;
    }
    
    protected override IEnumerator StartTimer()
    {
        while (timerProgress < 1f)
        {
            UpdateTimerProgress();
            updateDelegate();
            yield return null;
        }

        TimerEnded();
    }
}

// simplest timer possible, no need for extra variables
public class QuietTimer : Timer
{
    public QuietTimer(float t, float repeat, MonoBehaviour mono, TimerFinishDelegate finishFunc) : base(t, repeat, mono, finishFunc) {}
}

// step timer for controlled behaviours
public class StepTimer : Timer
{
    public StepTimer(float t, float repeat, MonoBehaviour mono, TimerFinishDelegate finishFunc) : base(t, repeat, mono, finishFunc) {}

    public override void Execute() 
    {
        UpdateTimerProgress();
        if (timerProgress >= 1f) TimerEnded();
    }

    public override void StopTimer() { return; }
}