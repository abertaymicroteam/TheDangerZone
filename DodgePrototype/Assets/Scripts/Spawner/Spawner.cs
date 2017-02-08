using UnityEngine;
using System.Collections;

/*Spawner
 * 
 * The spawner script attached to each turret
 * 
 * This script instantiates the projectiles in time with the animation of the turret
 * 
 */

public class Spawner : MonoBehaviour {

	public Vector3 SpawnLocation; 	//Current position of the turret
	public GameObject Projectile1;	//Projectiles to be fired
	public GameObject Projectile2; 	//Currently limited to 2
	public float nextProjectile;	//The next projectile that will be fired
	public bool fire;				//Start firing sequence?
	public bool select;				//Select the next projectile?
	public bool shoot;				//Shoot projectile?
	public float counter;			//Counter for animation timings

	private Animator anim;			//Animator

	void Start () {

		//Get the animator
		anim = GetComponent<Animator> ();

		//tell the turret to start in down position, set counter to 0, set bools to false and tell the animtor the fire state;
		nextProjectile = 0;
		counter = 0;
		fire = false;
		select = false;
		anim.SetBool ("Fire", fire);

	
	}
		
	void Update () {

		//set the animation bull to start animation
		anim.SetBool ("Fire", fire);

		//when the spawner is told to fire
		if (fire) {

			//select projectile to be fired 
			if (select) {
				nextProjectile = Random.Range (0, 2);
				//selectoin has been made for this fire, ready to shoot
				select = false;
				shoot = true;
			}
				
			//count out animation time 
			counter += Time.deltaTime;

			//when animation reaches fire frame set spawn location and instantiate projectile
			if (counter > 3 && shoot) {
				
				SpawnLocation.Set (gameObject.transform.position.x, gameObject.transform.position.y+0.45f, gameObject.transform.position.z);

				switch ((int)nextProjectile) {
				case 0:
					Instantiate (Projectile1, SpawnLocation, Quaternion.identity);
					break;
				case 1:
					Instantiate (Projectile2, SpawnLocation, Quaternion.identity);
					break;
				}

				//do not instantiate another projectile in this animation
				shoot = false;
			
			}

			//wait till end of animation to set fire to false;
			if (counter > 5) {
				fire = false;
				counter = 0;
			}
		}

	}


	public void Spawn(){
		fire = true;
		select = true;
	}
		
}
