using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    protected int cost;
    [SerializeField]
    protected int size;
    protected BuildController owner;

    public Vector2 CenterOnGrid(Vector2 point)
    {
        if (size % 2 == 1) point -= Vector2.one / 2;

        point.x = Mathf.Round(point.x);
        point.y = Mathf.Round(point.y);

        if (size % 2 == 1) point += Vector2.one / 2;
        return point;
    }

    public int GetCost()
    {
        return cost;
    }

    public int GetSize()
    {
        return size;
    }

    public void SetOwner(BuildController owner)
    {
        this.owner = owner;
    }
}
