using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour {

    public GameObject zombie;

    public int startTotalZombies;
    public int zombieIncrease;

    public int seconds;

    private float timeLeft;

    private int numberOfZombiesCreated;

    private int maxLevelZombies = 0;
    private Transform[] zombieSpawnLocations;

    private Radar radar;
    private GameObject player;
    private Director director;

    private readonly object syncLock = new object();
    // Use this for initialization
    void Start()
    {
        zombieSpawnLocations = new Transform[transform.childCount];

        for (int a = 0; a < zombieSpawnLocations.Length; a++)
            zombieSpawnLocations[a] = transform.GetChild(a);


        radar = GameObject.Find("Radar").GetComponent<Radar>();
        player = GameObject.Find("unitychan");
        director = GameObject.Find("PlayerCameraUI").GetComponent<Director>();

        maxLevelZombies = startTotalZombies;
    }

    public void StartSpawning()
    {
        timeLeft = 0;
        numberOfZombiesCreated = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfZombiesCreated < maxLevelZombies)
        {
            if (timeLeft <= 0)
                lock (syncLock)
                {
                    for (int a = 0; numberOfZombiesCreated < maxLevelZombies && a < zombieSpawnLocations.Length; a++)
                    {
                        GameObject newZombie = Instantiate(zombie, zombieSpawnLocations[a].position, Quaternion.identity);

                        GeneralZombieBehavior behaviorOfNewZombie = newZombie.GetComponent<GeneralZombieBehavior>();
                        behaviorOfNewZombie.target = player.transform;
                        behaviorOfNewZombie.radar = radar;
                        behaviorOfNewZombie.director = director;

                        radar.RegisterRadarObject(newZombie);

                        numberOfZombiesCreated++;
                    }
                    timeLeft = seconds;
                }
            else
                timeLeft -= Time.deltaTime;
        }


        //  Debug.Log("killed : "+numberOfZombiesKilled +" , created: " + numberOfZombiesCreated + " , max: " + maxLevelZombies);
    }
}
