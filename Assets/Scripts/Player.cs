using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float dir;					//Initial direction is set to 0 on start so the player doesn't move until he taps

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
		// Move the player nowhere on start
		this.transform.Translate (dir, 0, 0);
		// If the player is alive do this
		if (GameManager.instance.hasBegun == false){
			if (Input.GetButtonDown ("Fire1")) {
				GameManager.instance.hasBegun = true;
				GameManager.instance.startText.SetActive (false);
			} 
		}

		// General player movement
		if (isDead == false && GameManager.instance.hasBegun == true) {
			// If the player isn't moving i.e just started, then on tap set dir to left
			if (dir == 0f && Input.GetButtonDown ("Fire1")) {
				this.transform.Translate (-0.1f, 0, 0);
				dir = -0.1f;
			} 
			// If the player is moving left, then on tap set dir to right and animate accordingly
			else if (dir == -0.1f && Input.GetButtonDown ("Fire1")) {
				this.transform.Translate (0.1f, 0, 0);
				dir = 0.1f;
				animate.SetTrigger ("Right");
			} 
			// If the player is moving right, then on tap set dir to left and animate accordingly
			else if (dir == 0.1f && Input.GetButtonDown ("Fire1")) {
				this.transform.Translate (-0.1f, 0, 0);
				dir = -0.1f;
				animate.SetTrigger ("Left");
			}
		}

		// Pause the game
		if (GameManager.instance.gameOver == false && GameManager.instance.isPaused == false && Input.GetKeyDown (KeyCode.Escape)) {
			GameManager.instance.isPaused = true;
			GameManager.instance.pauseText.SetActive (true);
		}
		// Unpause the game
		if (GameManager.instance.gameOver == false && GameManager.instance.isPaused == true && Input.GetMouseButtonDown(0)) {
			GameManager.instance.isPaused = false;
			GameManager.instance.pauseText.SetActive (false);
		}
		// If the game is paused, freeze player
		if (GameManager.instance.isPaused == true && dir == -0.1f) {
			this.transform.Translate (0.1f, 0, 0);
		} else if (GameManager.instance.isPaused == true && dir == 0.1f) {
			this.transform.Translate (-0.1f, 0, 0);
		}

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
