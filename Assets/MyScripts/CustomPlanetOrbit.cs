using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlanetOrbit : MonoBehaviour {

    [Header("Orbit Simulation")]
    [Tooltip("Defines the Center of Gravity for this Object, which is used as Reference for our Orbit Simulation.")]
    public GameObject centerOfGravity;
    [Tooltip("Allows you to set the rotaion axis of the objects orbit")]
    public Vector3 orbitAxis = Vector3.up;
    [Tooltip("Defines the velocity at which this GameObject will move around the center of gravity")]
    public float orbitSpeed = 80.0f;
    private float oldorbitSpeed = 0;
    private bool orbitting = true;
    [Tooltip("Is this a Moon. Will work properly if moon is child of orbitting planet in Hierarchy")]
    public bool MoonOrbit = false;

    [Header("Trajectory Visualization")]
    [Tooltip("Enables a 3D Visulazation of our GameObjects Trajectory")]
    public bool showOrbit = true;
    [Tooltip("Defines the color of the rendered circle for the trajectory.")]
    public Color lineColor = Color.red;
    [Tooltip("Defines the line width of the rendered circle for the trajectory.")]
    public float lineWidth = 0.05f;
    private TrailRenderer trailRenderer;

	// Use this for initialization
	void Start () {
        trailRenderer = GetComponent<TrailRenderer>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        // Start to rotate our object around its center of gravity
        if(!MoonOrbit)
            transform.RotateAround(centerOfGravity.transform.localPosition, orbitAxis, orbitSpeed * Time.deltaTime);
        else{
            transform.RotateAround(centerOfGravity.transform.position, orbitAxis, orbitSpeed * Time.deltaTime);
        }
	}

    public void ToggleOrbitting()
    {
        if (orbitting)
        {
            oldorbitSpeed = orbitSpeed;
            orbitSpeed = 0;
            orbitting = !orbitting;
        }
        else
        {
            orbitSpeed = oldorbitSpeed;
            orbitting = !orbitting;
        }
    }
}
