using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    public int speed = 0;
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButton("UP"))
        {
            transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
        }
        if(Input.GetButton("DOWN"))
        {
            transform.Translate(new Vector3(0, -speed, 0) * Time.deltaTime);
        }

        // check bounds
        if(transform.position.y > 13)
        {
            transform.position = new Vector3(-15, 13, 0);
        }

        if (transform.position.y < -13)
        {            
            transform.position = new Vector3(-15, -13, 0);
        }

    }
}
