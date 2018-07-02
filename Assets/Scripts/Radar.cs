using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    public Transform player;
    public List<TagImagePair> recognizedTagImagePairs;

    private List<RadarObject> radarObjects = new List<RadarObject>();

    public float mapScale = 1.0f;
    

	// Use this for initialization
	void Start ()
    {}

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
    public void RemoveRadarObject(GameObject gameObject)
    {
        List<RadarObject> tempList = new List<RadarObject>();
        foreach (RadarObject ro in radarObjects)
        {
            if (ro.owner != gameObject)
                tempList.Add(ro);
            else
                Destroy(ro.icon);
        }

        radarObjects = tempList;
    }

    private void DrawRadarDots()
    {
        foreach(RadarObject ro in radarObjects)
        {
            Vector3 radarPos = (ro.owner.transform.position - player.position);
            float distToObject = Vector3.Distance(player.position, ro.owner.transform.position) * mapScale;
            float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - player.eulerAngles.y;

            radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) *-1;
            radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);

            //ro.icon.transform.SetParent(transform);
            ro.icon.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + transform.position;
        }
    }

	// Update is called once per frame
	void Update () {
        DrawRadarDots();

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
