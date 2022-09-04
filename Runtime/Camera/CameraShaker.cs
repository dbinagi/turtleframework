using System.Collections;
using UnityEngine;
using Cinemachine;
using TurtleGames.Framework.Runtime.Core;

namespace TurtleGames.Framework.Runtime.Camera
{

    public class CameraShaker: Singleton<CameraShaker>
    {

        [SerializeField]
        public float intensity = 5f;

        [SerializeField]
        public float duration = 0.5f;

        [SerializeField]
        public CinemachineVirtualCamera virtualCamera;

        CinemachineBasicMultiChannelPerlin noise;

        private void Start()
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise == null){
                Debug.LogError("Noise not found in Virtual Camera");
            }else if (noise.m_NoiseProfile == null){
                Debug.LogError("Profile not set in Noise from Virtual Camera");
            }
        }

        IEnumerator ProcessShake(float intensity, float duration)
        {
            Noise(1, intensity);
            yield return new WaitForSeconds(duration);
            Noise(0, 0);
        }

        [ContextMenu("Shake")]
        public void Shake() {
            StartCoroutine(ProcessShake(intensity, duration));
        }

        public void Shake(float intensity, float duration){
            StartCoroutine(ProcessShake(intensity, duration));
        }

        void Noise(float amplitudeGain, float frequencyGain)
        {
            noise.m_AmplitudeGain = amplitudeGain;
            noise.m_FrequencyGain = frequencyGain;
        }

    }

}
