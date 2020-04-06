using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleManager : MonoBehaviour
{

   [SerializeField] GameObject annihilationParticles;
    [SerializeField] ParticleSystem impulseParticles;

    public bool ffToggle = true;
    [SerializeField] bool isUnit = true;
    [SerializeField] bool isController = false;
    

    private float myTime = 0.0f;
    [SerializeField] float impulseCooldown = 3f;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;

        if (gameObject.tag == "Player1Owned" && Input.GetButton("p1Fire") && myTime > impulseCooldown && isController && !isUnit)
        {

            impulseParticles.Play();
        }

        if (gameObject.tag == "Player2Owned" && Input.GetButton("p2Fire") && myTime > impulseCooldown && isController && !isUnit)
        {
            {

                impulseParticles.Play();
            }

        }

    }

      
        void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "MinusBall" && collision.collider.tag == "PosBall" && (gameObject.transform.parent.tag != collision.collider.transform.parent.tag | ffToggle))
        {
            print("Play annihilation particles");
            Instantiate(annihilationParticles, transform.position, transform.rotation);


        }
        if (gameObject.tag == "PosBall" && collision.collider.tag == "MinusBall" && (gameObject.transform.parent.tag != collision.collider.transform.parent.tag | ffToggle))
        {

            print("Play annihilation particles");
            Instantiate(annihilationParticles, transform.position, transform.rotation);

        }
        if (collision.collider.tag == "Hole")
        {
            print("Play blackhole suck particles");
        }


    }
}
