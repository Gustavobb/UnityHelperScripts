using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester2 : MonoBehaviour
{
    void Start()
    {
        Tester1.timeEvent.AddListener(LogPrint);
    }

    void LogPrint()
    {
        Debug.Log("Log 1");
    }
}
