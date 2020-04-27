using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallDestroy : MonoBehaviour{
	void OnCollisionEnter2D(Collision2D collision){
		//Destroy the energyball if it collides with a posball or minusball
		if (collision.collider.tag == "PosBall" | collision.collider.tag == "MinusBall"){
            //print("ding");
            gameObject.GetComponent<AudioManager>().PostWwiseCollectPhoton();
			collision.collider.transform.parent.GetComponent<CharacterControl>().addEnergy();
			Destroy(gameObject);
		}
	}
}
