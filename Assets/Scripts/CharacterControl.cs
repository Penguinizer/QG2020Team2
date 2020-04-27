using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//Impulse timing based on documentation script reference for arcade firing.
public class CharacterControl : MonoBehaviour{	
	//Initializing variables
	[SerializeField]
	float moveSpeed = 4.0f;
	[SerializeField]
    float powerBase = 2.0f;
    [SerializeField]
    float impulseDistance = 5.0f;
    [SerializeField]
    float impulseForce = 2.0f;
	[SerializeField]
	float impulseCooldown = 0.5f;
	[SerializeField]
	float createCooldown = 1.0f;
	[SerializeField]
	GameObject P1PosPrefab;
	[SerializeField]
	GameObject P1MinPrefab;
	[SerializeField]
	GameObject P2PosPrefab;
	[SerializeField]
	GameObject P2MinPrefab;
	[SerializeField]
	float energyOnPickup = 1.0f;
	[SerializeField]
	float startingEnergy = 0.0f;
	[SerializeField]
	float areaBallCost = 1.0f;
	[SerializeField]
	GameObject AreaBallPrefab;
	[SerializeField]
	float posMinBallCost = 0.0f;
	[SerializeField]
	float areaToCoverToWin = 120.0f;
	[SerializeField]
	float passiveEnergyPerSecond = 1.0f;
	[SerializeField]
	Text energyText;
	[SerializeField]
	Text areaText;
	[SerializeField]
	Text winText;
	[SerializeField]
	float timeBeforeReset = 5.0f;
	[SerializeField]
	bool startGamePaused = false;
	[SerializeField]
	float maxEnergy = 200.0f;
	[SerializeField]
	Slider fillBar;
	[SerializeField]
	Slider areaBar;
	[SerializeField]
	GameObject mainMenu;

    GameObject musicPlayer;

	private bool gameIsUnpaused;
	
	private float myTime = 0.0f;
	private float nextImpulse = 0.0f;
	private float nextCreate = 0.0f;
	private Vector3 impForce;
	private Vector3 impForceDir;
	private Rigidbody2D character;
	private Vector3 ballPos;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 playerPos = Vector3.zero;
	private Vector3 newPos = Vector3.zero;
	private float currentEnergy;
	private string impulseControlString;
	private string areaControlString;
	private string minControlString;
	private string posControlString;
	private string horControlString;
	private string vertControlString;
	private string[] thingsToImpulse = new string[] {"PosBall", "MinusBall"};
	private bool isWinner = false;
	private float tempArea = 0.0f;
	private bool tooClose;
	
	//Handles adding energy
	public void addEnergy(){
		//print("BOOSH");
		if (currentEnergy+energyOnPickup<= maxEnergy){
			currentEnergy += energyOnPickup;
		}
		else{
			currentEnergy = maxEnergy;
		}
		fillBar.value = currentEnergy/maxEnergy;
	}
	
	public float returnEnergy(){
		return currentEnergy;
	}
	
	public bool hasWon(){
		return isWinner;
	}

	public void unpausePlayer(){
		//print ("DING DING DING DING");
		//print(gameObject.tag);
		//print (gameIsUnpaused);
		gameIsUnpaused = true;
		//print (gameIsUnpaused);
	}
	public void pausePlayer(){
		//print("ding");
		gameIsUnpaused = false;
	}
	
