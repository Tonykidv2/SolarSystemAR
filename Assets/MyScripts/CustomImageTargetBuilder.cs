using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CustomImageTargetBuilder : MonoBehaviour {


    private UserDefinedTargetBuildingBehaviour UserDefinedTargetBuilding;
	// Use this for initialization
	void Start () {
        UserDefinedTargetBuilding = GameObject.FindGameObjectWithTag("ImageTagetBuilder").GetComponent<UserDefinedTargetBuildingBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
