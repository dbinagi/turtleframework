using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Audio
{
	[CustomEditor(typeof(SOAudioConfig))]
    public class SOAudioConfigEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var audioConfig = (SOAudioConfig)target;

            if(GUILayout.Button("Load Sounds"))
            {
                audioConfig.LoadAudios();
            }

        }
    }
}