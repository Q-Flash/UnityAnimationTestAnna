using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnaController : MonoBehaviour {

	static Animator anim;
	public float speed = 10.0F;
	public float rotationSpeed = 100.0F;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		float translation = Input.GetAxis("Vertical")*speed;
		float rotation = Input.GetAxis("Horizontal")*rotationSpeed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		transform.Translate(0, 0, translation);
		transform.Rotate(0, rotation, 0);

		//Setting trigger for jumping when spacebar is pressed
		if(Input.GetButtonDown("Jump")){
			anim.SetTrigger("isJumping");
		}

		//Kick when M is pressed
		if(Input.GetButtonDown("Kick")){
			anim.SetTrigger("isKicking");
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
