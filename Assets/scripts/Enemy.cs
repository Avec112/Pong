using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int speed = 15; // should be global

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate(new Vector3(0, -speed, 0) * Time.deltaTime);
		}

		// check bounds
		if(transform.position.y > 13)
		{
			transform.position = new Vector3(15, 13, 0);
		}

		if (transform.position.y < -13)
		{            
			transform.position = new Vector3(15, -13, 0);
		}

	}
}
