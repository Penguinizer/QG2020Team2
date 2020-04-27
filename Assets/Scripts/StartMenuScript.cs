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
		descriptionText = new GUIContent("\nEscape to re-open this menu and pause \n\n The goal of the game is to gather photons created by annihilating particles to gain energy.\n This energy is used to create area control particles. Create enough area control particles to win.\n\n Annihilations create two entangled photons which we do not know the location of.\n Your particles can gather these photons by colliding into them in the area.\n When you collide with a photon the entangled photon's location is also revealed.");
		controlIMG = new GUIContent(controlImage);
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor=Color.white;
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect((Screen.width / 2) - 300, (Screen.height / 2) - 300, 600, 600));
        GUI.Box(new Rect(0, 0, 600, 600), "Controls:");
        GUI.Box(new Rect(0, 0, 600, 200), controlIMG, style);
        GUI.Box(new Rect(100, 200, 400, 200), descriptionText, style);

        if (GUI.Button(new Rect(200, 400, 200, 100), "Press To Start Game"))
        {
            GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>().PostWwisePlayMusic();
            unpausePlayers();
            Destroy(gameObject);
        }
        if (GUI.Button(new Rect(100, 500, 150, 100), "Press To Exit Game"))
        {
            Application.Quit();
        }
        if (GUI.Button(new Rect(250, 500, 150, 100), "Press to Restart Game"))
        {
            GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>().PostWwiseStopMusic();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
