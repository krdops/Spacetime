// =====================================================================================
// Script: player_controls
// Copyright (c) 2016, Golden Iota Gaming - All rights reserved.
//    Unauthorized copying of this file via any medium is strictly prohibited.
//    All contents within are closed-source, i.e. proprietary and confidential.
// Author(s): Erik Chase, Kody Dibble
// Date Created: 27 June 2016
// Description: Reads/logs player keyboard input.
// NOTE: All controls currently remain static, i.e. non-configurable by user.
// NOTE: This script is only for organized logging of all player input. All handling
//       of events related to control should be referenced from here.
// =====================================================================================

using UnityEngine;
using System.Collections;

public class player_controls : MonoBehaviour
{

    #region VARS

    // Key Codes
    private KeyCode key_up = KeyCode.W;
    private KeyCode key_down = KeyCode.S;
    private KeyCode key_left = KeyCode.A;
    private KeyCode key_right = KeyCode.D;

    private KeyCode key_fire = KeyCode.Z;
    private KeyCode key_run = KeyCode.LeftShift;
    private KeyCode key_jump = KeyCode.Space;
    private KeyCode key_pause = KeyCode.Escape;

    // Input enabling.
    private bool enable_kbm = true;

    // Directional input (keyboard).
    private int key_move_x = 0;                  // 
    private int key_move_y = 0;                  //

    // Action input (keyboard).
    private bool key_isFiring = false;           // 
    private bool key_isJumping = false;          // 
    private bool key_isPausing = false;
    private bool key_isRunning = false;

    // True input values.
    public float control_movement_y = 0f;
    public float control_movement_x = 0f;
    public float control_fire = 0f;
    public float control_jump = 0f;
    public float control_run = 0f;
    public float control_pause = 0f;
    #endregion

    #region UNITY_FUNCS
    // DESC: Start ===========================================================================================
    // Name  : Start
    // Params: n/a
    // Descr : Unity function. Upon game start, this gameObject will initialize with this function.
    // =======================================================================================================
    void Start()
    {
        // 1. Reset values for keyboard and control.
        reset_control_input();
        reset_key_input();
    }

    // DESC: Update ==========================================================================================
    // Name  : Update
    // Params: n/a
    // Descr : Unity function. This is called once per frame during gameplay.
    // =======================================================================================================
    void Update()
    {
        if (enable_kbm) read_input_key();
    }
    #endregion

    #region GETS
    public bool isEnabled_keyboard() { return enable_kbm; }
    public float get_control_movement_x() { return control_movement_x; }
    public float get_control_movement_y() { return control_movement_y; }
    public float get_control_fire() { return control_fire; }
    public float get_control_jump() { return control_jump; }
    public float get_control_pause() { return control_pause; }
    public float get_control_run() { return control_run; } 

    #endregion

    #region SETS
    // DESC: setKeyboardEnable ===============================================================================
    // Name  : setKeyBoardEnable
    // Params: bool setting - new setting for enabling/disabling keyboard.
    // Descr : Sets the keyboard setting (enable/disable).
    // =======================================================================================================
    public void setKeyboardEnable(bool setting)
    {
        // 1. Adjust this bool to what is given as setting.
        enable_kbm = setting;

        // 2. For debug logging.
        if (enable_kbm) Debug.Log("Keyboard input is enabled.");
        else Debug.Log("Keyboard input is disabled.");

    }
    #endregion

    #region MISC_FUNCS
    // DESC: read_input_key ==================================================================================
    // Name  : read_input_key
    // Params: n/a
    // Descr : Reads all keyboard-based input.
    // =======================================================================================================
    private void read_input_key()
    {
        // 1. Reset key input.
        reset_key_input();
        
        // 1. Read directional input.
        key_move_y = read_key_range_digital(key_down, key_up);
        key_move_x = read_key_range_digital(key_left, key_right);

        // 2. Read general input.
        key_isFiring = read_key_press(key_fire);
        key_isJumping = read_key_press(key_jump);
        key_isPausing = read_key_press(key_pause);
        key_isRunning = read_key_press(key_run);

        // 3. Record key input as "real" values.
        record_key_input_all();

    }

