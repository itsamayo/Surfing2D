using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public GameObject startText;
	public GameObject gameOverText;
	public GameObject pauseText;
	public Text scoreText;
	public bool gameOver = false;
	public bool hasBegun = false;
	public bool isPaused = false;

	private float score = 0;

	// Use this for initialization
	void Awake () {
		// Just to make sure there's only ever one instance of the GameManager
		if (instance == null) {
			instance = this;
		} 
		// If there is one, then destroy this one
		else if (instance != this) {
			Destroy (gameObject);
		}

		//First screen seen on entry
		startText.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		// If gameOver is true then on tap will restart the game - NB this is temporary
		if (gameOver == true && Input.GetMouseButtonDown(0)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		} 

		scoreText.text = "Score: " + score.ToString ();
	}

	//Collect coins
	public void CollectCoins(){
		score++;
	}

	// When the player dies
	public void SurferDied(){
		// Sets the game over overlay to visible
		gameOverText.SetActive (true);
		// Sets gameOver to true so that the player can't move anymore
		gameOver = true;
	}
}
