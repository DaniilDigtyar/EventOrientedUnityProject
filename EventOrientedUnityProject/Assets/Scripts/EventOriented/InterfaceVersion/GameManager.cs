using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance = null; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EventManager.Instance.PostNotification(EVENT_TYPE.MOVE, this);
    }
}
