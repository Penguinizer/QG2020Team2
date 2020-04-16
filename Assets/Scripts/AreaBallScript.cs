using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBallScript : MonoBehaviour
{
	//Stuff for editoring
	//Radius of control area
	[SerializeField]
	float controlRadius = 2.0f;
	//The amount of raycast spokes for doing collision checks for control area.
	//Note, raycasts are expensive in terms of performance so having this be a low amount is preferred even if it reduces resolution.
	//Also make 1 less than the amount as there is also one pointing straight up that isn't counted for this variable.
	[SerializeField]
	int amountOfRaycastSpokes = 11;
	
	//A temporary variable for Raycast Hits and a list for containing the distances them.
	private RaycastHit2D tmpHit;
	//private IList<Vector2> hitList = new List<Vector2>();
	private Vector2[] hitList;
	
	//Other assorted variables
	private int raycastAngle;
	private Vector2 tmpLoc;
	private Vector2 vec2Angle;
	
	//Mesh stuff
	private Mesh controlMesh;
	
	//Area stuff
	private float aTemp;
	
    // Start is called before the first frame update
    void Start()
    {
		//Calculate the angle per spoke so that the raycasts make a full rotation.
        raycastAngle = 360 / (amountOfRaycastSpokes+1);
		//Initialize the array for the mesh creation.
		hitList = new Vector2[amountOfRaycastSpokes+1];
		
		//Set the startup hitDistanceList
		//for (int index = 0; index <= amountOfRaycastSpokes; index++){
			//hitDistanceList.Add(controlRadius);
		//}
		
		//Create the control area mesh?
		//controlMesh = new Mesh();
		//controlMesh.vertices = new Vector3[] {new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)};
		//controlMesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
        //controlMesh.triangles =  new int[] {0, 1, 2};
		
		if (gameObject.transform.parent.tag == "Player1Owned"){
			 gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
		}
		else{
			 gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
		}
    }

    // Update is called once per frame
    void Update()
    {
		//Radiate spokes out from the center of the control unit.
		//Check if they don't go through anything within the control radius.
		//If they do write down the distance at which they collide, if not write down the radius.
		//This way the control area won't go through walls.
        for (int index = 0; index <= amountOfRaycastSpokes; index++){
			//Get a location and the Raycast hit
			tmpLoc = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y);
			vec2Angle = (Vector2)(Quaternion.Euler(0,0,raycastAngle*index) * Vector2.right);
			vec2Angle.Normalize();
			tmpHit = Physics2D.Raycast(tmpLoc, vec2Angle, controlRadius);
			//Check if the raycast hit anything. If it did get the distance. If not save controlradius.
			if (tmpHit.collider != null){
				//hitList.Add(tmpHit.point);
				hitList[index]=vec2Angle*(Vector2.Distance(tmpHit.point,tmpLoc))*2;
			}
			else{
				//hitList.Add(tmpLoc+vec2Angle);
				hitList[index]=vec2Angle*controlRadius*2;
			}
		}
		
		//Creating triangles for mesh using Triangulator
		//Created by runevision
		//Available at http://wiki.unity3d.com/index.php?title=Triangulator
		//Usage based on example on wiki.
		Triangulator tr = new Triangulator(hitList);
		int[] indices = tr.Triangulate();
		Vector3[] vertices = new Vector3[hitList.Length];
		for (int i=0; i<vertices.Length;i++){
			vertices[i]=new Vector3(hitList[i].x,hitList[i].y,0);
		}
		
		//Create the new mesh
		Mesh msh = new Mesh();
		msh.vertices = vertices;
		msh.triangles = indices;
		msh.RecalculateNormals();
		msh.RecalculateBounds();
		//Set the new mesh to be the gameobject mesh.
		GetComponent<MeshFilter>().mesh = msh;
		
		//calculateArea();
		
		//Clear the list after it has been used so it's empty for the next update.
		//hitList.Clear();
    }
	
	public float calculateArea(){
		//Calculate the area covered.
		//Implementation of formula for irregular polygons from Wikipedia.
		//Written by fafase
		//Available at: https://answers.unity.com/questions/684909/how-to-calculate-the-surface-area-of-a-irregular-p.html
		float aTemp = 0;
		for (int i=0; i<hitList.Length;i++){
			if (i != hitList.Length-1){
             float mulA = hitList[i].x * hitList[i+1].y;
             float mulB = hitList[i+1].x * hitList[i].y;
             aTemp = aTemp + ( mulA - mulB );
         }else{
             float mulA = hitList[i].x * hitList[0].y;
             float mulB = hitList[0].x* hitList[i].y;
             aTemp = aTemp + ( mulA - mulB );
			}
		}
		aTemp *= 0.5f;
		//print(aTemp);
		return aTemp;
	}
}
