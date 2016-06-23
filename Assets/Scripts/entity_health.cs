// =====================================================================================
// Script: entity_health
// Copyright (c) 2016, Golden Iota Gaming - All rights reserved.
//    Unauthorized copying of this file via any medium is strictly prohibited.
//    All contents within are closed-source, a.k.a. proprietary and confidential.
// Author(s): Erik Chase, Kody Dibble
// Date Created: 22 June 2016
// Description: Gives attached GameObject a "health" value. Securely handles all adjustments
//              to health values, and will mark entity as "dead" accordingly.
// =====================================================================================

using UnityEngine;
using System.Collections;

public class entity_health : MonoBehaviour {

    #region VARS

    // VARS ================================================================
    public float currentHealth = 0f;          // Current health of entity during gameplay.
    public float initialHealth = 100f;        // Health amount of entity upon "spawn".
    public float maximumHP = 100f;            // Maximum health amount of entity.
    public float deathThreshold = 0f;         // Exact point where death is determined. Usually 0.

    public bool startWithMaxHealth = false;   // Decides if entity should spawn with full (max) health.
    public bool isDead = false;               // Death state of entity (based on health). Should be useful for other scripts (animation, etc).

    #endregion

    #region UNITY_FUNCTIONS
    // DESC: Start ===========================================================================================
    // Name  : Start
    // Params: n/a
    // Descr : Unity function. Upon game start, this gameObject will initialize with this function.
    // =======================================================================================================
    void Start() {

        // 1. Check max/min health values.
        initializeHealth(startWithMaxHealth);

    }
    
    // DESC: Update ==========================================================================================
    // Name  : Update
    // Params: n/a
    // Descr : Unity function. This is called once per frame during gameplay.
    // =======================================================================================================
    void Update() {

    }
    #endregion

    #region GET_FUNCTIONS

    // DESC: getCurrentHealth ================================================================================
    // Name  : getCurrentHealth
    // Params: n/a
    // Return: float
    // Descr : Returns current health value.
    // =======================================================================================================
    public float getCurrentHealth()
    {
        return currentHealth;
    }

    // DESC: getDeathState ===================================================================================
    // Name  : getDeathState
    // Params: n/a
    // Return: bool
    // Descr : Returns the death state of entity.
    // =======================================================================================================
    public bool getDeathState()
    {
        return isDead;
    }

    // DESC: getDeathThreshold ===============================================================================
    // Name  : getDeathThreshold
    // Params: n/a
    // Return: float
    // Descr : Returns value at which death is determined.
    // =======================================================================================================
    public float getDeathThreshold()
    {
        return deathThreshold;
    }

    // DESC: getMaximumHealth ================================================================================
    // Name  : getCurrentHealth
    // Params: n/a
    // Return: float
    // Descr : Returns the ceiling value of this entity's health.
    // =======================================================================================================
    public float getMaximumHealth()
    {
        return maximumHP;
    }
    
    #endregion

    #region SET_FUNCTIONS

    // DESC: setCurrentHealth ================================================================================
    // Name  : setCurrentHealth
    // Params: float newCurrentHealth - new value to set this gameObject's current health.
    // Descr : Sets current health to specified value. Automatically binds within death/max boundaries.
    // =======================================================================================================
    public void setCurrentHealth(float newCurrentHealth) {

        // a. Is newCurrentHealth lower than death threshold? Set currentHealth to deathThreshold if so.
        if (newCurrentHealth < deathThreshold)
        {
            currentHealth = deathThreshold;
        }
        // b. Is newCurrentHealth higher than max threshold? Make currentHealth = maximumHP if so.
        else if (newCurrentHealth > maximumHP)
        {
            currentHealth = maximumHP;
        }
        else
        {
            currentHealth = newCurrentHealth;
        }

    }

    // DESC: setDeathState ===================================================================================
    // Name  : setDeathState
    // Params: bool markedAsDead - (true) mark this entity as "dead".
    //                             (false) this entity is still considered "alive".
    // Descr : Securely sets the "death" boolean and adjusts the current health values accordingly.
    // NOTES : "True" death state implies that health may no longer be adjusted.
    // NOTES : Does not (yet?) currently support any concept of "revival". Life is a one-shot deal so far.
    // =======================================================================================================
    private void setDeathState(bool markedAsDead)
    {
        // 1. Has the entity been marked as "dead"? Change the health values if so.
        if (markedAsDead)
        {
            // 1a. Set the current health to death threshold (usually 0).
            setCurrentHealth(deathThreshold);
            // 2a. Set the death state as "true".
            isDead = true;
        }
    }

    // DESC: setMaximumHealth ================================================================================
    // Name  : setMaximumHealth
    // Params: float newMaxHealth - New value to be set as the new maximum health amount.
    // Descr : Securely adjusts the "maximum health" threshold to a new value specified by parameter.
    // =======================================================================================================
    public void setMaximumHealth(float newMaxHealth)
    {
        // 1. Is the new maximum health greater than the death threshold? (proceed, otherwise log error to console)
        if (newMaxHealth >= getDeathThreshold())
        {

            // 2. Is the new maximum health lower than the current health?
            if (newMaxHealth < getCurrentHealth())
            {
                // 2a. Lower the current health to the new max value.
                setCurrentHealth(newMaxHealth);
            }

            // 3. Set the new maximum health.
            maximumHP = newMaxHealth;

        }
        else
        {
            // Print an error to the unity console. Do not perform any other action, especially change the maximum health.
            errorLogHandler("New maximum health is below death threshold! Aborting change.", gameObject.transform.name, this.GetType().FullName, 1);
        }
    }

