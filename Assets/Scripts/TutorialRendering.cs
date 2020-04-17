using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialRendering : MonoBehaviour
{
	[SerializeField]
	Text tutorialText;
	
	private BoxCollider2D uiCollider;
	//private Color origColor;
	//private Color transColor;
	
    // Start is called before the first frame update
    void Start()
    {
		//Set the variable for the colors
		//origColor = tutorialText.material.color;
		//transColor = new Color(origColor.r, origColor.g,origColor.b, 0.01f);
    }

	//Fade text on collision
	void OnTriggerEnter2D(Collider2D collision){
		//print("PONG");
		//tutorialText.material.color = transColor;
		tutorialText.text = "";
	}
	
	void OnTriggerExit2D(Collider2D collision){
		//print("PING");
		//tutorialText.material.color = origColor;
		tutorialText.text = "Player 1 Controls: Movement: Arrow Keys. Impulse: Numpad 1. Minus Particle Numpad 2. Positive Particle: Numpad 3. Area Control Particle: Numpad 0.\nPlayer 2 Controls: Movement: WASD. Impulse: Z. Minus Particle: X. Positive Particle: X. Area Control Particle V.";
	}
}
