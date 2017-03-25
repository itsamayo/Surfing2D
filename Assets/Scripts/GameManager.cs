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
	public GameObject pauseButton;
	public Text scoreText;
	public bool gameOver = false;
	public bool hasBegun = false;
	public bool isPaused = false;

	private float distance = 0f;
	private float score = 0f;

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
		//if (gameOver == true && Input.GetMouseButtonDown(0)) {
		//	SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		//} 

		if (isPaused == false && gameOver == false && hasBegun == true) {
			distance += Time.deltaTime;
			score = Mathf.Round (distance);
		}

		scoreText.text = score.ToString () + "m";
	}

	//Collect coins
	public void CollectCoins(){
		score++;
	}

	// When the player dies
	public void SurferDied(){
		// Sets the game over overlay to visible
		gameOverText.SetActive (true);
		pauseButton.SetActive (false);
		// Sets gameOver to true so that the player can't move anymore
		gameOver = true;
	}
}
