using UnityEngine;

namespace PERCEPT10N.Cuboids
{
    public class CuboidsSpawner : MonoBehaviour
    {
        [Tooltip("")]
        [SerializeField]
        [Min(0.01f)]
        private float _refreshTime = 0.11f;

        [Tooltip("The chance that colored blocks will appear in percentage.")]
        [SerializeField]
        [Range(0, 100)]
        private int _chancePercentage = 22;

        public int cuboidsCounter = 0;

        private float minX;
        private float maxX;
        private float minZ;
        private float maxZ;

        void Awake()
        {
            var targetPosition = transform.position;
            var targetSize = gameObject.GetComponent<Renderer>().bounds.size;

            minX = targetPosition.x - (targetSize.x / 2);
            maxX = targetPosition.x + (targetSize.x / 2);

            minZ = targetPosition.z - (targetSize.z / 2);
            maxZ = targetPosition.z + (targetSize.z / 2);
        }

        void Start()
        {
            InvokeRepeating(nameof(CreateRandomCuboid), 1.0f, _refreshTime);
            InvokeRepeating(nameof(CleanCuboids), _refreshTime * 100, _refreshTime * 100);
        }

        private void CreateRandomCuboid()
        {
            var cuboid = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var cuboidRenderer = cuboid.GetComponent<Renderer>();

            var cuboidColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

            var cuboidPosition = new Vector3(Random.Range(minX, maxX), Random.Range(50.0f, 250.0f), Random.Range(minZ, maxZ));
            var cuboidScale = new Vector3(Random.Range(0.1f, 1.5f), Random.Range(0.1f, 1.5f), Random.Range(0.1f, 1.5f));
            var cuboidRotation = new Vector3(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));

            cuboid.AddComponent<Rigidbody>();
            cuboid.transform.position = cuboidPosition;
            cuboid.transform.Rotate(cuboidRotation, Space.Self);
            cuboid.transform.localScale = cuboidScale;
            cuboid.tag = "Cuboid";

            if (Random.Range(1, 100) > _chancePercentage)
            {
                var cuboidMaterial = Resources.Load("Materials/Concretish", typeof(Material)) as Material;

                cuboidRenderer.material = cuboidMaterial;
            }
            else
            {
                var cuboidMaterial = Instantiate<Material>(Resources.Load("Materials/Default", typeof(Material)) as Material);

                cuboidMaterial.SetColor("_BaseColor", cuboidColor);
                cuboidMaterial.SetColor("_EmissionColor", cuboidColor);
                cuboidRenderer.material = cuboidMaterial;
            }

            cuboidsCounter++;
        }

        private void CleanCuboids()
        {
            var cuboids = GameObject.FindGameObjectsWithTag("Cuboid");

            if (cuboids.Length > 0)
            {
                foreach (GameObject cuboid in cuboids)
                {
                    if (cuboid.transform.position.y < -100.0f)
                    {
                        Destroy(cuboid);

                        cuboidsCounter--;
                    }
                }
            }
        }
    }
}
