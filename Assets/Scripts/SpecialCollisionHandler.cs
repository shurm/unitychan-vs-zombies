using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  SpecialCollisionHandler : MonoBehaviour
{
    public string TagOfTarget;
    public abstract void HandleStartOfCollision(GameObject gameObject);
    public abstract void HandleEndOfCollision(GameObject gameObject);
}
