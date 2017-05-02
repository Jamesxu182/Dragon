using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {

    public StateMachine fsm;

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Think() { }
}
