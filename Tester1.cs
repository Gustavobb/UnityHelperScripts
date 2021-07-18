using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester1 : MonoBehaviour
{
    public static Event timeEvent;
    QuietTimer timer1;

    void Awake()
    {
        timeEvent = new Event();
    }

    void Start()
    {
        new QuietTimer(1f, 0f, this, FinishTimer).Execute();
        timer1 = new QuietTimer(4f, 0f, this, () => Debug.Log("Log 3"));
        timer1.Execute();
        timeEvent.AddListener(timer1.StopTimer);
        timeEvent.AddListener(new QuietTimer(2f, 0f, this, () => { Debug.Log("Log 2. Preparing log 3"); timer1.Execute();}).Execute);
    }
    
    void FinishTimer()
    {
        timeEvent.Invoke();
    }
}
