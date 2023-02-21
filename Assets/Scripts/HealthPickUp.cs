using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
   

    AudioSource pickupAudio;
	public int healing = 4;
	public Vector3 spinSpeed = new Vector3(0, 180, 0);
    private void Awake()
    {
		pickupAudio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
	{

		DamageScript damage = collision.GetComponent<DamageScript>();//detects the damagescript on character


		if (damage && CompareTag("Player"))
		{
			bool wasHealed = damage.Heal(healing);
			if (wasHealed)
				if(pickupAudio)
				AudioSource.PlayClipAtPoint(pickupAudio.clip, gameObject.transform.position, pickupAudio.volume);
				Destroy(gameObject);


		}


	}


	private void Update()
	{

		transform.eulerAngles += spinSpeed * Time.deltaTime;

	}




	

}

