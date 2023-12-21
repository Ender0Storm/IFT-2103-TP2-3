using Game.ui;
using UnityEngine;

namespace Game.playerInformation
{
    public class CameraController : MonoBehaviour
    {
        private Camera _mainCamera;
        private Bounds _cameraBounds;

        [SerializeField] private float _cameraSpeed;
        [SerializeField] private float _scrollSpeed;
        [SerializeField] private int _cameraMinSize;
        [SerializeField] private MapBuilder _map;

        void Awake()
        {
            _mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (!PauseMenu.isPaused && _map.generationDone)
            {
                float newSize = _mainCamera.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed;
                _mainCamera.orthographicSize = RestrainCameraSize(newSize);

                UpdateCameraBounds();

                Vector2 cameraInputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
                    .normalized;

                Vector2 newCameraPos = cameraInputs * _cameraSpeed * Time.deltaTime +
                                       (Vector2)_mainCamera.transform.position;
                _mainCamera.transform.position = RestrainCameraPos(newCameraPos);
            }
        }

        private void UpdateCameraBounds()
        {
            float height = _mainCamera.orthographicSize;
            float width = height * _mainCamera.aspect;

            _cameraBounds = new Bounds(new Vector3((_map.GetMapWidth() % 2 == 0) ? 0 : 0.5f, (_map.GetMapHeight() % 2 == 0) ? 0 : 0.5f),
                new Vector3(_map.GetMapWidth() - width * 2, _map.GetMapHeight() - height * 2, 0));
        }

        private float RestrainCameraSize(float size)
        {
            float maxSize = Mathf.Min(_map.GetMapHeight() / 2, _map.GetMapWidth() / (2 * _mainCamera.aspect));
            return Mathf.Clamp(size, _cameraMinSize, maxSize);
        }

        private Vector3 RestrainCameraPos(Vector2 newCameraPos)
        {
            return new Vector3(
                Mathf.Clamp(newCameraPos.x, _cameraBounds.min.x, _cameraBounds.max.x),
                Mathf.Clamp(newCameraPos.y, _cameraBounds.min.y, _cameraBounds.max.y),
                _mainCamera.transform.position.z
            );
        }
    }
}

