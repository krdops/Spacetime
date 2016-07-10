######################
#WEAPON TO INVEN
######################


using UnityEngine;
using System.Collections;


public class Weapon : MonoBehaviour {
	
	
	
	public enum WeaponType 
	{
		
		Pistol,
		Rifle,
		Bow,
		Lazer,
		Knives
	};
	
	public WeaponType weaponType;
	public Texture weaponLogo = null; 
	public float dmg = 0; 
	
	
}



