﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {
    public State state;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Think());
    }

    System.Collections.IEnumerator Think()
    {
        while (true)
        {
            state.Think();
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void ChangeState(State new_state)
    {
        Debug.Log(this.GetType().Name + " changed to: " + new_state.GetType().Name);
        if (state != null)
        {
            state.Exit();
        }
        state = new_state;
        state.fsm = this;
        state.Enter();
    }
}
