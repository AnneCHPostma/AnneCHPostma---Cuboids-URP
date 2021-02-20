using UnityEngine;
using TMPro;
using System.Collections;

namespace AnneCHPostma.Cuboids
{
    [RequireComponent(typeof(GameObject))]
    public class UpdateFPSCounter : MonoBehaviour
    {
        [Tooltip("Attach the GameObject that is updating the FPS counter")]
        [SerializeField]
        private GameObject _fpsCounter = default;

        private TextMeshProUGUI fpsCounter = null;

        private float framesPerSecond = 0.0f;

        // Update rate in seconds
        private readonly float updateRate = 0.25f;

        private void Start()
        {
            fpsCounter = _fpsCounter.GetComponent<TextMeshProUGUI>();

            StartCoroutine(DelayCoroutine(UpdateFramesPerSecond(updateRate), 1.0f));
        }

        private void Update()
        {
            if (fpsCounter == null)
            {
                Debug.LogError("No TextMeshProUGUI component is attached to the FPSCounter GameObject.");

                return;
            }

            fpsCounter.text = string.Format("{0:0.00} fps", framesPerSecond);
        }

        /// <summary>
        /// Start a Coroutine after x seconds.
        /// </summary>
        /// <param name="coroutine">The Coroutine to be invoked.</param>
        /// <param name="seconds">The amount of time before invoke the Coroutine.</param>
        IEnumerator DelayCoroutine(IEnumerator coroutine, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            yield return coroutine;
        }

        /// <summary>
        /// Update the framesPerSecond variable.
        /// Based on code from https://gist.github.com/mstevenson/5103365
        /// </summary>
        /// <param name="updateDelay">The time to delay the update (in seconds). Default is a 1 second delay.</param>
        IEnumerator UpdateFramesPerSecond(float updateDelay = 1.0f)
        {
            while (true)
            {
                if (Time.timeScale == 1.0f)
                {
                    yield return new WaitForSeconds(0.1f);

                    framesPerSecond = (1.0f / Time.unscaledDeltaTime);
                }

                yield return new WaitForSeconds(updateDelay);
            }
        }
    }
}