    #endregion

    #region MISC_FUNCTIONS

    // DESC: checkDeath ======================================================================================
    // Name  : checkDeath
    // Params: n/a
    // Descr : Checks if this entity has reached the "death state". Intended for "update()" function for repeat checking.
    // NOTES : This function and the "initializeHealth()" functions should be the only ones that can adjust death state.
    // =======================================================================================================
    private void checkDeath()
    {
        // 1. If current health is less than or equal to death threshold, set this entity as "dead".
        if (getCurrentHealth() <= getDeathThreshold()) setDeathState(true);
    }

    // DESC: initializeHealth ================================================================================
    // Name  : initializeHealth
    // Params: bool maxInit - if true, initializes health to full, "maximum" value.
    //                        if false, initializes health to current value.
    // Descr : Securely initializes health values, ensures initialization is valid (i.e. deathThreshold !>= maximumHealth)
    // =======================================================================================================
    private void initializeHealth(bool maxInit)
    {
        // 1. Is the "deathThreshold" higher than the maximum health value?
        if (deathThreshold >= maximumHP)
        {
            // 1a. This entity is considered "dead".
            setDeathState(true);

            // 1b. Print this "caution" message to log.
            errorLogHandler("Maximum health is less than death threshold! This entity will initialize already in death state.", gameObject.transform.name, this.GetType().FullName, 1);
        }
        // 2. Death/max values are valid. Set the current health to value depending on "maxInit".
        else
        {
            // 2a. This entity is considered "alive"!
            setDeathState(false);

            // 2b. CASE A. Set current health to full (maximum).
            if (maxInit)
                setCurrentHealth(getMaximumHealth());
            // 2b. CASE B. Set current health to whatever is specified as "initial".
            else
                setCurrentHealth(initialHealth);
        }
        
    }

    // DESC: damage ==========================================================================================
    // Name  : damage
    // Params: float dmgAmt - must be positive, amount intended to take from current health.
    // Descr : Provided "dmgAmt" is negative, applies "damage" amount to current health through subtraction.
    // =======================================================================================================
    public void damage(float dmgAmt)
    {
        // 1. The damage amount is positive, so apply it to current health.
        if (dmgAmt >= 0)
            setCurrentHealth(getCurrentHealth() - dmgAmt);
        // 2. The damage amount is negative. Print error to unity debug log. Do nothing.
        else
            errorLogHandler("Damage must be a positive value!", this.GetType().FullName, gameObject.transform.name, 2);
    }

    // DESC: heal ============================================================================================
    // Name  : heal
    // Params: float healAmt - must be positive, amount intended to add to current health.
    // Descr : Provided "healAmt" is positive, applies "healing" amount to current health through addition.
    // =======================================================================================================
    public void heal(float healAmt)
    {
        // 1. The heal amount is postive, so apply it to current health.
        if (healAmt > 0)
            setCurrentHealth(getCurrentHealth() + healAmt);
        // 2. The heal amount is negative. Print error to unity debug log. Do nothing.
        else
            errorLogHandler("Heal amount must be a positive value!", this.GetType().FullName, gameObject.transform.name, 2);
    }

    // DESC: errorLogHandler =================================================================================
    // Name  : errorLogHandler
    // Params: string logMessage - message to be displayed in Unity debug log.
    //         string gameObjName - name of this gameObject which caused this error.
    //         string funcName - name of function that calls this "error" function. Usually "this.GetType().FullName".
    //         int errorType - Adjusts color of log message. 0 = default, 1 = yellow (caution), 2 = red (error).
    // Descr : Prints a "Debug.Log" message to Unity console. Useful for debugging, but must be explicitly defined in calling functions.
    // =======================================================================================================
    private void errorLogHandler(string logMessage, string gameObjName, string funcName, int errorType)
    {
        switch (errorType)
        {
            // Case 0 - Regular message.
            case 0:
                {
                    Debug.Log("[" + gameObjName + ", " + funcName + "] " + logMessage);
                    break;
                }
            // Case 1: Warning message. Yellow.
            case 1:
                {
                    Debug.Log("<color=yellow>WARNING: [" + gameObjName + ", " + funcName + "] " + logMessage + "</color>)");
                    break;
                }
            // Case 2: Error message. Red.
            case 2:
                {
                    Debug.Log("<color=red>ERROR: [" + gameObjName + ", " + funcName + "] " + logMessage + "</color>");
                    break;
                }
            // Default: Regular message.
            default:
                {
                    Debug.Log("[" + gameObjName + ", " + funcName + "] " + logMessage);
                    break;
                }
        }
    }
    #endregion
}
