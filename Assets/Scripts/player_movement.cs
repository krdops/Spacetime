// =====================================================================================
// Script: player_movement
// Copyright (c) 2016, Golden Iota Gaming - All rights reserved.
//    Unauthorized copying of this file via any medium is strictly prohibited.
//    All contents within are closed-source, i.e. proprietary and confidential.
// Author(s): Erik Chase, Kody Dibble
// Date Created: 1 July 2016
// Description: Moves the player entity based on control input and any logical restrictions.
// NOTE: Control input is referenced from "player_controls.cs"
// =====================================================================================

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(player_controls))]

public class player_movement : MonoBehaviour {

    #region VARS
    private player_controls controls;
    private Transform transf;
    private Rigidbody rigidb;
    public Vector3 velocity;

    public float min_x = -.75f;
    public float max_x = .75f;
    public float speed_x = 7.5f;
    public float min_y = 10f;
    public float max_y = 10f;
    public float speed_y = 10f;

    #endregion

    #region UNITY_FUNCS
    // DESC: Start ===========================================================================================
    // Name  : Start
    // Params: n/a
    // Descr : Unity function. Upon game start, this gameObject will initialize with this function.
    // =======================================================================================================
    void Start()
    {
        // 1. Initialize link to "player_controls" script.
        controls = this.GetComponent<player_controls>();

        // 2. Initialize link to "Transform" component.
        transf = this.GetComponent<Transform>();

        // 3. Initialize link to "Rigidbody" component.
        rigidb = this.GetComponent<Rigidbody>();
    }

    // DESC: FixedUpdate =====================================================================================
    // Name  : FixedUpdate
    // Params: n/a
    // Descr : Unity function. This is called every time interval (determined by Unity).
    // =======================================================================================================
    void FixedUpdate()
    {

        // 1. Calculate velocity.
        calc_velocity();

        // 2. Apply velocity.
        apply_velocity();

    }
    #endregion

    #region GETS
    #endregion

    #region SETS
    #endregion

    #region MISC_FUNCS

    // DESC: apply_velocity ==================================================================================
    // Name  : apply_velocity
    // Params: n/a
    // Descr : Applies movement to player entity.
    // =======================================================================================================
    private void apply_velocity()
    {
        move_x();
    }

    // DESC: calc_velocity ===================================================================================
    // Name  : calc_velocity
    // Params: n/a
    // Descr : Interface for calculating movement.
    // =======================================================================================================
    private void calc_velocity()
    {
        calc_rotation();
        calc_x();
        calc_y();
    }

    // DESC: calc_rotation ===================================================================================
    // Name  : calc_rotation
    // Params: n/a
    // Descr : Calculates rotational velocity with read rotational input.
    // =======================================================================================================
    private void calc_rotation()
    {

    }

    // DESC: calc_x ==========================================================================================
    // Name  : calc_x
    // Params: n/a
    // Descr : Calculates movement with read x-axis input.
    // =======================================================================================================
    private void calc_x()
    {
        // 1. Check if input is being received.
        if (controls.get_control_movement_x() != 0)
        {
            velocity = transf.TransformDirection(new Vector3(controls.get_control_movement_x() * speed_x, 0, 0));
            velocity.x = Mathf.Clamp(velocity.x, min_x, max_x);
        }
        else
        {
            velocity = new Vector3(0, 0, 0);
        }
    }

    // TEST
    private void calc_axis(float input, ref float coordinate, float min, float max)
    {
        if(input != 0)
        {
            coordinate = Mathf.Clamp(coordinate, min, max);
        }
        else
        {
            coordinate = 0;
        }
    }

    // DESC: calc_y ==========================================================================================
    // Name  : calc_y
    // Params: n/a
    // Descr : Calculates movement with read y-axis input.
    // =======================================================================================================
    private void calc_y()
    {

    }

    // DESC: move_x ==========================================================================================
    // Name  : move_x
    // Params: n/a
    // Descr : Applies movement with calculated x-axis input.
    // NOTE  : Moves by "strafing".
    // =======================================================================================================
    private void move_x()
    {
        transf.localPosition += velocity;
    }

    // DESC: move_y ==========================================================================================
    // Name  : move_y
    // Params: n/a
    // Descr : Applies movement with calculated y-axis input.
    // =======================================================================================================
    private void move_y()
    {

    }

    
    #endregion
}
