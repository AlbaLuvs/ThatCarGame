using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float speed;
    public bool rotate;
	
	void Update () {
        if (rotate)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        }
        else
        {
            transform.Rotate(Vector3.back * Time.deltaTime * speed);
        }
	}
}
