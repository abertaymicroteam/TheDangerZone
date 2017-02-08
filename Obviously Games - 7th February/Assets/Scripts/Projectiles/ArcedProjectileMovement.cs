using UnityEngine;
using System.Collections;

/*Arced Projectile Movement
 * 
 *Fires the projectile along an arced path towards the player
 *Physics is simulated in the script not using unitys gravity 
 *Speed variable can be changed (This is the time taken to reach the player)
 *the ball will always follow the same tragetory regardless of time taken
 * 
 */

public class ArcedProjectileMovement : MonoBehaviour {

	//objects
	private Rigidbody body; 	//projectile
	private Transform target;	//player

	//maths variables
	private float theta;				//angle
	private float sinTheta;				//sin angle
	private Vector3 vel;				//holder for velocity
	private Vector3 distance;			//distance between start and end
	private float timer;				//frames per second
	private bool hit;					//if hit
	private float s,t,g,holder;			//maths variables

	public float speed;					//time taken for the ball to hit the target

	public bool showIndicator;			// Show indicator for this projectile?

	//testing
	public Vector3 newDir;

	// Use this for initialization
	void Start () {

		//ball has not been hit by the player
		hit = false;

		//get ridid body and players positions
		body = GetComponent<Rigidbody>();
		target = (GameObject.FindWithTag("Player").transform);

		//set the value for gravity depending on the desired time taken to hit the player
		g = 9.8f / (speed*speed);
		timer = 0;

		//Launch ball
		ThrowBall();

		//Show indicator
		showIndicator = true;
	}


	void Update (){

		if (!hit) {
			//calculate value for gravity per frame
			timer = 1 / Time.deltaTime;
	
			Vector3 grav;
			grav.x = 0;
			grav.y = g / timer;
			grav.z = 0;

			//reduce the  y-velocity by gravity per frame

			body.velocity = body.velocity - grav;


			//rotation make the ball face the player and draw a line behind for debugging
			Debug.DrawRay (transform.position, -body.velocity, Color.red);
			transform.rotation = Quaternion.LookRotation (-body.velocity);
		}
	}
		
	void ThrowBall(){

		//get distance between player and positoin
		distance = target.position - transform.position;
	
		//set the x velocity to that distance / desired time to get there
		vel.x = distance.x / speed ;
		vel.z = distance.z / speed ;

		//variables
		s = distance.y;
		theta = 45;
		sinTheta = Mathf.Sin (theta * Mathf.Deg2Rad) ;
		t = 1;

		//equations
		holder = s/speed + ( (0.5f*speed) * g);
		holder = holder/sinTheta;
		holder = holder*sinTheta;
		vel.y = holder;

		//add velocity
		body.velocity = vel;
	}

	void OnCollisionEnter(Collision collision)
	{
		// Stop partical system
		//gameObject.GetComponent<ParticleSystem> ().enableEmission = false;
		hit = true;
		//if colliding with controller find out the vector between controller and ball launch ball in that direction by multiple of speed of controlle movement
		if (collision.collider.tag == "GameController") 
		{
			Debug.Log ("Adding Velocity!");
			ControllerVelocity controller = collision.gameObject.GetComponent<ControllerVelocity> ();

			newDir = collision.collider.transform.position - transform.position;
			newDir.y = controller.GetVelocity ().y;
			newDir.x = controller.GetVelocity ().x;
			newDir.z = controller.GetVelocity ().z;

			body.AddForce(newDir, ForceMode.VelocityChange);

			showIndicator = false;
		}

		//dont show indicators after a collision has occured
		showIndicator = false;
	}


}