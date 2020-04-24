﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StartMenuScript : MonoBehaviour
{
	[SerializeField]
	private GameObject player1;
	[SerializeField]
	private GameObject player2;

	
	GUIContent content;
	
	void unpausePlayers(){
		player1.GetComponent<CharacterControl>().unpausePlayer();
		player2.GetComponent<CharacterControl>().unpausePlayer();
	}
	
    // Start is called before the first frame update
    void Start()
    {
		content = new GUIContent("Player 1 Controls:\n Movement: Arrow Keys.\n Impulse: Numpad 1. Minus Particle Numpad 2. Positive Particle: Numpad 3.\n Area Control Particle: Numpad 0. \n\nPlayer 2 Controls:\n Movement: WASD. Impulse: Z. Minus Particle: X. Positive Particle: X.\n Area Control Particle V.\n\n ESC quits the game \n\n\n\n The goal of the game is to gather photons created by annihilating particles to gain energy.\n This energy is used to create area control particles. Create enough area control particles to win.");
    }
	
	void OnGUI(){
		GUI.Box(new Rect((Screen.width/2)-300, (Screen.height/2)-300, 600, 600), content);
		
		if(GUI.Button(new Rect((Screen.width/2)-100, (Screen.height/2)+100, 200, 100), "Press To Start Game")){
			unpausePlayers();
			Destroy(gameObject);
		}
		if(GUI.Button(new Rect((Screen.width/2)-100, (Screen.height/2)+200, 200, 100), "Press To Exit Game")){
			Application.Quit();
		}
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit")){
			unpausePlayers();
			Destroy(gameObject);
		}
		if (Input.GetButtonDown("Escape")){
			Application.Quit();
		}
    }
}
