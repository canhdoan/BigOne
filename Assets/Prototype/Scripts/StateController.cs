﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class StateController : MonoBehaviour {

    public State currentState;
    public State inactiveState, gameStartState;
    public State remainState;
    public Decision checkIfGameActive;

    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public State lastActiveState;

    protected bool isActive = true;

    // Update is called once per frame
    public virtual void Update()
    {
    }

    public virtual void TransitionToState(State nextState)
    {
    }

    protected void OnExitState()
    {
        stateTimeElapsed = 0;
    }

}