    // Start is called before the first frame update
    void Start(){
		//Set paused state according to if game is starting paused or not.
		gameIsUnpaused = !startGamePaused;
		
		//Acquire the character's rigid body for later reference.
        character = gameObject.GetComponent<Rigidbody2D>();
		currentEnergy = startingEnergy;
		
		if (gameObject.tag == "Player1Owned"){
			horControlString = "Horizontal";
			vertControlString = "Vertical";
			impulseControlString = "p1Fire";
			areaControlString = "p1CreateAreaBall";
			minControlString = "p1CreateMinBall";
			posControlString = "p1CreatePosBall";
			energyText.text = "Player 1 Energy:";
			areaText.text = "Player 1 Area:";
		}
		else if (gameObject.tag == "Player2Owned"){
			horControlString = "Horizontal2";
			vertControlString = "Vertical2";
			impulseControlString = "p2Fire";
			areaControlString = "p2CreateAreaBall";
			minControlString = "p2CreateMinBall";
			posControlString = "p2CreatePosBall";
			energyText.text = "Player 2 Energy:";
			areaText.text = "Player 2 Area:";
		}
		else{
			//Destroy untagged player gameobjects because they will fuck everything
			print ("Player Object Is Untagged and Fucked");
			Destroy(gameObject);
		}
		
		//Start the win check coroutine
		StartCoroutine("WinCheck");
		
		//Start the energy regen routine
		StartCoroutine("EnergyPerSecond");

        musicPlayer = GameObject.Find("MusicPlayer");
    }
	
	//Coroutine for checking the winner
	private IEnumerator WinCheck(){
		while(true){
			yield return new WaitForSeconds(1.0f);
			if (gameIsUnpaused){
				//Sum up area
				foreach (Transform child in transform){
					if(child.tag == "AreaBall"){
						tempArea += child.GetComponent<AreaBallScript>().calculateArea();
					}
				}
			
				//Set currently controlled area in ui
				//print(tempArea);
				areaBar.value = tempArea/areaToCoverToWin;
			
				//Check for win, if not reset temp variable.
				if(tempArea >= areaToCoverToWin){
                    musicPlayer.GetComponent<MusicPlayer>().PostWwiseEndGame();
                    musicPlayer.GetComponent<MusicPlayer>().PostWwisePlayAmbience();
                    isWinner = true;
					//print(gameObject.tag + " WINS");
				
					if (gameObject.tag == "Player1Owned"){
						//Set win text, wait for 2 seconds and reload scene.
						winText.text = "Player 2 Wins";
					}
					else{
						winText.text = "Player 1 Wins";
					}
					tempArea = 0;
					yield return new WaitForSeconds(timeBeforeReset);
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				}
				else{
					//print(tempArea);
					tempArea = 0;
				}
			}
		}
	}
	
	private IEnumerator EnergyPerSecond(){
		while(true){
			yield return new WaitForSeconds(1.0f);
			if (gameIsUnpaused){
				if (currentEnergy+passiveEnergyPerSecond <= maxEnergy){
					currentEnergy += passiveEnergyPerSecond;
				}
				else{
					currentEnergy = maxEnergy;
				}
				fillBar.value = currentEnergy/maxEnergy;
			}
		}
	}

