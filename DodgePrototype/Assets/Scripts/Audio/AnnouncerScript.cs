using UnityEngine;
using System.Collections;

public class AnnouncerScript : MonoBehaviour {

	// Handles to audio sources
	public AudioSource whosNextSource;
	public AudioSource cookingSource;
	public AudioSource crowdSnoresSource;
	public AudioSource dodgeThoseSource;
	public AudioSource moreFightSource;
	public AudioSource youAgainSource;

	// Play flags for other scripts to access
	public bool whosNext;
	public bool cooking;
	public bool snores;
	public bool dodgeThose;
	public bool moreFight;

	public bool newSound;

	// Frame counter
	int ticker;

	// Use this for initialization
	void Start () 
	{
		// Init bools
		whosNext = false;
     	cooking =  false;
    	snores =  false;
		dodgeThose = false;
		moreFight = false;

		newSound = true;

		// Play "You again?" 2 seconds in
		youAgainSource.PlayScheduled(2.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check if any source is currently playing 
		if (whosNextSource.isPlaying || cookingSource.isPlaying ||
		    crowdSnoresSource.isPlaying || dodgeThoseSource.isPlaying ||
		    moreFightSource.isPlaying || youAgainSource.isPlaying) 
		{
			newSound = false;
		} else 
		{
			newSound = true;
		}

		// Check individual sound flags
		if (whosNext && newSound) 
		{
			whosNextSource.Play ();
			whosNext = false;
		}
		if (cooking && newSound) 
		{
			cookingSource.Play ();
			cooking = false;
		}
		if (snores && newSound) 
		{
			crowdSnoresSource.Play ();
			snores = false;
		}
		if (dodgeThose && newSound) 
		{
			dodgeThoseSource.Play ();
			dodgeThose = false;
		}
		if (moreFight && newSound) 
		{
			moreFightSource.Play ();
			moreFight = false;
		}
			
	}
}
