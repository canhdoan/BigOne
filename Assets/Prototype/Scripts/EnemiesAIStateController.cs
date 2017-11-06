﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AI;

namespace StateMachine
{
    public class EnemiesAIStateController : StateController
    {

        [HideInInspector] public _AgentController m_AgentController;
        [HideInInspector] public float stateTimer = 0f;
        [HideInInspector] public CharacterInterface[] characterInterfaces;
        [HideInInspector] public GuardStates saveState;

        [Space(5)]
        [Header("States For Saving")]
        public State patrolState;
        public State checkNavPoint;

        protected override void Awake()
        {
            base.Awake();
            lastActiveState = currentState;
            m_AgentController = GetComponent<_AgentController>();
            inactiveState = (State)Resources.Load("InactiveAI");
        }

        private void Start()
        {
            characterInterfaces = GMController.instance.m_CharacterInterfaces;
        }

        public override void TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                currentState.OnExitState(this);
                currentState = nextState;
                currentState.OnEnterState(this);
                if (currentState != inactiveState)
                    lastActiveState = currentState;
                OnExitState();
            }
        }

        public override void Update()
        {
            base.Update();
            stateTimeElapsed += Time.deltaTime;

            if (!checkIfGameActive.Decide(this) && currentState != inactiveState)
            {
                TransitionToState(inactiveState);
            }
            else if (checkIfGameActive.Decide(this) && currentState == inactiveState)
            {
                TransitionToState(lastActiveState);
            }

            currentState.UpdateState(this);
        }

    }
}