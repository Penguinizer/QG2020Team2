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
	[SerializeField]
	public float force = 300;
	public double powerBase = 2;
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
				newForce = forceDir * (float)(force/Math.Pow(powerBase,Vector3.Distance(mousePos,ballPos)));
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
				newForce = forceDir * (float)(force/Math.Pow(powerBase,Vector3.Distance(mousePos,ballPos)));
				//Apply impulse to sphere
				ball.GetComponent<Rigidbody2D>().AddForce(newForce,ForceMode2D.Impulse);
			}
		}
    }
	
	void OnCollisionEnter2D(Collision2D collision){
		if (gameObject.tag == "MinusBall" && collision.collider.tag == "PosBall"){
			print("Colliding");
			Destroy(gameObject);
		}
		if (gameObject.tag == "PosBall" && collision.collider.tag == "MinusBall"){
			print("Colliding");
			Destroy(gameObject);
		}
	}
}