using UnityEngine;
using System.Collections;

/*Sphere collision script
 * 
 * If the object leaves the arena or collides with the player delete the object
 * 
 * 
 */

public class SphereCollisionScript : MonoBehaviour {


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "GameController") 
		{
			// vibrate controller and play hit audio
			SteamVR_Controller.Input ((int)collision.gameObject.GetComponentInParent<SteamVR_TrackedObject>().index).TriggerHapticPulse(3999);
			GetComponent<AudioSource> ().Play ();
		}
	}

	// Destroy on collision with player
	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player") 
		{
			Destroy (gameObject);
		}
	}

	//Destroy if leaving the arena
	void OnTriggerExit(Collider collider){
		
		if (collider.tag == "Arena") {
			Destroy (gameObject);
		}
	}
}
