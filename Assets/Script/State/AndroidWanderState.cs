using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidWanderState : State {
    public override void Enter()
    {
        fsm.GetComponent<FloatWanderBehaviour>().enabled = true;
        fsm.GetComponent<FollowPathBehaviour>().enabled = false;
    }

    public override void Exit()
    {
        fsm.GetComponent<FloatWanderBehaviour>().enabled = false;
        fsm.GetComponent<FollowPathBehaviour>().enabled = true;
    }
}
