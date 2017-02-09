using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	// Initialises game rounds and switches between them

	public enum GAMESTATE {ENTRANCE, PLAY, DEAD};
	public GAMESTATE currentState;

	private List<Round> rounds = new List<Round>();
	private int roundTimer;
	private int currRound;

	// Platform
	private Animator platformAnim;

	// Use this for initialization
	void Start () 
	{
		// Set Game State
		currentState = GAMESTATE.ENTRANCE;
		currRound = 0;

		// Create rounds 
		Round directRound = new Round();
		directRound.Init (60);
		rounds.Add (directRound);

		Round arcRound = new Round ();
		arcRound.Init (70);
		rounds.Add (arcRound);

		// Get platform state
		platformAnim = GameObject.FindGameObjectWithTag("Platform").GetComponent<Animator>();;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentState == GAMESTATE.ENTRANCE) 
		{
			// Check if platform has finished its entrance animation
			if (platformAnim.GetCurrentAnimatorStateInfo(0).IsName("Play"))
			{
				Debug.Log ("Platform Finished");
				currentState = GAMESTATE.PLAY;
			}
		}
	}
}
