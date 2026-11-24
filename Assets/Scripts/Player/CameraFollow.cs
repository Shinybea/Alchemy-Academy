using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera _cam;
    private Vector2 _camOgPos;

    private void Start()
    {
        _cam = Camera.main;
        if (_cam == null) { return; }
        _camOgPos = _cam.transform.position;
    }

    private void LateUpdate()
    {
        _cam.transform.position = transform.position;
    }
}
