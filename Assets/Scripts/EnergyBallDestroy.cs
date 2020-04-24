using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallDestroy : MonoBehaviour{
	void OnCollisionEnter2D(Collision2D collision){
		//Destroy the energyball if it collides with a posball or minusball
		if (collision.collider.tag == "Player1Owned" | collision.collider.tag == "Player2Owned"){
			//print("ding");
			collision.collider.GetComponent<CharacterControl>().addEnergy();
			Destroy(gameObject);
		}
	}
}
