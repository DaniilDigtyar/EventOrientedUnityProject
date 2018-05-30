using System.Collections;
using UnityEngine;

public enum EVENT_TYPE
{
    MOVE,
    EVENT_2
}
public interface EventListener {
    void OnEvent(EVENT_TYPE eventType, Component sender, Object param = null);
}
