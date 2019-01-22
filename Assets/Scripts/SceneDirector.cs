using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    public LevelTextDisplayer levelTextDisplayer;
    public RadarData radar;

    private ZombieSpawnManager zombieSpawnManager;
    private readonly object syncLock = new object();
   
    private int level = 1;
    // Use this for initialization
    void Start()
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
        Debug.Log("level " + level);
        level++;
        levelTextDisplayer.StartLevelTextAnimation();
        zombieSpawnManager.StartSpawning();
    }

    internal void RemoveRadarObject(GameObject gameObject)
    {
        lock (syncLock)
        {
            if(radar.RemoveRadarObject(gameObject))
            {
                if (radar.NoObjectsOnMap())
                {
                    BeginNextLevel();
                }
            }
        }
    }
}
