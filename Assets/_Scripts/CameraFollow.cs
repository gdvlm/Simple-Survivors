using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTransform;
    [SerializeField] private BoxCollider2D mapBounds;

    private float _xMin, _xMax, _yMin, _yMax;
    private float _cameraX, _cameraY;    
    private float _cameraOrthographicSize;
    private float _cameraRatio;

    void Start()
    {
        var bounds = mapBounds.bounds;
        _xMin = bounds.min.x;
        _xMax = bounds.max.x;
        _yMin = bounds.min.y;
        _yMax = bounds.max.y;
        
        Camera mainCamera = GetComponent<Camera>();
        _cameraOrthographicSize = mainCamera.orthographicSize;
        _cameraRatio = (_xMax + _cameraOrthographicSize) / 2.0f;
    }

    void LateUpdate()
    {
        _cameraX = Mathf.Clamp(followTransform.position.x, _xMin + _cameraRatio, _xMax - _cameraRatio);
        _cameraY = Mathf.Clamp(followTransform.position.y, _yMin + _cameraOrthographicSize, _yMax - _cameraOrthographicSize);
        transform.position = new Vector3(_cameraX, _cameraY, transform.position.z);
    }
}
