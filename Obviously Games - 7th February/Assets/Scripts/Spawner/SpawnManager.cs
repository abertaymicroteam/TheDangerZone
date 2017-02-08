using UnityEngine;
using System.Collections;

/*Spawn Manager
 * 
 * This script, attached to the parent object of the spawners handles 
 * telling which spawner when to fire and the delay between this
 * 
 */
public class SpawnManager : MonoBehaviour {

	private float SpawnDelay;		  //Counter between setTime and spawn
	public float setTime;			  //varied reset timer for spawn
	Spawner[] spawners;				  //avaliable spawners
	private int spawnerNo;			  //the spawner selected at random



	void Start () {

		//set timer to max time seed random
		SpawnDelay = setTime;
		Random.seed = (int)System.DateTime.Now.Ticks;

		//generate array of spawner objects
		//place projectlie types onto array
		spawners = FindObjectsOfType (typeof(Spawner)) as Spawner[];

	}

	//set the timer to count in real time when it reaches 0 spawn a projectile
	//from a random spawner and reset to new time
	void Update () {

		//count down to next shot
		SpawnDelay -= Time.deltaTime;

		//when timer is 0 
		if (SpawnDelay <= 0) {
			//select random spawner
			spawnerNo = Random.Range (0, spawners.Length);
			//if the spawner isnt currently firing, tell it to fire
			if (spawners [spawnerNo].fire == false) {
				spawners [spawnerNo].Spawn ();
			}
			//reset timer
			SpawnDelay = setTime;
		}
	}
}
