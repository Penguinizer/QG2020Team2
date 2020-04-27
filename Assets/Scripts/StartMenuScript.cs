using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{

	[SerializeField]
	private Texture controlImage;

	
	private GUIContent controlIMG;
	private GUIContent descriptionText;
	private GUIStyle style = new GUIStyle();

    GameObject musicPlayer;
	
	void unpausePlayers(){
		GameObject.FindWithTag("Player1Owned").GetComponent<CharacterControl>().unpausePlayer();
		GameObject.FindWithTag("Player2Owned").GetComponent<CharacterControl>().unpausePlayer();
	}
	
	void pausePlayers(){
		GameObject.FindWithTag("Player1Owned").GetComponent<CharacterControl>().pausePlayer();
		GameObject.FindWithTag("Player2Owned").GetComponent<CharacterControl>().pausePlayer();
	}
    // Start is called before the first frame update
    void Start()
    {
		pausePlayers();
		descriptionText = new GUIContent("\n ESC to open menu \n\n The goal of the game is to gather photons created by annihilation of particles. \n Annihilation happens when particles of different form and color collide. \n By collecting photons you gain energy which is used to create more particles. \n Create enough control enough area to win.\n\n Annihilations create two entangled photons which we do not know the location of.\n Your particles can gather these photons by colliding into them in the set area.\n When you collide with a photon the entangled photon's location is also revealed. \n\n Make impulses to move particles around.");
		controlIMG = new GUIContent(controlImage);
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor=Color.white;

        musicPlayer = GameObject.Find("MusicPlayer");

    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect((Screen.width / 2) - 300, (Screen.height / 2) - 300, 600, 600));
        GUI.Box(new Rect(0, 0, 600, 600), "");
        GUI.Box(new Rect(0, 0, 600, 200), controlIMG, style);
        GUI.Box(new Rect(100, 200, 400, 200), descriptionText, style);

        if (GUI.Button(new Rect(200, 400, 200, 100), "Press To Start Game"))
        {
            musicPlayer.GetComponent<MusicPlayer>().PostWwiseClickMenuItem();
            musicPlayer.GetComponent<MusicPlayer>().PostWwiseStartGame();
            musicPlayer.GetComponent<MusicPlayer>().PostWwisePlayMusic();
            musicPlayer.GetComponent<MusicPlayer>().PostWwiseStopAmbience();

            unpausePlayers();
            Destroy(gameObject);
        }
        if (GUI.Button(new Rect(150, 500, 150, 100), "Press To Exit Game"))
        {
            musicPlayer.GetComponent<MusicPlayer>().PostWwiseClickMenuItem();

            Application.Quit();
        }
        if (GUI.Button(new Rect(300, 500, 150, 100), "Press to Restart Game"))
        {
            musicPlayer.GetComponent<MusicPlayer>().PostWwiseClickMenuItem();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        GUI.EndGroup();
    }

        // Update is called once per frame
        void Update()
    {
        if (Input.GetButtonDown("Submit")){
			unpausePlayers();
			Destroy(gameObject);
		}
		//if (Input.GetButtonDown("Escape")){
		//	Application.Quit();
		//}
    }
}
