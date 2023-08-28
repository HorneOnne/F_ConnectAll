using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectAll
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public static event System.Action OnScoreUp;

        // SCORE & BEST
        private int _score;
        private int _bestScore;


        public List<LevelData> LevelsData;
        public LevelData PlayingLevelData;


        #region Properties
        public int Score { get => _score; }
        public int BestScore { get => _bestScore; }
        #endregion
        private void Awake()
        {
            // Check if an instance already exists, and destroy the duplicate
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            // FPS
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            // Make the GameObject persist across scenes
            DontDestroyOnLoad(this.gameObject);
        }


        public void ScoreUp()
        {
            _score++;
            OnScoreUp?.Invoke();
        }

        public void ScoreUp(int value)
        {
            _score += value;
            OnScoreUp?.Invoke();
        }

        public void ResetScore()
        {
            this._score = 0;
        }

        public void SetBestScore(int score)
        {
            this._score = score;
            if (_bestScore < score)
            {
                _bestScore = score;
            }
        }

        public void LevelUp()
        {
            int currentLevel = PlayingLevelData.Level;
            int nextLevel = currentLevel + 1;

            for (int i = 0; i < LevelsData.Count; i++)
            {
                if (LevelsData[i].Level == nextLevel)
                {
                    PlayingLevelData = LevelsData[i];
                    break;
                }
            }
        }
    }
}