using System;
using UI;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

namespace DataManager
{
    public class GameManager : MonoBehaviour
    {
        #region SINGELTON

        public static GameManager Instance { get; private set; }
        
        #endregion
        /*-------------------------------------------------------------------------*/
        
        #region CONSTANTS

        public const int PLAYER_MOVE_TIME_LIMIT = 7;
        
        #endregion
        /*-------------------------------------------------------------------------*/
        

        #region PRIVATE_VARIABLES
    
        private GameRulesManager _rules;
        private int _highScore;
        private int _currentScore;
        private Move _playerMove;
        private Move _aiMove;
        private GameResultStatus _lastGameResultStatus;
        
        #endregion
        /*-------------------------------------------------------------------------*/

        #region GETTERS

        public GameResultStatus LastGameResultStatus => _lastGameResultStatus;
        public Move AiMove => _aiMove;
        public int HighScore => _highScore;

        #endregion
        /*-------------------------------------------------------------------------*/
        
        
        #region EVENTS
    
        public static Action OnGameInitialized;
        
        #endregion
        /*-------------------------------------------------------------------------*/
        

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGame();
            GameEvents.OnPlayerMoveChoiceSelected += OnPlayerMoveChoiceSelected;
            GameEvents.OnAiMoveChoiceShownInUi += CheckAndUpdateResult;
            TimeManager.OnTimerFinished += OnTimerFinished;
        }

        private void OnDestroy()
        {
            GameEvents.OnPlayerMoveChoiceSelected -= OnPlayerMoveChoiceSelected;
            GameEvents.OnAiMoveChoiceShownInUi -= CheckAndUpdateResult;
            TimeManager.OnTimerFinished -= OnTimerFinished;
        }

        private void InitializeGame()
        {
            _rules = new GameRulesManager();
            LoadHighScore();
            OnGameInitialized?.Invoke();
        }

        private void LoadHighScore()
        {
            if (PlayerPrefs.HasKey("HighScore")) {
                _highScore = PlayerPrefs.GetInt("HighScore");
            }
        }

        private void OnPlayerMoveChoiceSelected(Move move)
        {
            _playerMove = move;
            PlayAiMove();
        }

        private void PlayAiMove()
        {
            _aiMove = (Move) Random.Range(0, 5);
        }

        private void OnTimerFinished()
        {
            _playerMove = Move.None;
            _aiMove = Move.None;
            CheckAndUpdateResult();
        }

        private void CheckAndUpdateResult()
        {
            _lastGameResultStatus = _rules.GetGameResultStatus(_playerMove, _aiMove);
            if (_lastGameResultStatus == GameResultStatus.WON) {
                _currentScore++;
                if (_currentScore > _highScore) {
                    _highScore = _currentScore;
                    PlayerPrefs.SetInt("HighScore", _highScore);
                    PlayerPrefs.Save();
                }
            }else {
                _currentScore = 0;
            }
        }
        
    }
}
