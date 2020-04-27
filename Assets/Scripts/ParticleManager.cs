using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleManager : MonoBehaviour
{

    [SerializeField] GameObject annihilationParticles;
    [SerializeField] ParticleSystem impulseParticles;

    public bool ffToggle = true;
    [SerializeField] bool isUnit = true;
    //[SerializeField] bool isController = false;
    



    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void PlayImpulseParticles()
    {
        impulseParticles.Play();
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
