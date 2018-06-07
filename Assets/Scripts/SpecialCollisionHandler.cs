using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  SpecialCollisionHandler : MonoBehaviour
{
    public string TagOfTarget;
    public abstract void HandleCollision(GameObject gameObject);
}