    // DESC: record_key_input_all ============================================================================
    // Name  : record_key_input_all
    // Params: n/a
    // Descr : Organizes all true value recording into one function.
    // =======================================================================================================
    private void record_key_input_all()
    {
        // 1. True values for directional movement.
        record_key_input(ref key_move_y, ref control_movement_y);
        record_key_input(ref key_move_x, ref control_movement_x);

        // 2. True values for action.
        record_key_input(ref key_isFiring, ref control_fire);
        record_key_input(ref key_isJumping, ref control_jump);
        record_key_input(ref key_isRunning, ref control_run);
        record_key_input(ref key_isPausing, ref control_pause);
    }

    // DESC: record_key_input ================================================================================
    // Name  : record_key_input
    // Params: 1. ref bool read, ref float write (bool --> float)
    //         2. ref int read, ref float write (int --> float)
    //         3. ref float read, ref float write (float --> float)
    // Descr : Takes logged key inputs and records them to centralized "true" values.
    // NOTE: This is an overloaded function.
    // =======================================================================================================
    private void record_key_input(ref bool read, ref float write)
    {
        if (read) write = 1.0f;
        else write = 0f;
    }
    private void record_key_input(ref int read, ref float write)
    {
        write = (float)read;
    }
    private void record_key_input(ref float read, ref float write)
    {
        write = read;
    }

    // DESC: reset_control_input =============================================================================
    // Name  : reset_control_input
    // Params: n/a
    // Descr : Resets all "true" control values to 0.
    // =======================================================================================================
    private void reset_control_input()
    {
        // 1. Directional movement.
        control_movement_x = 0f;
        control_movement_y = 0f;

        // 2. Action inputs.
        control_fire = 0f;
        control_jump = 0f;
        control_pause = 0f;
        control_run = 0f;
    }

    // DESC: reset_key_input =================================================================================
    // Name  : reset_key_input
    // Params: n/a
    // Descr : Resets all "keyboard" control values to 0.
    // =======================================================================================================
    private void reset_key_input()
    {
        // 1. Directional movement.
        key_move_x = 0;
        key_move_y = 0;

        // 2. Action inputs.
        key_isFiring = false;
        key_isJumping = false;
        key_isRunning = false;
        key_isPausing = false;
    }

    // DESC: read_key_range_digital ==========================================================================
    // Name  : read_key_range_digital
    // Params: string negative - Unity key code for "negative" value.
    //         string positive - Unity key code for "positive" value.
    // Return: int
    // Descr : Compares key input between a negative and positive value, returns integer (-1, 0, 1).
    // NOTE  : Not analog (real) input. Only constricted values (digital).
    // =======================================================================================================
    private int read_key_range_digital(KeyCode negative, KeyCode positive)
    {
        // CASE A: Both keys are depressed.
        if (Input.GetKey(negative) && Input.GetKey(positive))
        {
            return 0;
        }

        // CASE B: Positive key is depressed.
        else if (Input.GetKey(positive))
        {
            return 1;
        }

        // CASE C: Negative key is depressed.
        else if (Input.GetKey(negative))
            return -1;

        // CASE D: No key is depressed.
        else
            return 0;
    }

    // DESC: read_key_press ==================================================================================
    // Name  : read_key_press
    // Params: string keyCode - Unity key code for key being used in this function.
    // Return: bool - true: key has been input.
    //                false: key has not been input.
    // Descr : Determines whether this key is depressed or not.
    // =======================================================================================================
    private bool read_key_press(KeyCode key)
    {
        // CASE A: Key is depressed.
        if (Input.GetKey(key))
            return true;

        // CASE B: Key is NOT depressed.
        else
            return false;
    }


    #endregion
}
