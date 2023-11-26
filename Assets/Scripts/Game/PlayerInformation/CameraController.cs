using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _mainCamera;
    private Bounds _worldBounds;
    private Bounds _cameraBounds;

    [SerializeField]
    private float _cameraSpeed;
    [SerializeField]
    private float _scrollSpeed;
    [SerializeField]
    private int _cameraMinSize;

    void Awake()
    {
        _mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        _worldBounds = Globals.WorldBounds;
        UpdateCameraBounds();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            float newSize = _mainCamera.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed;
            _mainCamera.orthographicSize = RestrainCameraSize(newSize);

            UpdateCameraBounds();

            Vector2 cameraInputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            Vector2 newCameraPos = cameraInputs * _cameraSpeed * Time.deltaTime + (Vector2)_mainCamera.transform.position;
            _mainCamera.transform.position = RestrainCameraPos(newCameraPos);
        }
    }

    private void UpdateCameraBounds()
    {
        float height = _mainCamera.orthographicSize;
        float width = height * _mainCamera.aspect;

        _cameraBounds = new Bounds(_worldBounds.center, new Vector3(_worldBounds.size.x - width * 2, _worldBounds.size.y - height * 2, _worldBounds.size.z));
    }

    private float RestrainCameraSize(float size)
    {
        float maxSize = Mathf.Min(_worldBounds.size.y / 2, _worldBounds.size.x / (2 * _mainCamera.aspect));
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
