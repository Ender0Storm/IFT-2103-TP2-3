using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField]
    protected int cost;
    [SerializeField]
    protected int size;

    public Vector2 centerOnGrid(Vector2 point)
    {
        if (size % 2 == 1) point -= Vector2.one / 2;

        point.x = Mathf.Round(point.x);
        point.y = Mathf.Round(point.y);

        if (size % 2 == 1) point += Vector2.one / 2;
        return point;
    }
}
