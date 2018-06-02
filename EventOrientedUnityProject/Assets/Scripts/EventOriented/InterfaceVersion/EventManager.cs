using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    private static EventManager instance = null;
    private Dictionary<EVENT_TYPE, List<EventListener>> listeners = new Dictionary<EVENT_TYPE, List<EventListener>>();

    public static EventManager Instance
    {
        get { return instance; }
        set { }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(gameObject);
    }

    public void AddListener(EVENT_TYPE eventType, EventListener listener)
    {
        List<EventListener> listenList = null;

        if(listeners.TryGetValue(eventType,out listenList))
        {
            listenList.Add(listener);
            return;
        }

        listenList = new List<EventListener>();
        listenList.Add(listener);
        listeners.Add(eventType, listenList);
    }

    public void PostNotification(EVENT_TYPE eventType, Component sender, Object param = null)
    {
        List<EventListener> listenList = null;
        int i;

        if (!listeners.TryGetValue(eventType, out listenList))
            return;

        for(i = 0; i < listenList.Count; i++)
        {
            if (!listenList[i].Equals(null))
                listenList[i].OnEvent(eventType, sender, param);
        }
    }
    
    public void RemoveEvent(EVENT_TYPE eventType)
    {
        listeners.Remove(eventType);
    }

    public void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<EventListener>> TmpListeners = new Dictionary<EVENT_TYPE, List<EventListener>>();

        foreach (KeyValuePair<EVENT_TYPE, List<EventListener>> Item in listeners)
        {
            for(int i = Item.Value.Count-1; i >= 0; i--)
            {
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            }
            if (Item.Value.Count > 0)
                TmpListeners.Add(Item.Key, Item.Value);
        }
        listeners = TmpListeners;
    }

    private void OnLevelWasLoaded(int level)
    {
        RemoveRedundancies();
    }

}
