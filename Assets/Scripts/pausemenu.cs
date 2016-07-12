using UnityEngine;
using System.Collections;
using UnityStandardAssets Characters.ThirdPerson;

public class PauseGame : MonoBehaviour {

	public Transform canvas;

	
	void Update() {
		
		Pause();
		
	}
	
	public void Pause() {
		

		if(Input.GetKeyDown(keyCode.escape))
		{
			
			
			if(canvas.gameObject.activeInHirearchy == false)
			{
				canvas.gameObject.SetActive(true);
				Time.timeScale = 0;
				Player.GetComponent<ThirdPersonController>().enabled = false;
			} else 
			{
				canvas.gameObject.SetActive(false);
				Time.timeScale = 1;
				Player.GetComponent<ThirdPersonController>().enabled = true;
			}
			
			
		}
		
		
		
	}
	


}