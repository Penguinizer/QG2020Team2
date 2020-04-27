using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public AK.Wwise.Event PlayMusic;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PostWwisePlayMusic()
    {
        PlayMusic.Post(gameObject);
    }
}
