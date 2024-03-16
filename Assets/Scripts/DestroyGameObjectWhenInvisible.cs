using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectWhenInvisible : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(transform.parent.gameObject);
    }
}
