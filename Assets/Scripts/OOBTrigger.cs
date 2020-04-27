using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOBTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col){
		col.transform.position = new Vector3 (0,0,0);
	}
}
