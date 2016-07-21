// =====================================================================================
// Script: pausemenu.cs
// Copyright (c) 2016, Golden Iota Gaming - All rights reserved.
//    Unauthorized copying of this file via any medium is strictly prohibited.
//    All contents within are closed-source, i.e. proprietary and confidential.
// Author(s): Kody Dibble
// Date Created: July 2016
// Description: Pause event handler.
// =====================================================================================

using UnityEngine;
using System.Collections;
//using UnityStandardAssets Characters.ThirdPerson;

public class PauseGame : MonoBehaviour {

    #region VARS
    public Transform canvas;
    #endregion

    #region UNITY_FUNCS

    void Update() {
		
		Pause();
		
	}

    #endregion

    #region MISC_FUNCS

    public void Pause() {

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(canvas.gameObject.activeInHierarchy == false)
			{
				canvas.gameObject.SetActive(true);
				Time.timeScale = 0;
				// Player.GetComponent<ThirdPersonController>().enabled = false; // DISABLE THIRD PERSON CONTROLLER
			}
            else 
			{
				canvas.gameObject.SetActive(false);
				Time.timeScale = 1;
				// Player.GetComponent<ThirdPersonController>().enabled = true;  // ENABLE THIRD PERSON CONTROLLER
			}
		}
	}

    #endregion
    
}