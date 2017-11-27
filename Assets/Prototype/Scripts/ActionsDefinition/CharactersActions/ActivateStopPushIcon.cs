﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using StateMachine;

namespace Character.Actions
{
    [CreateAssetMenu(menuName = "Prototype/Actions/Characters/ActivateStopPush")]
    public class ActivateStopPushIcon : _Action
    {


        public override void Execute(CharacterStateController controller)
        {
            ActivateIcon(controller);
        }

        private void ActivateIcon(CharacterStateController controller)
        {
            controller.m_CharacterController.ShowStopPushIcon();
        }
    }
}
