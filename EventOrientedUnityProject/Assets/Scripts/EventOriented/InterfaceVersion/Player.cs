using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, EventListener {
    private bool _isMoving;
    public float speed = 10.0f;

	// Use this for initialization
	void Start () {
        EventManager.Instance.AddListener(EVENT_TYPE.MOVE, this);
	}
	
	// Update is called once per frame
	void Update () {
        if (_isMoving)
            Move();
	}

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
            
        }
    }

    void EventListener.OnEvent(EVENT_TYPE eventType, Component sender, Object param = null)
    {
        switch (eventType)
        {
            case EVENT_TYPE.MOVE:
                StopStart();
            break;
        }
    }

    public void StopStart()
    {
        if (_isMoving)
            IsMoving = false;
        else
            IsMoving = true;
    }

    public void Move()
    {
        transform.position += Time.deltaTime * speed * transform.forward;
    }
}
