using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRocket : MonoBehaviour {
    private float apply_force_timer = 0f;
    private float deathCounter = 10f;
    public float rotationSpeed = 60f;
	// Use this for initialization
	void Start () {
        apply_force_timer = 5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (apply_force_timer > 0f)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * .20f);
            //cur_rocket.GetComponent<Rigidbody>().AddForce(cur_rocket.transform.up*15f);
            //cur_rocket.GetComponent<Rigidbody>().AddTorque(-cur_rocket.transform.right*.5f);
            apply_force_timer -= Time.deltaTime;
        }
        deathCounter -= Time.deltaTime;
        if(deathCounter <= 0f)
        {
            Destroy(gameObject);
        }
        transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * rotationSpeed);
	}
}
