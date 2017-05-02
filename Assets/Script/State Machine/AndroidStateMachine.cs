using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidStateMachine : StateMachine {

    private FollowPathBehaviour follow_behaviour;

    // Use this for initialization
    void Start()
    {
        follow_behaviour = GetComponent<FollowPathBehaviour>();

        ChangeState(new AndroidWanderState());
    }

    // Update is called once per frame
    void Update()
    {
        if(this.state is AndroidWanderState)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ChangeState(new AndroidFollowState());
            }
        }

        else if(this.state is AndroidFollowState)
        {
            if(follow_behaviour && follow_behaviour.IsEnd())
            {
                ChangeState(new AndroidWanderState());
            }
        }
    }
}
