// =====================================================================================
// Script: player_movement
// Copyright (c) 2016, Golden Iota Gaming - All rights reserved.
//    Unauthorized copying of this file via any medium is strictly prohibited.
//    All contents within are closed-source, i.e. proprietary and confidential.
// Author(s): Erik Chase, Kody Dibble
// Date Created: 1 July 2016
// Description: Moves the player entity based on control input and any logical restrictions.
// NOTE: RUDIMENTARY STAGE -- STILL WIP. IF THERE'S A BETTER SCRIPT, FEEL FREE TO REPLACE
// =====================================================================================

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(player_controls))]

public class player_movement : MonoBehaviour {

    #region VARS

    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public float moveSpeed = 10.0f;

    #endregion

    #region UNITY_FUNCS
    // DESC: Start ===========================================================================================
    // Name  : Start
    // Params: n/a
    // Descr : Unity function. Upon game start, this gameObject will initialize with this function.
    // =======================================================================================================
    void Start()
    {

    }

    // DESC: FixedUpdate =====================================================================================
    // Name  : FixedUpdate
    // Params: n/a
    // Descr : Unity function. This is called every time interval (determined by Unity).
    // =======================================================================================================
    void FixedUpdate()
    {
        read_input();
    }
    #endregion

    #region MISC_FUNCS

    public void read_input()
    {
        if (Input.GetKey(forward)) move_forward();
        else if (Input.GetKey(backward)) move_backward();

        if (Input.GetKey(right)) move_right();
        else if (Input.GetKey(left)) move_left();
    }

    public void move_forward()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }

    public void move_backward()
    {
        transform.Translate(transform.forward * -moveSpeed * Time.deltaTime);
    }

    public void move_right()
    {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime);
    }

    public void move_left()
    {
        transform.Translate(transform.right * -moveSpeed * Time.deltaTime);
    }

    /*
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
        //calc_rotation();
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
    */
    
    #endregion
}
