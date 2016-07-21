// =====================================================================================
// Script: inventweaponex.cs
// Copyright (c) 2016, Golden Iota Gaming - All rights reserved.
//    Unauthorized copying of this file via any medium is strictly prohibited.
//    All contents within are closed-source, i.e. proprietary and confidential.
// Author(s): Kody Dibble
// Date Created: July 2016
// Description: Establishes weapon type.
// NOTE: SUGGEST MERGE TO INVENTEX.CS
// =====================================================================================

using UnityEngine;
using System.Collections;


public class Weapon : MonoBehaviour
{
	
	public enum WeaponType 
	{
		Pistol,
		Rifle,
		Bow,
		Laser,
		Knife
	};
	public WeaponType weaponType;
	public Texture weaponLogo = null; 
	public float dmg = 0; 
	
}



