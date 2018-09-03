using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnaController : MonoBehaviour {

	static Animator anim;
	private CharacterController controller;
	
	//Variables for movement
	private float verticalVelocity;
	private float gravity = 14.0F;
	private float jumpForce = 10.0F;
	
	private bool isCasting = false; //Bool to signal when a skill is being casted
	private float castingTimeout = 0.000F; //How much time is left for cast duration of current skill
	public float speed = 10.0F; //Running speed
	public float rotationSpeed = 100.0F; //Turn speed

	//Skill cast times
	private float kickCastDuration = 1.833F; //Cast time for kick skill
	private float jabCastDuration = 0.833F;
	private float punchCastDuration = 1.867F;
	private float backflipCastDuration = 2.033F;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Update skill cast durations
		if (castingTimeout > 0.000F) {
			castingTimeout -= Time.deltaTime;
		} else {
			isCasting = false;
		}	
	
		//Character movement
		if (controller.isGrounded) {
			verticalVelocity = -gravity * Time.deltaTime;

			if (Input.GetKeyDown (KeyCode.Space)) {
				verticalVelocity = jumpForce;
			}
		} else {
			verticalVelocity -= gravity * Time.deltaTime;
		}	

		Vector3 moveVector = Vector3.zero;
		//float rotation = 0;
		//rotation = Input.GetAxis ("Horizontal") * rotationSpeed;
		//rotation *= Time.deltaTime;
		moveVector.x = Input.GetAxis("Horizontal")*speed;
		moveVector.y = verticalVelocity;
		moveVector.z = Input.GetAxis("Vertical")*speed;

		//transform.Rotate (0, rotation, 0);
		controller.Move(moveVector*Time.deltaTime);


/*
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
*/

		//Setting trigger for jumping when spacebar is pressed
		if (Input.GetButtonDown ("Jump")) {
			if (isCasting) {

			} else {
				if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
					doBackFlip ();
				} else {
					anim.SetTrigger ("isJumping");
				}
			}
		}


		/*
		//Epic Backflip
		if ( translation < 0 && Input.GetButtonDown ("Jump")) {
			doBackFlip ();
		}
		*/
		//Roundhouse kick to the face
		if (Input.GetButtonDown ("Kick")) {
			if (isCasting) {
				
				
			} else {
				anim.SetTrigger ("isKicking");
				isCasting = true; 
				castingTimeout = kickCastDuration;
			}
			
		}

		//Punching time
		if (Input.GetButtonDown ("Punch")) {
			if (isCasting) {
				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("First Jab")) {
					//If we already cast second punch, do not add to timer
					if(!anim.GetBool("isSecondPunching")){
						castingTimeout += punchCastDuration;
					}

					anim.SetTrigger ("isSecondPunching");
				}	
				
			} else {
				anim.SetTrigger ("isPunching");
				isCasting = true; 
				castingTimeout = jabCastDuration;
			}
		}
		



		//Set running animation when character is running
		if((moveVector.x != 0) || (moveVector.z != 0)){
			anim.SetBool("isRunning", true);
		}
		else{
			anim.SetBool("isRunning", false);
		}

	}

	void doBackFlip (){
		anim.SetTrigger ("isBackFlipping");
		isCasting = true; 
		castingTimeout = backflipCastDuration;
	}

}
