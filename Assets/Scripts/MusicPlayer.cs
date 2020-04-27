using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public AK.Wwise.Event PlayMusic;
    public AK.Wwise.Event PlayAmbience;
    public AK.Wwise.Event StartGame;
    public AK.Wwise.Event EndGame;
    public AK.Wwise.Event ClickMenuItem;

    public AK.Wwise.Event StopMusic;
    public AK.Wwise.Event StopAmbience;

    void Start()
    {
        PostWwisePlayAmbience();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void PostWwisePlayAmbience()
    {
        PlayAmbience.Post(gameObject);
    }

    public void PostWwisePlayMusic()
    {
        PlayMusic.Post(gameObject);
    }

    public void PostWwiseStopMusic()
    {
        StopMusic.Post(gameObject);
       
        //PlayMusic.Stop(gameObject);
    }

    public void PostWwiseStopAmbience()
    {
        StopAmbience.Post(gameObject);
    }

    public void PostWwiseStartGame()
    {
        StartGame.Post(gameObject);
    }

    public void PostWwiseEndGame()
    {
        EndGame.Post(gameObject);
    }

    public void PostWwiseClickMenuItem()
    {
        ClickMenuItem.Post(gameObject);
    }
}
