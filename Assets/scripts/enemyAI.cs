// =====================================================================================
// Script: enemyAI
// Copyright (c) 2016, Golden Iota Gaming - All rights reserved.
//    Unauthorized copying of this file via any medium is strictly prohibited.
//    All contents within are closed-source, i.e. proprietary and confidential.
// Author(s): Erik Chase, Kody Dibble
// Date Created: 24 June 2016
// Description: Generic enemy AI designed to follow and attack player (when player is
//    within a certain range). 
// NOTE: Think of MMO enemy behavior with aggro mechanics? One of the behaviours exhibited by enemy AI.
// =====================================================================================

using UnityEngine;
using System.Collections;


public class Enemy_AI : MonoBehaviour {

    #region VARS

	public float frotspeed = 3.0f;              // Rotational velocity.
	public float fmovspeed = 3.0f;              // Displacement velocity.

    public bool playerInAggroRange = false;     // Flag that turns "on" when player entity is within range 
    public float aggroRange = 10.0f;            // Radius which player entity must enter in order to trigger enemy AI behaviour.
    public Vector3 baseLocation;                // Coordinates of this entity's base/return location when not triggered by player.
                                                   // Usually the very point of spawn.

    private Transform tr_player;               // Player Entity -- for accessing player's "Transform" component.

    #endregion

    #region UNITY_FUNCS

    // DESC: Start ===========================================================================================
    // Name  : Start
    // Params: n/a
    // Descr : Unity function. Upon game start, this gameObject will initialize with this function.
    // =======================================================================================================
    void start()
    {

	tr_player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // DESC: Update ==========================================================================================
    // Name  : Update
    // Params: n/a
    // Descr : Unity function. This is called once per frame during gameplay.
    // =======================================================================================================
    void update()
    {
        // 1. Check if player is within aggro range.
        if (isWithinAggroRange())
        {
            faceTowardsPlayer();
            moveTowardsPlayer();
        }
        
    }

    #endregion

    #region GETS
    // empty, no funcs yet
    #endregion

    #region SETS
    // empty, no funcs yet
    #endregion

    #region MISC_FUNCS

    // DESC: isWithinAggroRange===============================================================================
    // Name  : isWithinAggroRange
    // Params: n/a
    // Return: bool
    // Descr : Assigns this entity to "wait" for player to enter the radius of aggro.
    // =======================================================================================================
    private bool isWithinAggroRange()
    {
        // 1. Is the player entity in aggro range of this entity?
        if (Vector3.Distance(this.transform.position, tr_player.transform.position) < aggroRange)
            return true;
        else
            return false;
    }

    // DESC: faceTowardsPlayer================================================================================
    // Name  : faceTowardsPlayer
    // Params: n/a
    // Descr : Rotates this entity so its "front-side" faces toward aggro-ing player entity.
    // =======================================================================================================
    private void faceTowardsPlayer()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(tr_player.position - transform.position), (frotspeed * Time.deltaTime));
    }

    // DESC: moveTowardsPlayer================================================================================
    // Name  : moveTowardsPlayer
    // Params: n/a
    // Descr : Displaces this entity so its "front-side" moves toward aggro-ing player entity.
    // =======================================================================================================
    private void moveTowardsPlayer()
    {
        transform.position += transform.forward * fmovspeed * Time.deltaTime;
    }

    #endregion
}

