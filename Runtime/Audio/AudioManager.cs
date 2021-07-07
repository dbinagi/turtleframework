using System.Collections;
using UnityEngine;
using TurtleGames.Framework.Runtime.Core;

namespace TurtleGames.Framework.Runtime.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {

    	[SerializeField]
    	public SOAudioConfig config;

        bool toggleMute;

        #region "Public Functions"

        public void Play(string name, bool startMuted = false, float pitch = float.NegativeInfinity, float volume = float.NegativeInfinity, bool loop = false)
        {
            Sound sound = config.GetSoundByName(name);
            AudioSource audioSource = SetupVolumeAndPitch(sound.source, sound, startMuted ? 0 : volume, pitch);
            audioSource.loop = sound.Loop;
            audioSource.Play();
        }

        public void PlayOneShot(string name, float pitch = float.NegativeInfinity, float volume = float.NegativeInfinity)
        {
            Sound sound = config.GetSoundByName(name);
            AudioSource audioSource = SetupVolumeAndPitch(sound.source, sound, volume, pitch);
            audioSource.PlayOneShot(sound.clips[0], audioSource.volume);
        }

        public void ToggleMute(string name)
        {
            Sound param = config.GetSoundByName(name);
            param.source.mute = !param.source.mute;
        }

        public void ToggleMuteAll()
        {
            toggleMute = !toggleMute;

            if (toggleMute)
                MuteAll();
            else
                UnmuteAll();
        }

        public void MuteAll(){
            AudioListener.volume = 0f;
        }

        public void UnmuteAll(){
            AudioListener.volume = 1f;
        }

        public void FadeIn(string name, float duration = float.NegativeInfinity, float volume = float.NegativeInfinity)
        {
            Sound sound = config.GetSoundByName(name);
            AudioSource audioSource = SetupVolumeAndPitch(sound.source, sound, 0, sound.Pitch);
            audioSource.loop = sound.Loop;
            audioSource.Play();
            StartCoroutine(FadeInCoroutine(audioSource, duration == float.NegativeInfinity ? config.DefaultFade : duration, volume == float.NegativeInfinity ? sound.Volume : volume));
        }

        public void FadeOut(string name, float duration = float.NegativeInfinity, float volume = float.NegativeInfinity)
        {
            Sound sound = config.GetSoundByName(name);
            AudioSource audioSource = SetupVolumeAndPitch(sound.source, sound, volume == float.NegativeInfinity ? 0 : volume, sound.Pitch);
            StartCoroutine(FadeOutCoroutine(audioSource, duration == float.NegativeInfinity ? config.DefaultFade : duration, volume));
        }

        public void PlayRandomInGroup(string name, bool startMuted = false, float pitch = float.NegativeInfinity, float volume = float.NegativeInfinity, bool loop = false)
        {
            Sound sound = config.GetSoundByName(name);
            AudioSource audioSource = SetupVolumeAndPitch(sound.source, sound, startMuted ? 0 : volume, pitch);
            audioSource.loop = sound.Loop;
            audioSource.clip = sound.GetRandomClip();
            audioSource.Play();
        }

        public Sound GetSound(string name)
        {
            Sound sound = config.GetSoundByName(name);
            return sound;
        }

        #endregion

        #region "Private Functions"

        private void Initialize()
        {
            // Creates Audio Source for each sound
            foreach (var sound in config.Sounds)
            {
                var audioSource = BuildAudioSource(sound.clips[0], sound.Volume, sound.Pitch, sound.Name, sound.Loop, false);
                sound.source = audioSource;
            }
        }

        private AudioSource BuildAudioSource(AudioClip clip, float volume, float pitch, string name, bool loop, bool playOnAwake)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.loop = loop;
            audioSource.playOnAwake = playOnAwake;

            return audioSource;
        }

        private IEnumerator FadeInCoroutine(AudioSource source, float duration, float destinationVolume)
        {
            float currentDuration = 0;

            while (currentDuration < duration)
            {
                source.volume = Mathf.Lerp(0, destinationVolume, currentDuration / duration);

                currentDuration += Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }

            source.volume = destinationVolume;
        }

        private IEnumerator FadeOutCoroutine(AudioSource source, float duration, float destinationVolume)
        {
            float currentDuration = 0;

            float startVolume = source.volume;

            while (currentDuration < duration)
            {
                source.volume = Mathf.Lerp(startVolume, destinationVolume, currentDuration / duration);

                currentDuration += Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }

            source.volume = destinationVolume;
        }

        private AudioSource SetupVolumeAndPitch(AudioSource source, Sound sound, float volume, float pitch)
        {
            if (pitch == float.NegativeInfinity)
                source.pitch = sound.GetValueWithVariance(sound.Pitch, sound.PitchVariance);
            else
                source.pitch = sound.GetValueWithVariance(pitch, sound.PitchVariance);

            if (volume == float.NegativeInfinity)
                source.volume = sound.GetValueWithVariance(sound.Volume, sound.VolumeVariance);
            else
                source.volume = sound.GetValueWithVariance(volume, sound.VolumeVariance);

            return source;
        }

        protected override void OnAwake()
        {
            Initialize();
        }

        #endregion

    }
}