using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float dir;					//Initial direction is set to 0 on start so the player doesn't move until he taps
	public float speed = 0.03f;			//Speed for the surfer
	public float dec = 0.015f;			//Deceleration speed
	public float acc = 0.015f;			//Acceleration speed
	public float accTime = 0.4f;		//Acceleration init time
	public float moveTime = 0.8f;		//Max speed init time

	private bool isDead = false;		//Used for game over 
	private Animator animate;			//Access the animator for the player


	// Use this for initialization
	void Start () {
		//Set dir to 0 on initialise
		dir = 0f;
		//Get the animator from the player on initialise
		animate = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {		
		
		// If the player is alive do this
		if (GameManager.instance.hasBegun == false){
			if (Input.GetButtonDown ("Fire1")) {
				GameManager.instance.hasBegun = true;
				GameManager.instance.startText.SetActive (false);
				GameManager.instance.pauseButton.SetActive (true);
			} 
		}

		// General player placement
		if (isDead == false && GameManager.instance.hasBegun == true) {
			// If the player isn't moving i.e just started, then on tap set dir to left
			if (dir == 0f && Input.GetButtonDown ("Fire1")) {
				dir = -speed;
			} 
			// If the player is moving left, then on tap set dir to right and animate accordingly
			else if (dir == -speed && Input.GetButtonDown ("Fire1")) {
				// Decelerate
				dir = -dec;
				// Accelerate
				Invoke ("accRight", accTime);
				// Move
				Invoke ("moveRight", moveTime);

			} 
			// If the player is moving right, then on tap set dir to left and animate accordingly
			else if (dir == speed && Input.GetButtonDown ("Fire1")) {
				// Decelerate
				dir = dec;
				// Accelerate
				Invoke ("accLeft", accTime);
				// Move
				Invoke ("moveLeft", moveTime);
			}
		}

		// Move the player 
		this.transform.Translate (dir, 0, 0);

		// Pause the game
		if (GameManager.instance.gameOver == false && GameManager.instance.isPaused == false && Input.GetKeyDown (KeyCode.Escape)) {
			GameManager.instance.isPaused = true;
			GameManager.instance.pauseText.SetActive (true);
			GameManager.instance.pauseButton.SetActive (false);
		}
		// Unpause the game
		if (GameManager.instance.gameOver == false && GameManager.instance.isPaused == true && Input.GetMouseButtonDown(0)) {
			GameManager.instance.isPaused = false;
			GameManager.instance.pauseText.SetActive (false);
			GameManager.instance.pauseButton.SetActive (true);
		}
		// If the game is paused, freeze player
		if (GameManager.instance.isPaused == true && dir == -speed) {
			this.transform.Translate (speed, 0, 0);
		} else if (GameManager.instance.isPaused == true && dir == speed) {
			this.transform.Translate (-speed, 0, 0);
		}

	}

	public void accLeft(){
		dir = -acc;
	}

	public void moveLeft(){
		dir = -speed;
		animate.SetTrigger ("Left");
	}

	public void accRight(){
		dir = acc;
	}

	public void moveRight(){
		dir = speed;
		animate.SetTrigger ("Right");
	}

	// Collision with island kills player
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Obstacle") {
			// Set the player isDead state to true
			isDead = true;
			// Access and fire SurferDied() from GameManager
			GameManager.instance.SurferDied ();		
		} else if (coll.gameObject.tag == "Points") {
			GameManager.instance.CollectCoins ();
			Destroy (coll.gameObject);
		}
	}
}
