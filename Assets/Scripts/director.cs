using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {

    public LevelTextDisplayer levelTextDisplayer;
    public Radar radar;

    private ZombieSpawnManager zombieSpawnManager;

    // Use this for initialization
    void Start ()
    {
        if (levelTextDisplayer == null)
            levelTextDisplayer = GetComponent<LevelTextDisplayer>();
        zombieSpawnManager = GameObject.Find("ZombieSpawnLocations").GetComponent<ZombieSpawnManager>();

        StartSpawningAndTextAnimation();
    }
	
    private void BeginNextLevel()
    {
        levelTextDisplayer.IncrementLevel();
        StartSpawningAndTextAnimation();
    }
    private void StartSpawningAndTextAnimation()
    {
        levelTextDisplayer.StartLevelTextAnimation();
        zombieSpawnManager.StartSpawning();
    }

    internal void NextLevelCheck()
    {
        if (radar.NoObjectsOnMap())
            BeginNextLevel();
        
    }
}
