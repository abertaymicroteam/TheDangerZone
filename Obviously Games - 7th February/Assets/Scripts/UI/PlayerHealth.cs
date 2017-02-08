using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	//players health
	public float playerHealth;

	// Object containers
	Text txt;
	AnnouncerScript aScript;

	// Hurt counter and bool
	bool hurtFlag;
	bool gameOverFlag;
	float timer;

	// Use this for initialization
	void Start ()
	{
		playerHealth = 100.0f;

		txt = GameObject.FindObjectOfType<Text>();

		aScript = GameObject.Find ("Announcer").GetComponent<AnnouncerScript> ();

		// Audio parameters
		gameOverFlag = false;
		hurtFlag = false;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		txt.text = "Health: ";
		txt.text += playerHealth.ToString ();

		if (playerHealth <= 0.0f) 
		{
			if (gameOverFlag == false) 
			{
				// Play death audio and set game over text
				if (Random.Range (0.0f, 100.0f) < 50.0f) 
				{
					aScript.whosNext = true;
					gameOverFlag = true;
				} 
				else 
				{
					aScript.moreFight = true;
					gameOverFlag = true;
				}
			}
			
			txt.text = "Game Over";
		}

		if ((timer > 10) && (hurtFlag == false))
		{
			Debug.Log ("Hurt");
			aScript.snores = true;
			hurtFlag = true;
		}

		// increase timer
		timer += Time.deltaTime;
	}

	//Check for collision between player and a projectile
	//decreaseplayers health by 1/8th
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Projectile") 
		{
			playerHealth -= 12.5f;

			// Reset hurt timer
			timer = 0;

			if (Random.Range (0.0f, 100.0f) < 50.0f) 
			{
				if (Random.Range (0.0f, 100.0f) < 50.0f) 
				{
					if (!aScript.cookingSource.isPlaying)
						aScript.cooking = true;
				} 
				else 
				{
					if (!aScript.dodgeThoseSource.isPlaying)
						aScript.dodgeThose = true;
				}
			}
		}
	}
}
