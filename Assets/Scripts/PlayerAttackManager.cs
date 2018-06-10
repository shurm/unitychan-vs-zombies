using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackManager : SpecialCollisionHandler
{
    public int zombiePoints = 10;

    public int attackDamage = 1;

    public DisplayScore score;

    public LevelTextDisplayer LevelTextDisplayer;

    private director zombieSpawnScript;

    // Use this for initialization
    void Start ()
    {
        zombieSpawnScript = GameObject.Find("ZombieSpawnLocations").GetComponent<director>();
    }

    public override void HandleCollision(GameObject gameObject)
    {
        GeneralZombieBehavior script = gameObject.GetComponentInParent<GeneralZombieBehavior>();
        bool zombieDestroyed = script.DamageZombie(attackDamage);

        if (zombieDestroyed)
        {
            score.UpdateScore(zombiePoints);

            /*
            if()
            {

                LevelTextDisplayer.StartNextLevelAnimation();
            }
            */
        }

    }
}
