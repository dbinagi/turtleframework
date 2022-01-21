using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Unity.Collections;

namespace TurtleGames.Framework.Runtime.Command
{
    [CreateAssetMenu(fileName = "CommandConfig", menuName = "TurtleFramework/Command/CommandConfig", order = 1)]
    public class SOCommandConfig : ScriptableObject
    {

        [SerializeField]
        public string CommandPath = "Assets\\Scripts\\Commands";

        [SerializeField]
        KeyCode keyToOpen;

        [SerializeField]
        KeyCode keyToSubmit;


        [SerializeField]
        float height;

        [SerializeField]
        Font font;

        [SerializeField]
        Color color;

        [SerializeField]
        int maxConsoleOutputLines;

        [SerializeField]
        string gameObjectName = "CommandDialog";

        /*[SerializeField]
        List<ScriptableObject> commandsScripts = new List<ScriptableObject>();*/
        //List<UnityEngine.Object> commandsScripts = new List<UnityEngine.Object>();

        [SerializeField]
        List<string> CommandsListReference;
        



        public KeyCode KeyToOpen { get => keyToOpen; set => keyToOpen = value; }
        public KeyCode KeyToSubmit { get => keyToSubmit; set => keyToSubmit = value; }
        public float Height { get => height; set => height = value; }
        public Font Font { get => font; set => font = value; }
        public Color Color { get => color; set => color = value; }
        public int MaxConsoleOutputLines { get => maxConsoleOutputLines; set => maxConsoleOutputLines = value; }
        //public List<ScriptableObject> CommandsScripts { get => commandsScripts; set => commandsScripts = value; }
        
        public string GameObjectName { get => gameObjectName; set => gameObjectName = value; }

        /*public void LoadCommands()
        {

            AvailableCommands = new List<Command>();

            HelpCommand helpCommand = ScriptableObject.CreateInstance(typeof(HelpCommand)) as HelpCommand;
            AvailableCommands.Add(helpCommand);
            CommandsListReference.Add(helpCommand.Pattern());


            string[] guids1 = AssetDatabase.FindAssets("t:Script", new[] {CommandPath});

            foreach (string guid1 in guids1)
            {

                string path = AssetDatabase.GUIDToAssetPath(guid1);

                MonoScript ms = (MonoScript)AssetDatabase.LoadAssetAtPath(path, typeof(MonoScript)) as MonoScript;

                if(ms != null){

                    Command c = ScriptableObject.CreateInstance(ms.GetClass()) as Command;
                    AvailableCommands.Add(c);
                    CommandsListReference.Add(c.Pattern());
                }
            }

            Debug.Log("Added " + AvailableCommands.Count + " commands");
        }*/

    }

}