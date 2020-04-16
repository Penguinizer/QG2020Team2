using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDisplay : MonoBehaviour
{
	public Text energyText;
	private GameObject Player1;
	private GameObject Player2;
    // Start is called before the first frame update
    void Start()
    {
        Player1 = GameObject.FindGameObjectsWithTag("Player1Owned")[0];
		Player2 = GameObject.FindGameObjectsWithTag("Player2Owned")[0];
		//print(Player1);
		//print(Player2);
    }

    // Update is called once per frame
    void Update()
    {
        energyText.text = "Player 1 Energy: " + Player1.GetComponent<CharacterControl>().returnEnergy() + "\nPlayer 2 Energy: " + Player2.GetComponent<CharacterControl>().returnEnergy();
    }
}
