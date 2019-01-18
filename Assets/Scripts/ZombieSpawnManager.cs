using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ZombieSpawnManager : MonoBehaviour
{

    public GameObject zombie;
    public int activeSpawnSpots;
    public int startTotalZombies;
    public int zombieIncrease;
    public int seconds;

    private float timeLeft;

    private int numberOfZombiesCreated;

    private int maxLevelZombies = 0;
    private Vector3[] zombieSpawnLocations;

    private Vector3[] currentZombieSpawnLocations;

    private RadarData radar;
    private Transform player;
    private SceneDirector director;

    private readonly object syncLock = new object();
    // Use this for initialization
    void Start()
    {
        zombieSpawnLocations = new Vector3[transform.childCount];
        currentZombieSpawnLocations = new Vector3[activeSpawnSpots];

        for (int a = 0; a < zombieSpawnLocations.Length; a++)
            zombieSpawnLocations[a] = transform.GetChild(a).GetChild(0).position;


        radar = GameObject.Find("Radar").GetComponent<RadarData>();
        player = GameObject.Find("unitychan").transform;
        director = GameObject.Find("PlayerCameraUI").GetComponent<SceneDirector>();

        maxLevelZombies = startTotalZombies;
    }

    public void StartSpawning()
    {
        
        zombieSpawnLocations= zombieSpawnLocations.OrderBy(point => Vector3.Distance(player.position, point)).ToArray();

        for (int a = 0; a < currentZombieSpawnLocations.Length; a++)
            currentZombieSpawnLocations[a] = zombieSpawnLocations[a];

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
                    for (int a = 0; numberOfZombiesCreated < maxLevelZombies && a < currentZombieSpawnLocations.Length; a++)
                    {
                        GameObject newZombie = Instantiate(zombie, zombieSpawnLocations[a], Quaternion.identity);
                        newZombie.SetActive(false);
                        ZombieEnvironmentVariables variablesToBeSet = newZombie.GetComponent<ZombieEnvironmentVariables>();
                        variablesToBeSet.target = player.transform;
                        variablesToBeSet.director = director;

                        radar.RegisterRadarObject(newZombie);

                        newZombie.SetActive(true);
                        numberOfZombiesCreated++;
                    }
                    timeLeft = seconds;
                }
            else
                timeLeft -= Time.deltaTime;
        }


          Debug.Log(" , created: " + numberOfZombiesCreated + " , max: " + maxLevelZombies);
    }
}
