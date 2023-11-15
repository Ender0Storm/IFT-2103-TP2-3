using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWorldBounds : MonoBehaviour
{
    void Awake()
    {
        Bounds bounds = new Bounds(transform.position, transform.localScale);
        Globals.WorldBounds = bounds;
    }
}
