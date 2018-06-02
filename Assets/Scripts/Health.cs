using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Range(0, 100)]
    public int currentValue = 100;

    private readonly object syncLock = new object();
    private bool currentlyTakingDamage = false;
 

    public void DealDamage(int damage)
    {
        if (!currentlyTakingDamage)
        {
            currentlyTakingDamage = true;
            lock (syncLock)
            {
                currentValue = currentValue - damage;
            }

            currentlyTakingDamage = false;
        }
    }
}
