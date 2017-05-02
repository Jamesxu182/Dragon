using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidFollowState : State {
    public override void Enter()
    {
        fsm.GetComponent<FollowPathBehaviour>().enabled = true;
        fsm.GetComponent<FloatWanderBehaviour>().enabled = false;
    }

    public override void Exit()
    {
        fsm.GetComponent<FollowPathBehaviour>().enabled = false;
        fsm.GetComponent<FloatWanderBehaviour>().enabled = true;
    }
}
