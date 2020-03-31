using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	bool sakuSandbox;
	[SerializeField]
	bool background;
	
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color=Color.black;
    }

	void OnMouseEnter(){
		GetComponent<Renderer>().material.color = Color.red;
	}
	
	void OnMouseExit(){
		GetComponent<Renderer>().material.color=Color.black;
	}
     
	 void OnMouseUp(){
		 if (sakuSandbox){
			 Application.LoadLevel("Saku_Sandbox");
		 }
		 if (background){
			 Application.LoadLevel("Background");
		 }
	 }
}
