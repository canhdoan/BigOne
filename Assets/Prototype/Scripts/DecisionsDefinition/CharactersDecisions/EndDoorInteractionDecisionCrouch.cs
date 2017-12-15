﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace Character.Decisions
{
    [CreateAssetMenu(menuName = "Prototype/Decisions/Characters/EndDoorInteractionDecisionCrouch")]
    public class EndDoorInteractionDecisionCrouch : Decision
    {
        public override bool Decide(CharacterStateController controller)
        {
            bool isInteracting = CheckIfEndDoorInteraction(controller);
            return isInteracting;
        }

        private bool CheckIfEndDoorInteraction(CharacterStateController controller)
        {

            if (!controller.m_CharacterController.startDoorAction && !controller.m_CharacterController.isEndDoorAction && !controller.m_CharacterController.m_Animator.GetBool("isWalking"))
            {

                return true;


            }
            else
            {
                return false;
            }


        }


    }
}
