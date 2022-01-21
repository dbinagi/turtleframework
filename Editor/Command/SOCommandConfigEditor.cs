using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.Command
{
	[CustomEditor(typeof(SOCommandConfig))]
    public class SOCommandConfigEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var commandConfig = (SOCommandConfig)target;

            if(GUILayout.Button("Load Commands"))
            {
                //commandConfig.LoadCommands();
            }

        }
    }
}