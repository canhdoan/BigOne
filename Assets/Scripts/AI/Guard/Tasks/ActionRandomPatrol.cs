﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BT
{
    public class ActionRandomPatrol : Task
    {
        private Vector3 randomDestination;

        public override TaskState Run()
        {
            
            m_BehaviourTree.m_Blackboard.m_Agent.GetComponent<Guard>().GetRandomPoint(out randomDestination);
            //m_BehaviourTree.m_Blackboard.m_Agent.m_NavMeshAgent.destination = randomDestination;
            Debug.Log("Random pos " + randomDestination);
            m_BehaviourTree.m_Blackboard.SetVector3Value("LastPercievedPosition", randomDestination); 

           return TaskState.SUCCESS;
        }
    }
}