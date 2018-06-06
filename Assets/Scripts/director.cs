using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class director : MonoBehaviour {

    

    public GameObject zombie;

    public int startTotalZombies;
    public int zombieIncrease;

    public int seconds;

    private float timeLeft;
    private int numberOfZombiesCreated, numberOfZombiesKilled;

    private int maxLevelZombies;
    private Transform[] zombieSpawnLocations;

    private Radar radar;
    private GameObject player;

    private readonly object syncLock = new object();
    // Use this for initialization
    void Start ()
    {
        zombieSpawnLocations = new Transform[transform.childCount];

        for (int a = 0;  a < zombieSpawnLocations.Length; a++)
        {
            zombieSpawnLocations[a] = transform.GetChild(a);
        }

        radar = GameObject.Find("Radar").GetComponent<Radar>();

        player = GameObject.Find("unitychan");


        timeLeft = 0;

        numberOfZombiesCreated = 0;
        numberOfZombiesKilled = 0;
        maxLevelZombies = startTotalZombies;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (timeLeft <= 0)
        {
            lock(syncLock)
            { 
                for (int a = 0; numberOfZombiesCreated < maxLevelZombies && a < zombieSpawnLocations.Length; a++)
                {
                    GameObject newZombie = Instantiate(zombie, zombieSpawnLocations[a].position, Quaternion.identity);

                    //newZombie.GetComponent<zombie>().target = player.transform;

                    newZombie.GetComponent<GeneralZombieBehavior>().target = player.transform;

                    newZombie.GetComponent<GeneralZombieBehavior>().radar = radar;

                    radar.RegisterRadarObject(newZombie);

                    numberOfZombiesCreated++;
                }
                timeLeft = seconds;
            }
        }
        timeLeft -= Time.deltaTime;

     //  Debug.Log("killed : "+numberOfZombiesKilled +" , created: " + numberOfZombiesCreated + " , max: " + maxLevelZombies);
	}

    public void zombieDead()
    {
        numberOfZombiesKilled++;

        if (numberOfZombiesKilled==maxLevelZombies)
        {
            lock (syncLock)
            {
                maxLevelZombies += zombieIncrease;
                numberOfZombiesCreated = 0;
                numberOfZombiesKilled = 0;
                timeLeft = seconds;

                player.GetComponent<player>().nextLevelAnimation();
            }
        }

    }
    
}
