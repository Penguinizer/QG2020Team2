using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public float speed;
    public bool ffToggle = true;

    public AK.Wwise.Event Annihilation;
    public AK.Wwise.Event Collision;
    public AK.Wwise.Event Impulse;

    public AK.Wwise.RTPC particleSpeed;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 lastPosition = Vector3.zero;
    void FixedUpdate()
    {
        speed = (transform.position - lastPosition).magnitude / Time.fixedDeltaTime;
        lastPosition = transform.position;
    }

    public void PostImpulseWwiseEvent()
    {
        Impulse.Post(gameObject);
        print("play Impulse sound");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "MinusBall" && collision.collider.tag == "PosBall" && (gameObject.transform.parent.tag != collision.collider.transform.parent.tag | ffToggle))
        {
            print("Play collision sound");
            Annihilation.Post(gameObject);
            
        }
        if (gameObject.tag == "PosBall" && collision.collider.tag == "MinusBall" && (gameObject.transform.parent.tag != collision.collider.transform.parent.tag | ffToggle))
        {
           print("Play collision sound");
            Annihilation.Post(gameObject);

        }
        if (collision.collider.tag == "Hole")
        {
            print("Play black hole sound");
        }

       else if(collision.collider.tag == "PosBall" || collision.collider.tag == "MinusBall")
        {
            particleSpeed.SetValue(gameObject, speed);
            Collision.Post(gameObject);
        }

   

    }
}
