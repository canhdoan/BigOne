﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[CreateAssetMenu(menuName = "Prototype/CharactersActions/Walk")]
public class WalkAction : _Action
{

    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private Vector3 m_GroundNormal;
    private float m_TurnAmount;
    private float m_ForwardAmount;
    private bool m_IsGrounded;

    public override void Execute(StateController controller)
    {
        Walk(controller);
    }

    private void Walk(StateController controller)
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        // calculate move direction to pass to character
        if (controller.characterObj.m_Camera != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(controller.characterObj.m_Camera.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * controller.characterObj.m_Camera.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }

        //Move(m_Move, controller);
    }

    //public void Move(Vector3 move, StateController controller)
    //{

    //    // convert the world relative moveInput vector into a local-relative
    //    // turn amount and forward amount required to head in the desired
    //    // direction.
    //    if (move.magnitude > 1f) move.Normalize();
    //    move = controller.characterObj.CharacterTansform.InverseTransformDirection(move);
    //    CheckGroundStatus();
    //    move = Vector3.ProjectOnPlane(move, m_GroundNormal);
    //    m_TurnAmount = Mathf.Atan2(move.x, move.z);
    //    m_ForwardAmount = move.z;

    //    ApplyExtraTurnRotation();

    //    // control and velocity handling is different when grounded and airborne:
    //    if (m_IsGrounded)
    //    {
    //        HandleGroundedMovementWalk();
    //    }
    //    else
    //    {
    //        HandleAirborneMovement();
    //    }

    //}

    //void HandleGroundedMovement(bool crouch, bool jump)
    //{
    //    // check whether conditions are right to allow a jump:
    //    if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
    //    {
    //        // jump!
    //        m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
    //        m_IsGrounded = false;
    //        m_Animator.applyRootMotion = false;
    //        m_GroundCheckDistance = 0.1f;
    //    }
    //}



//    void CheckGroundStatus()
//    {
//        RaycastHit hitInfo;
//#if UNITY_EDITOR
//        // helper to visualise the ground check ray in the scene view
//        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
//#endif
//        // 0.1f is a small offset to start the ray from inside the character
//        // it is also good to note that the transform position in the sample assets is at the base of the character
//        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
//        {
//            m_GroundNormal = hitInfo.normal;
//            m_IsGrounded = true;
//            m_Animator.applyRootMotion = true;
//        }
//        else
//        {
//            m_IsGrounded = false;
//            m_GroundNormal = Vector3.up;
//            m_Animator.applyRootMotion = false;
//        }
//    }
}
