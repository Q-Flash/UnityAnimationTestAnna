using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnaController : MonoBehaviour {

	static Animator anim;
	private bool isCasting = false; //Bool to signal when a skill is being casted
	private float castingTimeout = 0.00F; //How much time is left for cast duration of current skill
	public float speed = 10.0F; //Running speed
	public float rotationSpeed = 100.0F; //Turn speed

	//Skill cast times
	private float kickCastDuration = 1.833F; //Cast time for kick skill


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Update skill cast durations
		if (castingTimeout > 0.00F) {
			castingTimeout -= Time.deltaTime;
		} else {
			isCasting = false;
			castingTimeout = 0.00F;
		}	


		//Movement with arrow-keys/WASD
		float translation = 0;
		float rotation = 0;
		
		if (!isCasting) {
			translation = Input.GetAxis ("Vertical") * speed;
			rotation = Input.GetAxis ("Horizontal") * rotationSpeed;
			translation *= Time.deltaTime;
			rotation *= Time.deltaTime;
			transform.Translate (0, 0, translation);
			transform.Rotate (0, rotation, 0);
		}


		//Setting trigger for jumping when spacebar is pressed
		if (Input.GetButtonDown ("Jump")) {
			if (isCasting) {
				
				
			} else {
				anim.SetTrigger("isJumping");
			}
		
			
		}

		//Kick when M is pressed
		if(Input.GetButtonDown("Kick")){
			if (isCasting) {
				
				
			} else {
				anim.SetTrigger("isKicking");
				isCasting = true; 
				castingTimeout = kickCastDuration;
			}
			
		}

		//Set running animation when character is running
		if(translation != 0){
			anim.SetBool("isRunning", true);
		}
		else{
			anim.SetBool("isRunning", false);
		}
	}
}
