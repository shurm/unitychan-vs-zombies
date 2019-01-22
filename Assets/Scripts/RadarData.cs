using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarData : MonoBehaviour
{

    public Transform player;
    public List<TagImagePair> recognizedTagImagePairs;

    public List<RadarObject> radarObjects;

    // Use this for initialization
    void Start()
    {
        radarObjects = new List<RadarObject>();
    }

    public void RegisterRadarObject(GameObject newGameObject)
    {
        Image imageicon = CreateImageIcon(newGameObject.tag);

        RadarObject newRadarObject = new RadarObject() { owner = newGameObject, icon = imageicon };
        radarObjects.Add(newRadarObject);
    }

    private Image CreateImageIcon(string tag)
    {
        foreach (TagImagePair ti in recognizedTagImagePairs)
            if (ti.tag == tag)
                return Instantiate(ti.icon, transform).GetComponent<Image>();

        return null;
    }
    public bool RemoveRadarObject(GameObject gameObject)
    {
        List<RadarObject> tempList = new List<RadarObject>();
        foreach (RadarObject ro in radarObjects)
        {
            if (ro.owner != gameObject)
                tempList.Add(ro);
            else
                Destroy(ro.icon);
        }
        bool removed = true;
        if (tempList.Count == radarObjects.Count)
        {
            removed = false;
        }

        radarObjects = tempList;

        return removed;
    }

   
    public bool NoObjectsOnMap()
    {
        return radarObjects.Count == 0;
    }

    public int GetObjectsOnMapCount()
    {
        return radarObjects.Count;
    }
}
