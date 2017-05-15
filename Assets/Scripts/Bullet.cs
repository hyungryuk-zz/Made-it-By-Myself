using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Bullet");
        transform.GetComponent<Rigidbody>().AddForce(transform.forward * 500.0f);
    }
}
