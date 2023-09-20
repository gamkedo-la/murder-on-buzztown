using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSM : StateMachine
{
    [HideInInspector] public BossIdle idleState;
    // [HideInInspector] public BossMoving movingState;
    [HideInInspector] public BossDashing dashingState;
    // [HideInInspector] public BossShooting shootingState;

    BossSM _sm;
    public Transform playerTransform;
    public bool movedLeftLast;
    // public Animator anim;

    private void Awake()
    {
        idleState = new BossIdle(this);
        dashingState = new BossDashing(this);
        UnityEngine.Debug.Log("start");
        // anim = GetComponent<Animator>();
        _sm = this;
        movedLeftLast = false;
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}

