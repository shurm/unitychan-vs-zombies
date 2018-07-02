using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDisplay : MonoBehaviour {

    private RadarData radarData;

    private Transform player;

    public float mapScale = 4.0f;



    // Use this for initialization
    void Start ()
    {
        radarData = GameObject.Find("Radar").GetComponent<RadarData>();
        player = radarData.player;

    }

    private void DrawRadarDots()
    {
        foreach (RadarObject ro in radarData.radarObjects)
        {
            Vector3 radarPos = (ro.owner.transform.position - player.position);
            float distToObject = Vector3.Distance(player.position, ro.owner.transform.position) * mapScale;
            float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - player.eulerAngles.y;

            radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
            radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);

            ro.icon.transform.SetParent(transform);
            ro.icon.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DrawRadarDots();

    }
}
