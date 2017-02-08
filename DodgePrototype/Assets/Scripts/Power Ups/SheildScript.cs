using UnityEngine;
using System.Collections;

public class SheildScript : MonoBehaviour {

	// Collision counter
	int collisions;

	// Use this for initialization
	void Start () 
	{
		collisions = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (collisions == 2) 
		{
			// Destroy shield after two collisions
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter()
	{
		collisions++;
	}

}
