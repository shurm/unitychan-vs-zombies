using UnityEngine;
using System.Collections;

public class MouseAimCamera : MonoBehaviour
{
	public GameObject target;
	public float rotateSpeed = 5;

    public bool turnDirection = true;

	private Vector3 offset;

    private int multiplier = 1;
	
	void Start() {
		offset = target.transform.position - transform.position;
        if (turnDirection)
            multiplier = 1;
        else
            multiplier = -1;


    }
	
	void LateUpdate() {
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed * multiplier;
		target.transform.Rotate(0, horizontal, 0);

		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		transform.position = target.transform.position - (rotation * offset);
		
		transform.LookAt(target.transform);
	}
}