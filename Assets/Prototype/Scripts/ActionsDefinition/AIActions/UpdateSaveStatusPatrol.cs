﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using SaveGame;

namespace AI.Actions
{
    [CreateAssetMenu(menuName = "Prototype/AIActions/UpdateSaveStatus")]
    public class UpdateSaveStatusPatrol : _Action
    {

        public override void Execute(EnemiesAIStateController controller)
        {
            UpdateState(controller);
        }

        private void UpdateState(EnemiesAIStateController controller)
        {
            controller.m_AgentController.m_SaveComponent.activeState = GuardSaveComponent.GuardStates.Patrol;

        }
    }
}
