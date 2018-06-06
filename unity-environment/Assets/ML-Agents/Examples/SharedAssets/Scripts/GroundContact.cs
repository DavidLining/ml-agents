﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  MLAgents
{
    /// <summary>
    /// This class contains logic for locomotion agents with joints which might make contact with the ground.
    /// By attaching this as a component to those joints, their contact with the ground can be used as either
    /// an observation for that agent, and/or a means of punishing the agent for making undesirable contact.
    /// </summary>
    [DisallowMultipleComponent]
    public class GroundContact : MonoBehaviour
    {
        // [HideInInspector]
        public Agent agent;

        [Header("Ground Check")]
        public bool agentDoneOnGroundContact; //reset agent on ground contact?
        public bool penalizeGroundContact; //if this body part touches the ground should the agent be penalized?
        public float groundContactPenalty; //penalty amount (ex: -1)
        public bool touchingGround;
        private const string Ground = "ground"; //tag on ground obj


        /// <summary>
        /// Check for collision with ground, and optionally penalize agent.
        /// </summary>
        void OnCollisionEnter(Collision col)
        {
            if (col.transform.CompareTag(Ground))
            {
                touchingGround = true;
                if (penalizeGroundContact)
                {
                    agent.AddReward(groundContactPenalty);
                }
                if (agentDoneOnGroundContact)
                {
                    agent.Done();
                }
            }
        }

        /// <summary>
        /// Check for end of ground collision and reset flag appropriately.
        /// </summary>
        void OnCollisionExit(Collision other)
        {
            if (other.transform.CompareTag(Ground))
            {
                touchingGround = false;
            }
        }
    }
}
