using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace CarGame
{
    public class DeathScreenManager : Singleton<DeathScreenManager>
    {
        [SerializeField] private TextMeshProUGUI _score;

        public void DisplayScore(int score)
        {
            _score.text = score.ToString();
        }

        public void OnPlayAgain()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

        public void OnExit()
        {
            Application.Quit();
        }

    }
}
