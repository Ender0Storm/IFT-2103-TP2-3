using System.Collections;
using System.Collections.Generic;
using Game;
using Game.pathFinding;
using UnityEngine;

public class HighlightChecks : MonoBehaviour
{
    private bool _buildable;

    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Color _normalColor;
    [SerializeField]
    private Color _blockedColor;

    private GameObject town;
    private GameObject portal;

    private List<Tile> possiblePath;
    private PathFinding _pathFinding;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        town = GameObject.Find("Town");
        portal = GameObject.Find("Spawn Portal");
        Tile start = new Tile
        {
            X = portal.transform.position.x,
            Y = portal.transform.position.y
        };
        Tile finish = new Tile
        {
            X = town.transform.position.x,
            Y = town.transform.position.y
        };
        start.SetDistance(finish.X, finish.Y);
        _pathFinding = new PathFinding(start, finish);
    }

    private void Update()
    {
        //possiblePath = new List<Tile>();
        _buildable = Physics2D.OverlapBox(transform.position, new Vector2(transform.localScale.x - 0.05f, transform.localScale.y - 0.05f), 0) == null;
        if (_buildable)
        {
            //possiblePath = _pathFinding.FindPath();
            //_buildable = possiblePath.Count != 0;
        }

        if (_buildable)
        {
            _spriteRenderer.color = _normalColor;
        }
        else _spriteRenderer.color = _blockedColor;
    }

    public bool CheckIfClear()
    {
        return _buildable;
    }
}
