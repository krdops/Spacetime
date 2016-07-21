// =====================================================================================
// Script: inventex.cs
// Copyright (c) 2016, Golden Iota Gaming - All rights reserved.
//    Unauthorized copying of this file via any medium is strictly prohibited.
//    All contents within are closed-source, i.e. proprietary and confidential.
// Author(s): Kody Dibble
// Date Created: July 2016
// Description: Inventory management.
// =====================================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic; // For list creation.

public class Inventory : MonoBehaviour {
    
    #region VARS
    public bool DEBUG_MODE = false;
    public List<KeyCode> numbers = new List<KeyCode>{
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5
    };
    public List<Weapon> myWeaponList = null; 
    public Weapon currentWeapon = null; 
    public Transform weaponPos = null;
    public GameObject currentWeaponGO = null;
    public Rect guiAreaRect = new Rect(0, 0, 0, 0);
    #endregion

    #region UNITY_FUNCS

    // DESC: Update ==========================================================================================
    // Name  : Update
    // Params: n/a
    // Descr : Unity function. This is called once per frame.
    // =======================================================================================================
    void Update()
    {
        // ChangeWeaponWithNumbers();
    }

    // DESC: OnGUI ===========================================================================================
    // Name  : OnGUI
    // Params: n/a
    // Descr : Unity function. For rendering/handling GUI events, and may be called several times per frame 
    //         (but one call per event).
    // =======================================================================================================
    void OnGUI() 
    {
	    // 1. Begin guilayout area.
	    GUILayout.BeginArea(guiAreaRect);

	    // 2. Begin vertical group.
	    GUILayout.BeginVertical();
	
	    // 3. Loop through the list of weapons
	    foreach(Weapon weapon in myWeaponList)
	    {
		    // WEAPON GUI 
		    if(weapon != null)
		    {
			    if(GUILayout.Button(weapon.weaponLogo,GUILayout.Width(50),GUILayout.Height(50)))
			    {
				    currentWeapon = weapon;
                    
                    // Debug: log current weapon.
                    if(DEBUG_MODE) Debug.Log(currentWeapon);
                }
		    }
	    }

	    // 4. We need to close the vertical grp and gui area group.
	    GUILayout.EndVertical();
	    GUILayout.EndArea();
	}

    #endregion

    #region MISC_FUNCS

    // DESC: ChangeWeaponWithNumbers =========================================================================
    // Name  : ChangeWeaponWithNumbers
    // Params: n/a
    // Descr : Swaps currently held weapon according to keycode (Alpha-Numeric).
    // Note  : TEMPORARILY DISABLED FOR WORK IN PROGRESS
    // =======================================================================================================
    private void ChangeWeaponWithNumbers()
    {
	    for(int i=0;i<myWeaponList.Count;i++)
	    {
	        if(myWeaponList[i] != null)
	        {
                // Temporarily Disabled: Needs keycode for switching weapons.
		        if(true /*input.GetKeyDown()*/)
		        {
			        //currentWeapon = myWeaponList[i];
			        //ChangeWeapon(myWeaponList[i]);
		        }
	        }
        }
    }
	
    #endregion

}





