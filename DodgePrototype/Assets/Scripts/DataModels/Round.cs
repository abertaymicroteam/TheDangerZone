using UnityEngine;
using System.Collections;

public class Round : MonoBehaviour 
{
	// Template class for game rounds 

	public int duration; // Round duration in seconds
	public GameObject projectile; // The projectiles that the turrets will spawn in this round

	// Use this for initialization
	void Start () 
	{
		duration = 0;
	}

	public void Init(int dur)
	{
		duration = dur;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
