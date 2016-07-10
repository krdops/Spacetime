############################
#INVENTORY SYSTEM
############################



using UnityEngine;
using System.Collections;

// Required if you will be using list and we will.

using System.Collections.Generic;


public class Inventory : MonoBehaviour {
	
	
	
	
	
########
### WEAPON OBJECTS / LIST / POSITIONING 
########


public List<KeyCode> numberss = new List<KeyCode>{ KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
        KeyCode.Alpha4, KeyCode.Alpha5};
		
public List<Weapon> myWeaponList = null; 
public Weapon currentWeapon = null; 
public Transform weaponPos = null;
public GameObject currentWeaponGO = null;

public Rect guiAreaRect = new Rect(0, 0, 0, 0);

void OnGUI() 
{
	
	
	//begin guilayout area 
	GUILayout.BeginArea(guiAreaRect);
	//begin vertical group 
	GUILayout.BeginVertical();
	
	//loop through the list of weapons 
	
	
	foreach(Weapon weapon in myWeaponList)
	{
		##### WEAPON GUI 
		
		if(weapon != null)
		{
			
			if(GUILayout.Button(weapon.weaponLogo,GUILayout.Width(50),GUILayout.Height(50)))
			{
				
				currentWeapon = weapon; 
				
			}
		}
	}
	//We need to close the vertical grp and gui area group.
	GUILayout.EndVertical();
	GUILayout.EndArea();
	
	
	#### WEAPON DEBUG PRINT WEAPON
	
if(GUILayout.Button(weapon.weaponLogo,GUILayout.Width(50),GUILayout.Height(50)))
{
    //if we clicked the button it will but that weapon to our selected(equipped) weapon
    currentWeapon = weapon;
    Debug.Log(currentWeapon);
}
	
}
}


### CHANGE WEAPON WITH KEY CODES 

public void ChangWeaponWithNumbers() {
	
	
	for(int i=0;i<numberss.Count;i++)
	{
		
	 if(myWeaponList[i] != null)
	 {
		 
	 
		if(Input.GetKeyDown(numberss[i]))
		{
			
			currentWeapon = myWeaponList[i];
			
			ChangeWeapon(myWeaponList[i]);
		}
	}
}

}
	
	


void Update() {
	
	ChangWeaponWithNumbers();
	
	
	
}

	
	




