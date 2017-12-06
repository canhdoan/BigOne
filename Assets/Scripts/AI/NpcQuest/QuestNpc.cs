﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SaveGame;
using RootMotion.FinalIK;

namespace AI
{
    public class QuestNpc : AIAgent
    {
        LookAtIK lookAtComponent;
        public Transform lookAtTarget;
        [HideInInspector] public float headClamp;    // Reference to the maximum weight according to inspector values

        Transform eyes;
        // Saving Game
        //[HideInInspector] public GuardSaveComponent m_SaveComponent;

        public bool playerSaw = false;

        public void SetBlackboardValue(string valueName, int value)
        {
            m_Blackboard.SetIntValue(valueName, value);
        }

        public void SetBlackboardValue(string valueName, bool value)
        {
            m_Blackboard.SetBoolValue(valueName, value);
        }

        public void SetBlackboardValue(string valueName, Vector3 value)
        {
            m_Blackboard.SetVector3Value(valueName, value);
        }

        public int GetBlackboardIntValue(string valueName)
        {
            return 0;
        }

        public bool GetBlackboardBoolValue(string valueName)
        {
            return false;
        }

        public override void UpdateNavPoint()
        {
            throw new System.NotImplementedException();
        }

        public override void ReachNavPoint()
        {
            throw new System.NotImplementedException();
        }

        public void LookAtManager()
        {
            if(GetBlackboardBoolValue("lookAtPlayer"))
            {
                // Turn head speed
                if (lookAtComponent.solver.headWeight < headClamp)
                {
                    lookAtComponent.solver.headWeight += Time.deltaTime;
                }
            }
            else
            {
                lookAtComponent.solver.headWeight -= Time.deltaTime;
            }
        }

        private void Awake()
        {
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
            m_Animator = GetComponent<Animator>();
            m_Brain = GetComponent<Brain>();
            m_Brain.decisionMaker.m_Blackboard = new QuestNpcBlackboard();
            m_Blackboard = m_Brain.decisionMaker.m_Blackboard;
            m_Blackboard.m_Agent = this;
        }

        private void Start()
        {
            headClamp = lookAtComponent.solver.headWeight;
        }

        private void Update()
        {
            LookAtManager();
        }
    }

    
}

