using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class director : MonoBehaviour {

    public Transform player;

    public Transform zombie;

    public int startTotalZombies;
    public int zombieIncrease;

    public int seconds;

    private float timeLeft;
    private int numberOfZombiesCreated, numberOfZombiesKilled;

    private int maxLevelZombies;
    public Transform[] zombieSpawnLocations;

    private readonly object syncLock = new object();
    // Use this for initialization
    void Start () {

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
                    Transform t = Instantiate(zombie, zombieSpawnLocations[a].position, Quaternion.identity);
                    t.gameObject.SetActive(true);
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
