using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public delegate void Listeners();
    protected Listeners listeners;

    public void AddListener(Listeners listener) { if (listener != null) listeners += listener; }
    public void ClearListeners() { listeners = null; }
    public void Invoke() { if (listeners != null) listeners(); }
}