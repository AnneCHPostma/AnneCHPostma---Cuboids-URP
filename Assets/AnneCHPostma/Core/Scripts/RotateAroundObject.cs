using UnityEngine;

namespace AnneCHPostma.Core
{
    public class RotateAroundObject : MonoBehaviour
    {
        [Tooltip("GameObject to rotate around")]
        [SerializeField]
        private GameObject _gameObject = default;

        [Tooltip("The rotation speed in angle degrees per second")]
        [SerializeField]
        private float _rotationSpeed = 0.5f;

        [Tooltip("The axis to rotate around (default around the y-axis)")]
        [SerializeField]
        private Vector3 _rotateAroundAxis = new Vector3(0.0f, 1.0f, 0.0f);

        private void Update()
        {
            if (_gameObject == null)
            {
                Debug.LogError("No GameObject is attached to the RotateAroundObject script!");
                return;
            }

            transform.LookAt(_gameObject.transform.position);
            transform.RotateAround(_gameObject.transform.position, _rotateAroundAxis, _rotationSpeed * Time.deltaTime);
        }
    }
}