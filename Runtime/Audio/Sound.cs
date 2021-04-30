
using System.Collections.Generic;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Audio
{
    [System.Serializable]
    public class Sound
    {
        public string Name;
        [Range(0, 1)]
        public float Volume;
        [Range(0, 1)]
        public float VolumeVariance;
        [Range(0, 1)]
        public float Pitch;
        [Range(0, 1)]
        public float PitchVariance;

        public bool Loop;

        [SerializeField]
        public Transform SourceParent;

        [SerializeField, Range(0, 1)]
        public float SpatialBlend;

		public List<AudioClip> clips;

        [HideInInspector]
        public AudioSource source;

        public AudioClip GetRandomClip()
        {
            return clips[Random.Range(0, clips.Count)];
        }

        public float GetValueWithVariance(float value, float variance)
        {
            float newValue = value;
            if (variance > 0)
                newValue = Random.Range(value - variance / 2, value + variance / 2);
            return Mathf.Clamp(newValue, 0, 1);
        }
    }
}
