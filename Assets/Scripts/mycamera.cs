using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mycamera : MonoBehaviour {

    public int turnSpeed;

    public Transform player;

    private Vector3 diff;
	// Use this for initialization
	void Start () {

        diff = player.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        UpdatePosition();

    }

    void FixedUpdate()
    {
        rotateCamera((int)(Input.GetAxis("Camera") * turnSpeed));
    }

    void rotateCamera(int speed)
    {
        diff = Quaternion.AngleAxis(speed, Vector3.up) * diff;
        UpdatePosition();
        float x = transform.rotation.x;
        float z = transform.rotation.z;
        float w = transform.rotation.w;

        transform.LookAt(player.position);

        transform.rotation = new Quaternion(x, transform.rotation.y, z, transform.rotation.w);
    }
    void UpdatePosition()
    {
        transform.position = player.position - diff;
    }
}
