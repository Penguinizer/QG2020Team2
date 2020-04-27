using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PhotonScript : MonoBehaviour{
	//SerializeField variables for things
	[SerializeField]
	private int photonRaycastTrajectorySpokes = 11;
	[SerializeField]
	private float photonSpeed = 1.0f;
	[SerializeField]
	private float photonMaxDistance = 10.0f;
	[SerializeField]
	private GameObject energyBall;
	[SerializeField]
	private float initialPhotonDistance = 1.0f;
	[SerializeField]
	private float initialPhotonPush = 1.0f;
	//[SerializeField]
	//private Material photonMaterial;
	
	//The angle for each spoke
	private float raycastAngle;
	//The randomly selected spoke for the photon
	private float randomRaycastSpoke;
	//List for hits
	private Vector2[] hitList;
	
	//Mesh for stuff
	private Mesh controlMesh;
	
	//Other assorted variables
	private Vector2 tmpLoc;
	private Vector2 vec2Angle;
	//A temporary variable for Raycast Hits and a list for containing the distances them.
	private RaycastHit2D tmpHit;
	
	private float photonDistance;
	
	private LayerMask wallMask;
	
	private IEnumerator photonRandomizer(){
		while(true){
			yield return new WaitForSeconds(0.5f);
			randomRaycastSpoke = UnityEngine.Random.Range(0,photonRaycastTrajectorySpokes);
			//print(randomRaycastSpoke);
		}
	}
	
	private IEnumerator expandPhotonDistance(){
		while(true){
			yield return new WaitForSeconds(0.5f);
			if (photonDistance <= photonMaxDistance){
				photonDistance += photonSpeed;
			}
			for (int index = 0; index <= photonRaycastTrajectorySpokes; index++){
				//Get a location and the angle
				tmpLoc = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y);
				vec2Angle = (Vector2)(Quaternion.Euler(0,0,raycastAngle*index) * Vector2.right);
				vec2Angle.Normalize();
				//Make a wall layer mask
				tmpHit = Physics2D.Raycast(tmpLoc, vec2Angle, photonDistance, wallMask);
				//Check if the raycast hit anything. If it did get the distance. If not save controlradius.
				if (tmpHit.collider != null){
					hitList[index]=vec2Angle*(Vector2.Distance(tmpHit.point,tmpLoc));
				}
				else{
					hitList[index]=vec2Angle*photonDistance;
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
		}
	}
	
    // Start is called before the first frame update
    void Start(){
		raycastAngle = 360/(photonRaycastTrajectorySpokes+1);
		hitList = new Vector2[photonRaycastTrajectorySpokes+1];
		
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
		
		//Create layermasks
		wallMask = LayerMask.GetMask("Walls");
		
		//Set photon distance
		photonDistance = initialPhotonDistance;
		//Start the subroutines
		StartCoroutine("photonRandomizer");
		StartCoroutine("expandPhotonDistance");
		
    }

    // Update is called once per frame
    void Update(){
		
		//Check for if photon hits player.
		tmpLoc = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y);
		vec2Angle = (Vector2)(Quaternion.Euler(0,0,raycastAngle*randomRaycastSpoke) * Vector2.right);
		vec2Angle.Normalize();
		tmpHit = Physics2D.Raycast(tmpLoc, vec2Angle, photonDistance);
		if (tmpHit.collider != null){
			if(tmpHit.collider.tag=="PosBall" | tmpHit.collider.tag=="MinusBall"){
                gameObject.GetComponent<AudioManager>().PostWwiseRevelPhoton();
                //Give energy
                tmpHit.collider.transform.parent.GetComponent<CharacterControl>().addEnergy();
                //Reveal entangled particle opposite of gathered particle              
				GameObject obj = (GameObject) Instantiate(energyBall, tmpLoc + vec2Angle*photonDistance*-0.6f, Quaternion.identity);
				obj.GetComponent<Rigidbody2D>().AddForce(vec2Angle*initialPhotonPush*-1, ForceMode2D.Impulse);
				Destroy(gameObject);
			}
		}
    }
}
