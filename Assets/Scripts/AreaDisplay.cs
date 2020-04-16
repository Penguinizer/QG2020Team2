using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaDisplay : MonoBehaviour
{
	public Text areaText;
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
        areaText.text = "Player 1 Area: " + Player1.GetComponent<CharacterControl>().returnArea() +"\nPlayer 2 Area: " + Player2.GetComponent<CharacterControl>().returnArea();
    }
}
