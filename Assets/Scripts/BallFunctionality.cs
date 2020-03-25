using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Script based on unity documentation example.
public class BallFunctionality : MonoBehaviour
{
	//Rigidbody2D rb;
	Vector3 newForce;
	Vector3 forceDir;
	Vector3 mousePos;
	Vector3 ballPos;
	float objDistance;
	[SerializeField]
	public float impulseForce = 300;
	public float repelForce = 1;
	public float attractForce = 1;
	public float powerBase = 2;
	public float pushrepelDistance = 4;
    // Start is called before the first frame update
    void Start()
    {
		//Get the rb component attached to sphere.
        //rb = GetComponent<Rigidbody2D>();
		
		//Initialization nonsense
    }

    // Update is called once per frame
    void Update()
    {
		// Code for pushing balls away from mouse when mouse is pressed.
        if (Input.GetMouseButtonDown(0)){
			foreach (GameObject ball in GameObject.FindGameObjectsWithTag("PosBall")){
				//Get mouse position, use to calculation vector from mouse to sphere.
				mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				ballPos = new Vector3 (ball.GetComponent<Rigidbody2D>().position.x, ball.GetComponent<Rigidbody2D>().position.y,0);
				forceDir = ballPos - new Vector3(mousePos.x, mousePos.y, 0) ;
				//Normalize vector to avoid fuckery
				forceDir.Normalize();
				//Multiply direction with force to get force vector
				newForce = forceDir * (float)(impulseForce/Math.Pow(powerBase,Vector3.Distance(mousePos,ballPos)));
				//Apply impulse to sphere
				ball.GetComponent<Rigidbody2D>().AddForce(newForce,ForceMode2D.Impulse);
			}
			foreach (GameObject ball in GameObject.FindGameObjectsWithTag("MinusBall")){
				//Get mouse position, use to calculation vector from mouse to sphere.
				mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				ballPos = new Vector3 (ball.GetComponent<Rigidbody2D>().position.x, ball.GetComponent<Rigidbody2D>().position.y,0);
				forceDir = ballPos - new Vector3(mousePos.x, mousePos.y, 0) ;
				//Normalize vector to avoid fuckery
				forceDir.Normalize();
				//Multiply direction with force to get force vector
				newForce = forceDir * (float)(impulseForce/Math.Pow(powerBase,Vector3.Distance(mousePos,ballPos)));
				//Apply impulse to sphere
				ball.GetComponent<Rigidbody2D>().AddForce(newForce,ForceMode2D.Impulse);
			}
		}
		//Balls of the same type repel and different types pull eachother
		foreach (GameObject ball in GameObject.FindGameObjectsWithTag("MinusBall")){
			objDistance = Vector3.Distance(ball.GetComponent<Rigidbody2D>().position, gameObject.GetComponent<Rigidbody2D>().position);
			if(ball != gameObject && objDistance < pushrepelDistance){
				forceDir = gameObject.GetComponent<Rigidbody2D>().position - ball.GetComponent<Rigidbody2D>().position;
				forceDir.Normalize();
				//Repel
				if (gameObject.tag == "MinusBall"){	
					newForce = -1 * forceDir * (float)(repelForce/Math.Pow(powerBase,objDistance));
					ball.GetComponent<Rigidbody2D>().AddForce(newForce, ForceMode2D.Force);
				}
				//Attract
				if (gameObject.tag == "PosBall"){
					newForce = forceDir * (float)(attractForce/Math.Pow(powerBase,objDistance));
					ball.GetComponent<Rigidbody2D>().AddForce(newForce, ForceMode2D.Force);
				}
			}
		}
		foreach (GameObject ball in GameObject.FindGameObjectsWithTag("PosBall")){
			objDistance = Vector3.Distance(ball.GetComponent<Rigidbody2D>().position, gameObject.GetComponent<Rigidbody2D>().position);
			if(ball != gameObject && objDistance < pushrepelDistance){
				forceDir = gameObject.GetComponent<Rigidbody2D>().position - ball.GetComponent<Rigidbody2D>().position;
				forceDir.Normalize();
				//Repel
				if (gameObject.tag == "PosBall"){	
					newForce = -1 * forceDir * (float)(repelForce/Math.Pow(powerBase,objDistance));
					ball.GetComponent<Rigidbody2D>().AddForce(newForce, ForceMode2D.Force);
				}
				//Attract
				if (gameObject.tag == "MinusBall"){
					newForce = forceDir * (float)(attractForce/Math.Pow(powerBase,objDistance));
					ball.GetComponent<Rigidbody2D>().AddForce(newForce, ForceMode2D.Force);
				}
			}
		}
    }
	
	void OnCollisionEnter2D(Collision2D collision){
		if (gameObject.tag == "MinusBall" && collision.collider.tag == "PosBall"){
			//print("Colliding");
			Destroy(gameObject);
		}
		if (gameObject.tag == "PosBall" && collision.collider.tag == "MinusBall"){
			//print("Colliding");
			Destroy(gameObject);
		}
		if (collision.collider.tag == "Hole"){
			Destroy(gameObject);
		}
	}
}