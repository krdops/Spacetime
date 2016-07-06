using UnityEngine;
using System.Collections;


public class AnimScript : MonoBehaviour


{

	Animator anim;
	int jumpHash = Animator.StringToHash("Jump");
	
	
	void Start() {
	
	
		anim = GetComponent<Animator>();
		
	}
	
	
	void Update() {
	
		float move = Input.GetAxis("Vertical");
		anim.SetFloat("Speed", move);
		
	
		if(Input.GetKeyDown(KeyCode.Space))
		{
			
			anim.SetTrigger(jumpHash);
			
		}
	}


}