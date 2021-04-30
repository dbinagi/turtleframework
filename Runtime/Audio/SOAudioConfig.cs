using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Audio
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "TurtleFramework/Audio/AudioConfig", order = 1)]
    public class SOAudioConfig : ScriptableObject
    {
        [Range(0, 1)]
        public float DefaultVolume = 0.5f;
        [Range(0, 1)]
        public float DefaultPitch = 1.0f;
        [Range(0, 20)]
        public float DefaultFade = 2.0f;

        public List<Sound> Sounds = new List<Sound>();

        public Sound GetSoundByName(string name)
        {
            Sound sound = Sounds.FirstOrDefault(s => s.Name == name);
            
            if(sound == null)
                throw new Exception("Sound not found: " + name);

            return sound;
        }

        public void LoadAudios()
        {
            AudioClip[] files = Resources.LoadAll<AudioClip>("Audio");

            foreach(var obj in files)
            {
                Sound sound = GetSoundByClip(obj);
                if(sound == null)
                {
                    var sp = new Sound();
                    sp.Volume = DefaultVolume;
                    sp.Pitch = DefaultPitch;
                    sp.clips = new List<AudioClip>();
                    sp.clips.Add(obj);
                    sp.Name = obj.name;

                    Sounds.Add(sp);
                }

            }

        }

        public Sound GetSoundByClip(AudioClip clip)
        {
            if(Sounds != null)
            {
                foreach (var param in Sounds)
                {
                    foreach(var c in param.clips)
                    {
                        if (c == clip)
                            return param;
                    }
                }
            }
            return null;
        }
    }

}