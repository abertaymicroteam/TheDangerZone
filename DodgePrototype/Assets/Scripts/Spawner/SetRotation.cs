using UnityEngine;
using System.Collections;

/*Set Rotation
 * 
 * Calculations to rotate the turrets to always be facing the player as they move
 * Also rotates the turret to a 45 degree angle when shooting arced projectiles
 * 
 */

public class SetRotation : MonoBehaviour {

	public Transform target;    //player location
	public float speed;	        //rotation speed
	GameObject player;	        //player object
	public Vector3 targetDir;   //vector between turret and player
	public float opp, adj,hyp;	//variables for calculations
	public float step;			//movement speed reduced to frames

	// Use this for initialization
	void Start () {

		//set the target transform to the players location
		player = GameObject.FindGameObjectWithTag("Player");
		target = player.transform;

	}
	
	// Update is called once per frame
	void Update () 
	{

		//update target location
		target = player.transform;

		//calculate variable values for equations
		opp = target.transform.position.y - transform.position.y;
		adj = Mathf.Sqrt(Mathf.Pow(transform.position.x - target.transform.position.x,2)+Mathf.Pow(transform.position.z - target.transform.position.z,2));
		hyp = Mathf.Sqrt (Mathf.Pow (opp, 2) + Mathf.Pow (adj, 2));

		//calculate the direction of the target 
		targetDir = target.position - transform.position;

		//if the next projectile this turret shoots is arced increase y rotation by 45 degrees
		if (GetComponentInChildren<Spawner> ().nextProjectile > 0) 
		{
			targetDir.y = adj * Mathf.Tan (45 * Mathf.Deg2Rad); 
		}

		//calculate rotation amount per frame
		step = speed * Time.deltaTime;

		//rotate towards new direction
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0f);
		transform.rotation = Quaternion.LookRotation (newDir);

		//Draw a ray for debugging
		Debug.DrawRay (transform.position, newDir, Color.red);
	}

}
