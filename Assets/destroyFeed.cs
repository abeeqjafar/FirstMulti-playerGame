using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyFeed : MonoBehaviour
{
    public float destroyTime = 4f;

    private void OnEnable()
    {
        Destroy(gameObject, destroyTime);
    }
}
