using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerScript : MonoBehaviour {
    public GameObject PlanetToLookAt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.LookAt(PlanetToLookAt.transform.position);
	}
}
