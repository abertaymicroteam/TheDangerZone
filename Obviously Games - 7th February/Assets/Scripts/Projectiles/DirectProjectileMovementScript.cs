using UnityEngine;
using System.Collections;

/*Direct Projectile Movement
 * 
 * This script controls the movement of projectiles directly from one point to the other
 * it calculates a vector between the point of initialization and the targets current position
 * and moves the projectile along that vector
 * 
 * If the projectile collides with the controller it will calculate the new vector to follow
 * depending on point and speed of collision
 * 
 */
public class DirectProjectileMovementScript : MonoBehaviour 
{
	// Movement variables
	public float speed;		//movement speed of ball
	Vector3 direction;		//direction vector of projectile
	Rigidbody rigB;			//rigidbody
	Vector3 Vel;			// Final velocity

	//rotation
	Transform target;			
	public Vector3 targetDir;

	//testing
	public Vector3 newDir;

	// Show indicator for this projectile?
	public bool showIndicator;

	// Use this for initialization
	void Start ()
    {
		//initialize variables 
		rigB = this.GetComponent<Rigidbody> ();
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		target = player.transform;

		//calculate direction ( target position - current position)
		direction.Set(player.transform.position.x -rigB.position.x, player.transform.position.y -rigB.position.y,player.transform.position.z -rigB.position.z);            
		direction.Normalize ();

		//add velocity to the projectile
		Vel = direction * speed;
		rigB.velocity = Vel;

		// Show projectile by default
		showIndicator = true;

		//calculate the direction of the target and rotate towards it
		targetDir = target.position - transform.position;
		transform.rotation = Quaternion.LookRotation (-rigB.velocity);

	}
	
	// Update is called once per frame
	void Update ()
    {
		//rotate to face the player and draw a ray facing the opposite direction from movement
		Debug.DrawRay (transform.position, -rigB.velocity, Color.red);
		transform.rotation = Quaternion.LookRotation (-rigB.velocity);
	}

	void OnCollisionEnter(Collision collision)
	{
		// Stop particle emission
//		gameObject.GetComponent<ParticleSystem> ().enableEmission = false;

		//if colliding with controller find out the vector between controller and ball launch ball in that direction by multiple of speed of controlle movement
		if (collision.collider.tag == "GameController") 
		{
			Debug.Log ("Adding Velocity!");
			ControllerVelocity controller = collision.gameObject.GetComponent<ControllerVelocity> ();

			newDir = collision.collider.transform.position - transform.position;
			newDir.y = controller.GetVelocity ().y;
			newDir.x = controller.GetVelocity ().x;
			newDir.z = controller.GetVelocity ().z;

			rigB.AddForce(newDir, ForceMode.VelocityChange);

			showIndicator = false;
		}

		//dont show indicators after a collision has occured
		showIndicator = false;
	}
}
