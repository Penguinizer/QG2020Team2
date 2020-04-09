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
    // Start is called before the first frame update
	[SerializeField]
	float holeForce = 1;
	[SerializeField]
	double powerBase = 2;
	[SerializeField]
	float holeDistance = 5;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject ball in GameObject.FindGameObjectsWithTag("MinusBall")){
			ballPos = new Vector3 (ball.GetComponent<Rigidbody2D>().position.x, ball.GetComponent<Rigidbody2D>().position.y,0);
			holePos = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y,0);
			if (Vector3.Distance(ballPos,holePos)<5.0){
				forceDir=(holePos - ballPos);
				forceDir.Normalize();
				pullForce = forceDir * (float)(holeForce/Math.Pow(powerBase,Vector3.Distance(holePos,ballPos)));
				ball.GetComponent<Rigidbody2D>().AddForce(pullForce, ForceMode2D.Force);
			}
		}
		foreach(GameObject ball in GameObject.FindGameObjectsWithTag("PosBall")){
			ballPos = new Vector3 (ball.GetComponent<Rigidbody2D>().position.x, ball.GetComponent<Rigidbody2D>().position.y,0);
			if (Vector3.Distance(ballPos,holePos)<holeDistance){
				forceDir=(holePos - ballPos);
				forceDir.Normalize();
				pullForce = forceDir * (float)(holeForce/Math.Pow(powerBase,Vector3.Distance(holePos,ballPos)));
				ball.GetComponent<Rigidbody2D>().AddForce(pullForce, ForceMode2D.Force);
			}
		}
    }
}
