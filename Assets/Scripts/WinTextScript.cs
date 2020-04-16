using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTextScript : MonoBehaviour
{
	public Text winnerText;
	private GameObject Player1;
	private GameObject Player2;
    // Start is called before the first frame update
    void Start()
    {
        Player1 = GameObject.FindGameObjectsWithTag("Player1Owned")[0];
		Player2 = GameObject.FindGameObjectsWithTag("Player2Owned")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Player1.GetComponent<CharacterControl>().hasWon()){
			winnerText.text = "Player 1 Has Won";
		}
		else if(Player2.GetComponent<CharacterControl>().hasWon()){
			winnerText.text = "Player 2 Has Won";
		}
		else{
			winnerText.text = "";
		}
    }
}
