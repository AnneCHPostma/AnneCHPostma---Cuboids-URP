using UnityEngine;
using TMPro;
using PERCEPT10N.Cuboids;

namespace AnneCHPostma.Cuboids
{
    [RequireComponent(typeof(GameObject))]
    public class UpdateCuboidsCounter : MonoBehaviour
    {
        [Tooltip("Add the GameObject that is spawning the cuboids")]
        [SerializeField]
        private GameObject _cuboidsSpawner = null;

        [Tooltip("Add the GameObject that is updating the cuboids counter")]
        [SerializeField]
        private GameObject _cuboidsCounter = null;

        private CuboidsSpawner cuboidsSpawner = null;
        private TextMeshProUGUI cuboidsCounter = null;

        private void Start()
        {
            cuboidsSpawner = _cuboidsSpawner.GetComponent<CuboidsSpawner>();
            cuboidsCounter = _cuboidsCounter.GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (cuboidsSpawner == null)
            {
                Debug.LogError("No CuboidsSpawner component is attached to the CuboidsSpawner GameObject.");

                return;
            }

            if (cuboidsCounter == null)
            {
                Debug.LogError("No TextMeshProUGUI component is attached to the CuboidsCounter GameObject.");

                return;
            }

            cuboidsCounter.text = cuboidsSpawner.cuboidsCounter.ToString();
        }
    }
}
