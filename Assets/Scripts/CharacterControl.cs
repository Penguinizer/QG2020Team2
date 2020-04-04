using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Impulse timing based on documentation script reference for arcade firing.
public class CharacterControl : MonoBehaviour
{	
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
	
	private float myTime = 0.0f;
	private float nextImpulse = 0.0f;
	private Vector3 impForce;
	private Vector3 impForceDir;
	private Rigidbody2D character;
	private Vector3 ballPos;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 playerPos = Vector3.zero;
	private Vector3 newPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
		//Acquire the character's rigid body for later reference.
        character = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		//Player Object Movement vector
		if (gameObject.tag == "Player1Owned"){
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
		}
		if (gameObject.tag == "Player2Owned"){
			moveDirection = new Vector3(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"), 0.0f);
		}
		//Get change in position
		moveDirection  *= moveSpeed;
		
		//Add change in position to present position
		newPos = new Vector3(character.position.x + moveDirection.x * Time.deltaTime, character.position.y + moveDirection.y * Time.deltaTime, 0);
		//Update position
		character.MovePosition(newPos);
		
		//Impulse timing
		myTime += Time.deltaTime;
		//Impulse things
		if (gameObject.tag == "Player1Owned" && Input.GetButton("p1Fire") && myTime > impulseCooldown){
			//Set impulse cooldown
			nextImpulse = myTime + impulseCooldown;
			//Apply impulse to balls
			foreach (GameObject ball in GameObject.FindGameObjectsWithTag("PosBall")){
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
			foreach (GameObject ball in GameObject.FindGameObjectsWithTag("MinusBall")){
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
			
			//More impulse cooldown matters
			nextImpulse = nextImpulse - myTime;
			myTime = 0.0f;
		}
		if (gameObject.tag == "Player2Owned" && Input.GetButton("p2Fire") && myTime > impulseCooldown){
			//Set impulse cooldown
			nextImpulse = myTime + impulseCooldown;
			//Apply impulse to balls
			foreach (GameObject ball in GameObject.FindGameObjectsWithTag("PosBall")){
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
			foreach (GameObject ball in GameObject.FindGameObjectsWithTag("MinusBall")){
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
			//More impulse cooldown matters
			nextImpulse = nextImpulse - myTime;
			myTime = 0.0f;
		}
    }
}
