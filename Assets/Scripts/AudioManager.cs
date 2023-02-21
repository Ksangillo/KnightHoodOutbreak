using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] soundFX;
    public static AudioManager instance;

    private void Awake()//singelton pattern
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);//destory objects
        }
        DontDestroyOnLoad(this.gameObject);//saves the first one
    }
}
