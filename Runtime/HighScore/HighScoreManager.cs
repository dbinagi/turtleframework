using System.Collections;
using System.Collections.Generic;
using TurtleGames.Framework.Runtime.Core;
using UnityEngine;

namespace TurtleGames.Framework.Runtime.HighScore
{

    public class HighScoreManager : Singleton<HighScoreManager>
    {

        public int ScoresToKeep;

        public string PlayerPrefName;

        List<int> scores;

        protected override void OnAwake()
        {
            base.OnAwake();
            this.scores = LoadFromPlayerPref();
        }

        public List<int> GetTop(int amount)
        {
            List<int> auxScores = new List<int>();

            foreach (int i in this.scores)
            {
                auxScores.Add(i);
            }

            for (int i = 0; i < amount - this.scores.Count; i++)
            {
                auxScores.Add(0);
            }

            auxScores.Sort((a, b) => b.CompareTo(a));

            return auxScores.GetRange(0, amount);
        }

        public void SetNewScore(int score)
        {
            scores.Add(score);
            scores.Sort((a, b) => b.CompareTo(a));
            scores.GetRange(0, scores.Count >= ScoresToKeep ? ScoresToKeep : scores.Count);

            UpdatePlayerPref(scores);
        }

        public void ClearScores()
        {
            PlayerPrefs.DeleteKey(PlayerPrefName);
        }

        List<int> LoadFromPlayerPref()
        {
            scores = new List<int>();

            string pref = PlayerPrefs.GetString(PlayerPrefName, "0");
            if (pref != null)
            {
                string[] prefString = pref.Split(',');
                foreach (string s in prefString)
                {
                    scores.Add(int.Parse(s));
                }
            }
            return scores;
        }

        void UpdatePlayerPref(List<int> scores)
        {
            string scoresString = "";

            foreach (int score in scores)
            {
                scoresString += score + ",";
            }

            // Remove last comma
            scoresString = scoresString.Remove(scoresString.Length - 1);

            PlayerPrefs.SetString(PlayerPrefName, scoresString);
        }
    }
}