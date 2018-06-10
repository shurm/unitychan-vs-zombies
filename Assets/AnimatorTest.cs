using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        AnimatorStateInfo animatorStateInfo = GetComponent<Animator>().GetNextAnimatorStateInfo(0);

        Debug.Log(animatorStateInfo.fullPathHash);

    }
}
