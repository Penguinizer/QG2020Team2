using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public AK.Wwise.Event PlayMusic;
    public AK.Wwise.Event PlayAmbience;

    public AK.Wwise.Event StopMusic;
    public AK.Wwise.Event StopAmbience;

    void Start()
    {
        PlayAmbience.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PostWwisePlayMusic()
    {
        PlayMusic.Post(gameObject);
    }

    public void PostWwiseStopMusic()
    {
        //StopMusic.Post(gameObject);
       
        PlayMusic.Stop(gameObject,10);
    }

    public void PostWwiseStopAmbience()
    {
        StopAmbience.Post(gameObject);
    }
}
