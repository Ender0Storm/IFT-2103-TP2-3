using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _mainCamera;
    private Bounds _cameraBounds;

    [SerializeField]
    private float _cameraSpeed;

    void Awake()
    {
        _mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        Bounds worldBounds = Globals.WorldBounds;
        float height = _mainCamera.orthographicSize;
        float width = height * _mainCamera.aspect;

        _cameraBounds = new Bounds(worldBounds.center, new Vector3(worldBounds.size.x - width * 2, worldBounds.size.y - height * 2, worldBounds.size.z));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cameraInputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        Vector2 newCameraPos = cameraInputs * _cameraSpeed * Time.deltaTime + (Vector2)_mainCamera.transform.position;
        _mainCamera.transform.position = RestrainCamera(newCameraPos);
    }

    private Vector3 RestrainCamera(Vector2 newCameraPos)
    {
        return new Vector3(
            Mathf.Clamp(newCameraPos.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(newCameraPos.y, _cameraBounds.min.y, _cameraBounds.max.y),
            _mainCamera.transform.position.z
            );
    }
}
