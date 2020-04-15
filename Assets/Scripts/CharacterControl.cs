using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	GameObject PosPartPrefab;
	[SerializeField]
	GameObject MinPartPrefab;
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
	
	//Handles adding energy
	public void addEnergy(){
		currentEnergy += energyOnPickup;
		print(gameObject.tag + " Energy: " + currentEnergy);
	}

    // Start is called before the first frame update
    void Start(){
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
		}
		else if (gameObject.tag == "Player2Owned"){
			horControlString = "Horizontal2";
			vertControlString = "Vertical2";
			impulseControlString = "p2Fire";
			areaControlString = "p2CreateAreaBall";
			minControlString = "p2CreateMinBall";
			posControlString = "p2CreatePosBall";
		}
		else{
			//Destroy untagged player gameobjects because they will fuck everything
			print ("Player Object Is Untagged and Fucked");
			Destroy(gameObject);
		}
    }

    // Update is called once per frame
    void Update(){
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
		if (Input.GetButton(impulseControlString) && myTime > impulseCooldown){
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
		if (Input.GetButton(areaControlString) && myTime > createCooldown && currentEnergy >= areaBallCost){
			nextCreate = myTime + createCooldown;
			Instantiate(AreaBallPrefab,gameObject.transform);
			nextCreate = nextCreate - myTime;
			myTime = 0.0f;
			currentEnergy -= areaBallCost;
			print (gameObject.tag + " Area Ball Created, Current Energy: " + currentEnergy);
		}
		//Handles creating pos and min particles
		//Create a new prefab object with the playerobject as its parent.
		//After creating resets the cooldown
		else if(Input.GetButton(posControlString)  && myTime > createCooldown && currentEnergy >= posMinBallCost){
			nextCreate = myTime + createCooldown;
			Instantiate(PosPartPrefab,gameObject.transform);
			nextCreate = nextCreate - myTime;
			myTime = 0.0f;
			currentEnergy -= posMinBallCost;
			print (gameObject.tag + " Positive Particle Created, Current Energy: " + currentEnergy);
		}
		else if(Input.GetButton(minControlString) && myTime > createCooldown && currentEnergy >= posMinBallCost){
			nextCreate = myTime + createCooldown;
			Instantiate(MinPartPrefab,gameObject.transform);
			nextCreate = nextCreate - myTime;
			myTime = 0.0f;
			currentEnergy -= posMinBallCost;
			print (gameObject.tag + " Minus Particle Created, Current Energy: " + currentEnergy);
		}
    }
}
