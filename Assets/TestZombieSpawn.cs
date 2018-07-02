using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestZombieSpawn : MonoBehaviour {

    public GameObject zombiePrefab;
    public RadarData rader;

	// Use this for initialization
	void Start ()
    {
        GameObject t = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
        t.SetActive(true);

        rader.RegisterRadarObject(t);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
