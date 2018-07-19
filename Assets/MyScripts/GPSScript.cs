using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSScript : MonoBehaviour {

    public enum Locations
    {
        TimeSquare,
        LosAngeles,
        Orlando,
        London,
        Bentonville
    };
    public enum Planet
    {
        Sun = 0,
        Mercury,
        Venus,
        Earth,
        Moon,
        Mars

    };

    public Locations curLocal;
    public Planet curPlanet;
    private Locations oldLocal;
    private Text buttonText;
    public float Lat, Lon, radius;
    public GameObject _markergameObject;
    public GameObject PlanetTracker;
    private List<GameObject> Planets;

	// Use this for initialization
    void Start () {
        curPlanet = Planet.Earth;
        GetPlanets();

        PlanetTracker = Planets[(int)curPlanet];
        radius = PlanetTracker.GetComponent<SphereCollider>().radius;
        SetLatandLon();
        oldLocal = curLocal;
        if(_markergameObject != null)
        {
            _markergameObject = Instantiate(_markergameObject, GPSLocation(), new Quaternion(0,0,0,1), PlanetTracker.transform);
            _markergameObject.GetComponent<MarkerScript>().PlanetToLookAt = PlanetTracker;
        }
        buttonText = GameObject.FindGameObjectWithTag("InfoButton").GetComponent<Text>();

        SetUpGPS();
	}
	
	// Update is called once per frame
	void Update () {
        //Debugging Editor
        //Checks to see if the modeled earth is somewhat accurate
        //Hoping the $15 I sent wasnt a watch
        if(oldLocal != curLocal)
        {
            oldLocal = curLocal;
            SetLatandLon();
            float yRot = PlanetTracker.transform.eulerAngles.y;
            PlanetTracker.transform.rotation = Quaternion.identity;
            _markergameObject.transform.position = GPSLocation();
            PlanetTracker.transform.rotation = Quaternion.Euler(0, yRot, 0);

        }

        //Just checked its okay
        if(Input.location.status == LocationServiceStatus.Running)
        {
            //Getting new Lat and Long
            Lat = Input.location.lastData.latitude;
            Lon = Input.location.lastData.longitude;

            //I have to reset the rotation of the globe before moving the GPS marker
            //I will put back the original rotation after calculation and let Unity do the rest
            float yRot = PlanetTracker.transform.eulerAngles.y;
            PlanetTracker.transform.rotation = Quaternion.identity;
            _markergameObject.transform.position = GPSLocation();
            PlanetTracker.transform.rotation = Quaternion.Euler(0, yRot, 0);
        }
	}


    //A simple way of getting a Vector location based off a given Lat/Long
    //This will take into account where the the globe (Not Rotation) is at in the scene
    private Vector3 GPSLocation()
    {
        
        Vector3 result;
        float ltR = Lat * Mathf.Deg2Rad; 
        float lnR = Lon * Mathf.Deg2Rad;
        //float xRot = PlanetTracker.transform.eulerAngles.x;
        //float yRot = PlanetTracker.transform.eulerAngles.y;
        //float zRot = PlanetTracker.transform.eulerAngles.z;

        //https://github.com/mapbox/mapbox-unity-sdk/issues/238
        float xPos = (radius * PlanetTracker.transform.lossyScale.z) * Mathf.Cos(ltR) * Mathf.Cos(lnR);
        float zPos = (radius * PlanetTracker.transform.lossyScale.z) * Mathf.Cos(ltR) * Mathf.Sin(lnR);
        float yPos = (radius * PlanetTracker.transform.lossyScale.z) * Mathf.Sin(ltR);
        Vector3 earthPositon = PlanetTracker.transform.position;
        result = new Vector3((xPos + earthPositon.x), (yPos + earthPositon.y), (zPos + earthPositon.z));

        return result;

    }

    //Key Location on Earth to test if the model earth is somewhat accurate and to test GPS Marker.
    private void SetLatandLon()
    {
        if (curLocal == Locations.Bentonville)
        {
            Lat = 36.37233f; //N
            Lon = -94.20949f; //E
        }
        if (curLocal == Locations.TimeSquare)
        {
            Lat = 40.75615f; //N
            Lon = -73.98743f; //E
        }
        if (curLocal == Locations.LosAngeles)
        {
            Lat = 34.04975f; //N
            Lon = -118.2497f; //E
        }
        if (curLocal == Locations.Orlando)
        {
            Lat = 28.53823f; //N
            Lon = -81.37739f; //E
        }
        if (curLocal == Locations.London)
        {
            Lat = 51.50642f;
            Lon = -0.12721f;
        }
    }

    private void GetPlanets()
    {
        Planets = new List<GameObject>();
        Planets.Add(GameObject.FindGameObjectWithTag("Sun"));
        Planets.Add(GameObject.FindGameObjectWithTag("Mercury"));
        Planets.Add(GameObject.FindGameObjectWithTag("Venus"));
        Planets.Add(GameObject.FindGameObjectWithTag("Earth"));
        Planets.Add(GameObject.FindGameObjectWithTag("Moon"));
        Planets.Add(GameObject.FindGameObjectWithTag("Mars"));
    }

    private void SetUpGPS()
    {
        //Code grab from unity's documentation
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("GPS disabled by User");
            return;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            return;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            return;
        }

        else
        {
            // Access granted and location value could be retrieved
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        //Stop service if there is no need to query location updates continuously
        //Probably don't have to do it at all maybe when the app is on standby will research on that later
        //Input.location.Stop();
    }
}
