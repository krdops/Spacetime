using UnityEngine;
using System.Collections;


public class Enemy_AI : MonoBehaviour {

	Transform tr_player;
	float frotspeed = 3.0f;
	float fmovspeed = 3.0f;

void start() {

	tr_player = GameObject.FindGameObjectWithTag("Player").transform;
	

}


void update() {

	transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(tr_player.position - transform.position),
		frotspeed * Time.deltaTime);
	
	

	transform.position += transform.forward * fmovspeed * Time.deltaTime;
	

}

}

