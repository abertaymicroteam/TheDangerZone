using UnityEngine;
using System.Collections;

/*Set Rotation
 * 
 * will rotate an object at a set speed in the y axis at point 0,0
 * 
 */

public class SpawnerRotation : MonoBehaviour {

	public float rotation; 

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		//rotate all the spawners in the scene around y axis at 0,0
		transform.RotateAround (transform.position, Vector3.up, rotation * Time.deltaTime);

	}
}
