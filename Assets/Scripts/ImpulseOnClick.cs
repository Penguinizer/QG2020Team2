﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Script based on unity documentation example.
public class ImpulseOnClick : MonoBehaviour
{
	//Rigidbody2D rb;
	Vector3 newForce;
	Vector3 forceDir;
	Vector3 mousePos;
	Vector3 ballPos;
	float force;
	double powerBase;
    // Start is called before the first frame update
    void Start()
    {
		//Get the rb component attached to sphere.
        //rb = GetComponent<Rigidbody2D>();
		
		//Initialization nonsense
		force = 300;
		powerBase = 2;
    }

    // Update is called once per frame
    void Update()
    {
		// Figure out if mouse was pressed
        if (Input.GetMouseButtonDown(0)){
			foreach (GameObject ball in GameObject.FindGameObjectsWithTag("PhysAffected")){
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
}