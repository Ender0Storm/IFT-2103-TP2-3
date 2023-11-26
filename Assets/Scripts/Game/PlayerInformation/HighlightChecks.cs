using UnityEngine;

namespace Game.playerInformation
{
    public class HighlightChecks : MonoBehaviour
    {
        private bool _buildable;

        private SpriteRenderer _spriteRenderer;

        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _blockedColor;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _buildable = Physics2D.OverlapBox(transform.position,
                new Vector2(transform.localScale.x - 0.05f, transform.localScale.y - 0.05f), 0) == null;

            if (_buildable) _spriteRenderer.color = _normalColor;
            else _spriteRenderer.color = _blockedColor;
        }

        public bool CheckIfClear()
        {
            return _buildable;
        }
    }
}

