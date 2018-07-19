using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRockets : MonoBehaviour {

    public GameObject Rocket;
    public float spawnTimer = 0;
    public float min = -2.0f, max = 2.0f;
    public bool StopSpawning = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            
            Vector3 position = new Vector3(Random.Range(min, max), 0, Random.Range(min, max));
            Instantiate(Rocket, position, Random.rotation, transform.parent);
            spawnTimer = 3;
        }
	}
}