    // Update is called once per frame
    void Update(){
		if (Input.GetButtonDown("MainMenu") & tag == "Player1Owned" & gameIsUnpaused)
        {
            musicPlayer.GetComponent<MusicPlayer>().PostWwisePlayAmbience();
            musicPlayer.GetComponent<MusicPlayer>().PostWwiseStopMusic();
            Instantiate(mainMenu);
		}
		if (gameIsUnpaused){
			//Player Object Movement vector
			moveDirection = new Vector3(Input.GetAxis(horControlString), Input.GetAxis(vertControlString), 0.0f);
			//Get change in position
			moveDirection  *= moveSpeed;
		
			//Add change in position to present position
			newPos = new Vector3(character.position.x + moveDirection.x * Time.deltaTime, character.position.y + moveDirection.y * Time.deltaTime, 0);
			//Update position
			character.MovePosition(newPos);
		
			//Impulse timing
			myTime += Time.deltaTime;
			//Impulse things
			if (Input.GetButtonDown(impulseControlString) && myTime > impulseCooldown){
				//Set impulse cooldown
				nextImpulse = myTime + impulseCooldown;
				//call method from ParticleManager script
				gameObject.GetComponent<ParticleManager>().PlayImpulseParticles();
				//call method from AudioManager script
				gameObject.GetComponent<AudioManager>().PostImpulseWwiseEvent();
				//Apply impulse to balls using nested foreach loops
				foreach (string inputString in thingsToImpulse) {
					foreach (GameObject ball in GameObject.FindGameObjectsWithTag(inputString)){
						//Get mouse position, use to calculation vector from mouse to sphere.
						playerPos = new Vector3(gameObject.GetComponent<Rigidbody2D>().position.x, gameObject.GetComponent<Rigidbody2D>().position.y,0);
						ballPos = new Vector3(ball.GetComponent<Rigidbody2D>().position.x, ball.GetComponent<Rigidbody2D>().position.y, 0);
						impForceDir = ballPos - playerPos;
						//Normalize vector to avoid fuckery
						impForceDir.Normalize();
						//Multiply direction with force to get force vector
						impForce = impForceDir * (float)(impulseForce / Math.Pow(powerBase, Vector3.Distance(playerPos, ballPos)));
						//Apply impulse to sphere
						if (Vector3.Distance(playerPos, ballPos) < impulseDistance){
							ball.GetComponent<Rigidbody2D>().AddForce(impForce, ForceMode2D.Impulse);
						}
					}
				}
				//More impulse cooldown matters
				nextImpulse = nextImpulse - myTime;
				myTime = 0.0f;
			}	
		
			//Code for creating particles.
			//Handles creating area control particles:
			if (Input.GetButtonDown(areaControlString) && myTime > createCooldown && currentEnergy >= areaBallCost){
				//Check if any area control orbs are too close.
				foreach (GameObject child in GameObject.FindGameObjectsWithTag("AreaBall")){
					if ((Vector3.Distance(child.transform.position, gameObject.transform.position) < child.GetComponent<AreaBallScript>().controlRadius)){
						tooClose = true;
					}
				}
				//If there are no area orbs too close create new one
				if(!tooClose){
                    gameObject.GetComponent<AudioManager>().PostWwiseCreateTerritory();
                    nextCreate = myTime + createCooldown;
					Instantiate(AreaBallPrefab,gameObject.transform);
					nextCreate = nextCreate - myTime;
					myTime = 0.0f;
					currentEnergy -= areaBallCost;
					//print (gameObject.tag + " Area Ball Created, Current Energy: " + currentEnergy);
				}
				//Reset tooClose variable to false state.
				tooClose = false;
			}
			//Handles creating pos and min particles
			//Create a new prefab object with the playerobject as its parent.
			//After creating resets the cooldown
			else if(Input.GetButtonDown(posControlString)  && myTime > createCooldown && currentEnergy >= posMinBallCost){
                gameObject.GetComponent<AudioManager>().PostCreatePlusMin();
                nextCreate = myTime + createCooldown;
				if(gameObject.tag == "Player1Owned"){
					Instantiate(P1PosPrefab,gameObject.transform);
				}
				else if (gameObject.tag == "Player2Owned"){
					Instantiate(P2PosPrefab,gameObject.transform);
				}
				nextCreate = nextCreate - myTime;
				myTime = 0.0f;
				currentEnergy -= posMinBallCost;
				//print (gameObject.tag + " Positive Particle Created, Current Energy: " + currentEnergy);
			}
			else if(Input.GetButtonDown(minControlString) && myTime > createCooldown && currentEnergy >= posMinBallCost){
                gameObject.GetComponent<AudioManager>().PostCreatePlusMin();
                nextCreate = myTime + createCooldown;
				if(gameObject.tag == "Player1Owned"){
					Instantiate(P1MinPrefab,gameObject.transform);
				}
				else if (gameObject.tag == "Player2Owned"){
					Instantiate(P2MinPrefab,gameObject.transform);
				}
				nextCreate = nextCreate - myTime;
				myTime = 0.0f;
				currentEnergy -= posMinBallCost;
				//print (gameObject.tag + " Minus Particle Created, Current Energy: " + currentEnergy);
			}
		}
    }
}