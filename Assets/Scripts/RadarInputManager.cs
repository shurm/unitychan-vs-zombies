using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarInputManager : MonoBehaviour
{
    public GameObject radar1;

    public GameObject radar2;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Map") > 0)
        {
            radar1.SetActive(false);
            radar2.SetActive(true);
        }
        else
        {
            radar1.SetActive(true);
            radar2.SetActive(false);
        }
    }
}