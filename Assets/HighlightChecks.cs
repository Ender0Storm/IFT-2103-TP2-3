using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightChecks : MonoBehaviour
{
    private bool _colliding;

    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Color _normalColor;
    [SerializeField]
    private Color _blockedColor;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _colliding = Physics2D.OverlapBox(transform.position, new Vector2(transform.localScale.x - 0.05f, transform.localScale.y - 0.05f), 0) != null;

        if (_colliding) _spriteRenderer.color = _blockedColor;
        else _spriteRenderer.color = _normalColor;
    }

    public bool CheckIfClear()
    {
        return !_colliding;
    }
}
