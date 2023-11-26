using Game;
using Unity.Netcode;
using UnityEngine;

public class HighlightChecksMP : NetworkBehaviour
{
    private bool _buildable;

    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Color _normalColor;
    [SerializeField]
    private Color _blockedColor;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            Globals.PlayerID = IsHost ? 1 : 2;
            FindFirstObjectByType<BuildControllerMP>().SetHighlight(gameObject);
            FindFirstObjectByType<WaveManagerMP>().SetMPPositions();
            GameObject.Find($"BoardP{Globals.PlayerID % 2 + 1}").transform.Find("Ground").GetComponent<Collider2D>().enabled = true;
        }
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _buildable = Physics2D.OverlapBox(transform.position, new Vector2(transform.localScale.x - 0.05f, transform.localScale.y - 0.05f), 0) == null;

        if (_buildable) _spriteRenderer.color = _normalColor;
        else _spriteRenderer.color = _blockedColor;
    }

    public bool CheckIfClear()
    {
        return _buildable;
    }
}
