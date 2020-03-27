using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Script based on unity documentation example.
public class BallFunctionality : MonoBehaviour
{
    //Rigidbody2D rb;
    Vector3 attRepForce;
    Vector3 impForce;
    Vector3 attRepForceDir;
    Vector3 impForceDir;
    Vector3 mousePos;
    Vector3 upMousePos;
    Vector3 ballPos;
    float objDistance;
    //Editor editable values
    [SerializeField]
    float impulseDistance = 5;
    [SerializeField]
    float impulseForce = 2;
    [SerializeField]
    float repelForce = 1;
    [SerializeField]
    float attractForce = 1;
    [SerializeField]
    float powerBase = 2;
    [SerializeField]
    float pushrepelDistance = 4;
	[SerializeField]
	bool ffToggle = false;
    // Start is called before the first frame update
    void Start()
    {
        //Get the rb component attached to sphere.
        //rb = GetComponent<Rigidbody2D>();

        //Initialization nonsense
    }

    // Update is called once per frame
    void Update()
    {
        // Code for pushing balls away from mouse when mouse is pressed.
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject ball in GameObject.FindGameObjectsWithTag("PosBall"))
            {
                //Get mouse position, use to calculation vector from mouse to sphere.
                var v3 = Input.mousePosition;
                v3.z = 10;
                mousePos = Camera.main.ScreenToWorldPoint(v3);
                ballPos = new Vector3(ball.GetComponent<Rigidbody2D>().position.x, ball.GetComponent<Rigidbody2D>().position.y, 0);
                impForceDir = ballPos - mousePos;
                //Normalize vector to avoid fuckery
                impForceDir.Normalize();
                //Multiply direction with force to get force vector
                impForce = impForceDir * (float)(impulseForce / Math.Pow(powerBase, Vector3.Distance(mousePos, ballPos)));
                //impForce = impForceDir * impulseForce;
                //print ("impulseForce:" + impulseForce);
                //print ("iimpForceDir:" + impForceDir);
                //print("impforce:" + impForce);
                //print(Vector3.Distance(upMousePos,ballPos));
                //Apply impulse to sphere
                if (Vector3.Distance(mousePos, ballPos) < impulseDistance)
                {
                    ball.GetComponent<Rigidbody2D>().AddForce(impForce, ForceMode2D.Impulse);
                }
            }
            foreach (GameObject ball in GameObject.FindGameObjectsWithTag("MinusBall"))
            {
                //Get mouse position, use to calculation vector from mouse to sphere.
                var v3 = Input.mousePosition;
                v3.z = 10;
                mousePos = Camera.main.ScreenToWorldPoint(v3);
                ballPos = new Vector3(ball.GetComponent<Rigidbody2D>().position.x, ball.GetComponent<Rigidbody2D>().position.y, 0);
                impForceDir = ballPos - mousePos;
                //Normalize vector to avoid fuckery
                impForceDir.Normalize();
                //Multiply direction with force to get force vector
                impForce = impForceDir * (float)(impulseForce / Math.Pow(powerBase, Vector3.Distance(mousePos, ballPos)));
                //impForce = impForceDir * impulseForce;
                //Apply impulse to sphere
                if (Vector3.Distance(mousePos, ballPos) < impulseDistance)
                {
                    ball.GetComponent<Rigidbody2D>().AddForce(impForce, ForceMode2D.Impulse);
                }
            }
        }
        //Balls of the same type repel and different types pull eachother
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("MinusBall"))
        {
            objDistance = Vector3.Distance(ball.GetComponent<Rigidbody2D>().position, gameObject.GetComponent<Rigidbody2D>().position);
            if (ball != gameObject && objDistance < pushrepelDistance)
            {
                attRepForceDir = gameObject.GetComponent<Rigidbody2D>().position - ball.GetComponent<Rigidbody2D>().position;
                attRepForceDir.Normalize();
                //Repel
                if (gameObject.tag == "MinusBall")
                {
                    attRepForce = -1 * attRepForceDir * (float)(repelForce / Math.Pow(powerBase, objDistance));
                    ball.GetComponent<Rigidbody2D>().AddForce(attRepForce, ForceMode2D.Force);
                }
                //Attract
                if (gameObject.tag == "PosBall")
                {
                    attRepForce = attRepForceDir * (float)(attractForce / Math.Pow(powerBase, objDistance));
                    ball.GetComponent<Rigidbody2D>().AddForce(attRepForce, ForceMode2D.Force);
                }
            }
        }
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("PosBall"))
        {
            objDistance = Vector3.Distance(ball.GetComponent<Rigidbody2D>().position, gameObject.GetComponent<Rigidbody2D>().position);
            if (ball != gameObject && objDistance < pushrepelDistance)
            {
                attRepForceDir = gameObject.GetComponent<Rigidbody2D>().position - ball.GetComponent<Rigidbody2D>().position;
                attRepForceDir.Normalize();
                //Repel
                if (gameObject.tag == "PosBall")
                {
                    attRepForce = -1 * attRepForceDir * (float)(repelForce / Math.Pow(powerBase, objDistance));
                    ball.GetComponent<Rigidbody2D>().AddForce(attRepForce, ForceMode2D.Force);
                }
                //Attract
                if (gameObject.tag == "MinusBall")
                {
                    attRepForce = attRepForceDir * (float)(attractForce / Math.Pow(powerBase, objDistance));
                    ball.GetComponent<Rigidbody2D>().AddForce(attRepForce, ForceMode2D.Force);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "MinusBall" && collision.collider.tag == "PosBall" && (gameObject.transform.parent.tag != collision.collider.transform.parent.tag | ffToggle))
        {
            //print("Colliding");
            Destroy(gameObject);
        }
        if (gameObject.tag == "PosBall" && collision.collider.tag == "MinusBall" && (gameObject.transform.parent.tag != collision.collider.transform.parent.tag | ffToggle))
        {
            //print("Colliding");
            Destroy(gameObject);
        }
        if (collision.collider.tag == "Hole")
        {
            Destroy(gameObject);
        }
    }
}