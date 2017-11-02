﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace Character.Actions
{
    [CreateAssetMenu(menuName = "Prototype/Actions/Characters/Animator/SetForwardCrouch")]
    public class AnimSetForwardCrouch : _Action
    {

        public override void Execute(CharacterStateController controller)
        {
            SetForwardAmount(controller);
        }

        private void SetForwardAmount(CharacterStateController controller)
        {
            controller.m_CharacterController.m_Animator.SetFloat("Forward", controller.m_CharacterController.m_ForwardAmount, 0.1f, Time.deltaTime);
            //Debug.Log(controller.m_CharacterController.m_ForwardAmount);
        }
    }
}
