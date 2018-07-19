using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlanetRotation : MonoBehaviour {

    [Tooltip("The time in hours, that the planet needs for a full rotation (eg. 24 for the Earth).")]
    public float rotationTime = 0.05f;
    public bool ActivateRotation = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ActivateRotation)
            this.gameObject.transform.Rotate(0, (360 / (rotationTime * 60 * 60)) * Time.deltaTime, 0, Space.Self);
        
	}
}
