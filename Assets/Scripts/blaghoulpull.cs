using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class blaghoulpull : MonoBehaviour
{
	
	Vector3 pullForce;
	Vector3 forceDir;
	Vector3 holePos;
	Vector3 ballPos;
	float holeForce;
	double powerBase;
    // Start is called before the first frame update
    void Start()
    {
        holeForce = 1;
		powerBase = 2;
		holePos = GameObject.FindGameObjectWithTag("Hole").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject ball in GameObject.FindGameObjectsWithTag("MinusBall")){
			ballPos = new Vector3 (ball.GetComponent<Rigidbody2D>().position.x, ball.GetComponent<Rigidbody2D>().position.y,0);
			if (Vector3.Distance(ballPos,holePos)<5.0){
				forceDir=(holePos - ballPos);
				forceDir.Normalize();
				pullForce = forceDir * (float)(holeForce/Math.Pow(powerBase,Vector3.Distance(holePos,ballPos)));
				ball.GetComponent<Rigidbody2D>().AddForce(pullForce, ForceMode2D.Force);
			}
		}
		foreach(GameObject ball in GameObject.FindGameObjectsWithTag("PosBall")){
			ballPos = new Vector3 (ball.GetComponent<Rigidbody2D>().position.x, ball.GetComponent<Rigidbody2D>().position.y,0);
			if (Vector3.Distance(ballPos,holePos)<5.0){
				forceDir=(holePos - ballPos);
				forceDir.Normalize();
				pullForce = forceDir * (float)(holeForce/Math.Pow(powerBase,Vector3.Distance(holePos,ballPos)));
				ball.GetComponent<Rigidbody2D>().AddForce(pullForce, ForceMode2D.Force);
			}
		}
    }
}
