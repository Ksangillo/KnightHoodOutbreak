using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int value;
    AudioSource pickupAudio;
    // Start is called before the first frame update
    void Start()
    {
        pickupAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (pickupAudio) 
            AudioSource.PlayClipAtPoint(pickupAudio.clip, gameObject.transform.position, pickupAudio.volume);
            Destroy(gameObject);
            CoinCounterScript.instance.IncreaseCoins(value);
        }
    }
}
