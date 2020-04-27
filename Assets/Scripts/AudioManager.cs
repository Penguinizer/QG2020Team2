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
    public AK.Wwise.Event ParticleMove;
    public AK.Wwise.Event WallCollision;
    public AK.Wwise.Event CreatePlusMin;
    public AK.Wwise.Event CreateTerritory;
    public AK.Wwise.Event RevealPhoton;
    public AK.Wwise.Event CollectPhoton;

    public AK.Wwise.RTPC particleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        ParticleMove.Post(gameObject);
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
        particleSpeed.SetValue(gameObject, speed);
    }

    public void PostImpulseWwiseEvent()
    {
        Impulse.Post(gameObject);
        print("play Impulse sound");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

       if(collision.collider.tag == "PosBall" || collision.collider.tag == "MinusBall")
        {
            particleSpeed.SetValue(gameObject, speed);
            Collision.Post(gameObject);
        }

        else
        {
            particleSpeed.SetValue(gameObject, speed);
            WallCollision.Post(gameObject);
        }

    }

    public void PostAnnihilationWwiseEvent()
    {
        print("Play collision sound");
        Annihilation.Post(gameObject);
        ParticleMove.Stop(gameObject);
    }


    public void PostCreatePlusMin()
    {
        CreatePlusMin.Post(gameObject);
    }

    public void PostWwiseCreateTerritory()
    {
        CreateTerritory.Post(gameObject);
    }

    public void PostWwiseRevelPhoton()
    {
        RevealPhoton.Post(gameObject);
    }

    public void PostWwiseCollectPhoton()
    {
        CollectPhoton.Post(gameObject);
    }
}
