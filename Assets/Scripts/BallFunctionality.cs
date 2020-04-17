using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Script based on unity documentation example.
public class BallFunctionality : MonoBehaviour{

    public bool destroyedByHole = false;
    //Rigidbody2D rb;
    private Vector3 attRepForce;
    private Vector3 impForce;
    private Vector3 attRepForceDir;
    private Vector3 impForceDir;
    private Vector3 mousePos;
    private Vector3 upMousePos;
    private Vector3 ballPos;
    private float objDistance;
	private Vector3 eBPos;
	private string[] thingsToImpulse = new string[] {"PosBall", "MinusBall"};
	private float pushOrRepel = 1;
    //Editor editable values
    [SerializeField]
    float impulseDistance = 5;
    [SerializeField]
    float impulseForce = 2;
    [SerializeField]
    float attractForce = 1;
    [SerializeField]
    float powerBase = 2;
    [SerializeField]
    float pushrepelDistance = 4;
	[SerializeField]
	bool ffToggle = false;
	[SerializeField]
	bool BlackHoleDestroyBasedOnMaterial = true;
	[SerializeField]
	GameObject EnergyBallPrefab;
	
    // Start is called before the first frame update
    void Start(){
        //Get the rb component attached to sphere.
        //rb = GetComponent<Rigidbody2D>();

        //Initialization nonsense
    }

    // Update is called once per frame
    void Update(){
        // Code for pushing balls away from mouse when mouse is pressed.
        if (Input.GetMouseButtonDown(0)){
			foreach (string inputString in thingsToImpulse){
				foreach (GameObject ball in GameObject.FindGameObjectsWithTag(inputString)){
					//Get mouse position, use to calculation vector from mouse to sphere.
					var v3 = Input.mousePosition;
					v3.z = 10;
					mousePos = Camera.main.ScreenToWorldPoint(v3);
					ballPos = new Vector3(ball.GetComponent<Rigidbody2D>().position.x, ball.GetComponent<Rigidbody2D>().position.y, 0);
					impForceDir = ballPos - mousePos;
					//Normalize vector to avoid fuckery
					impForceDir.Normalize();
					//Multiply direction with force to get force vector
					impForce = impForceDir * (float)(impulseForce / Math.Pow(powerBase, Vector3.Distance(mousePos, ballPos)));
					//impForce = impForceDir * impulseForce;
					//print ("impulseForce:" + impulseForce);
					//print ("iimpForceDir:" + impForceDir);
					//print("impforce:" + impForce);
					//print(Vector3.Distance(upMousePos,ballPos));
					//Apply impulse to sphere
					if (Vector3.Distance(mousePos, ballPos) < impulseDistance){
                    ball.GetComponent<Rigidbody2D>().AddForce(impForce, ForceMode2D.Impulse);
					}
                }
            }
        }
        //Balls of the same type repel and different types pull eachother
		foreach (string inputString in thingsToImpulse){
			foreach (GameObject ball in GameObject.FindGameObjectsWithTag(inputString)){
				objDistance = Vector3.Distance(ball.GetComponent<Rigidbody2D>().position, gameObject.GetComponent<Rigidbody2D>().position);
				if (ball != gameObject && objDistance < pushrepelDistance){
					attRepForceDir = gameObject.GetComponent<Rigidbody2D>().position - ball.GetComponent<Rigidbody2D>().position;
					attRepForceDir.Normalize();
				
					if (gameObject.tag == inputString){
						pushOrRepel = -1;
					}
					else{
						pushOrRepel = 1;
					}
					
					attRepForce = pushOrRepel * attRepForceDir * (float)(attractForce / Math.Pow(powerBase, objDistance));
					ball.GetComponent<Rigidbody2D>().AddForce(attRepForce, ForceMode2D.Force);
				}
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
		//For collisions between particles.
		//Includes spawning energy ball on annihilation
        if (((gameObject.tag == "PosBall" && collision.collider.tag == "MinusBall")|(gameObject.tag == "MinusBall" && collision.collider.tag == "PosBall")) && ((gameObject.transform.parent.tag != collision.collider.transform.parent.tag) | ffToggle)){
            //print("Colliding");
            Destroy(gameObject);
			//eBPos = new Vector3 (gameObject.GetComponent<Rigidbody2D>().position.x, gameObject.GetComponent<Rigidbody2D>().position.y, 0);
			//Create energyball where the collision happened
			eBPos = collision.contacts[0].point;
			Instantiate(EnergyBallPrefab, eBPos, new Quaternion(0,0,0,0));
        }
		//For collisions with black hole. Includes an if for two ways of handling black holes (either via tag or material)
		if(BlackHoleDestroyBasedOnMaterial){
			if (collision.collider.sharedMaterial){
				if (collision.collider.sharedMaterial.name == "blaghoulmatter"){
					Destroy(gameObject);
				}
			}
		}
		else{
			if(collision.collider.tag=="Hole"){
				Destroy(gameObject);
			}
		}
		//For collision with energy ball and adding energy to the player.
		if(collision.collider.tag == "Energyball"){
			//Call the function to add energy to player 
			gameObject.transform.parent.gameObject.GetComponent<CharacterControl>().addEnergy();
		}
    }
}