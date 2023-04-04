using UnityEngine;

namespace Core.Tools
{
    public class WorldBoundaries : MonoBehaviour
    {
        [SerializeField] private Transform _startCamera;
        [SerializeField] private Transform _finalCamera;
        [SerializeField] private Transform _levelLeftBorder;
        [SerializeField] private Transform _levelRightBorder;
        [SerializeField] private UnityEngine.Camera _mainCamera;

        private Vector2 _horizontalPosition;

        private void Awake()
        {
            _horizontalPosition.x =  _levelLeftBorder.position.x + (_levelLeftBorder.localScale.x / 2) + _mainCamera.aspect * _mainCamera.orthographicSize;
            Vector3 startCameraPosition = _startCamera.position;
            startCameraPosition = new Vector3(_horizontalPosition.x, startCameraPosition.y, startCameraPosition.z);
            _startCamera.position = startCameraPosition;

            _horizontalPosition.x =  _levelRightBorder.position.x - ((_levelRightBorder.localScale.x / 2) + _mainCamera.aspect * _mainCamera.orthographicSize);
            Vector3 finalCameraPosition = _finalCamera.position;
            finalCameraPosition = new Vector3(_horizontalPosition.x, finalCameraPosition.y, finalCameraPosition.z);
            _finalCamera.position = finalCameraPosition;
        }
    }
}