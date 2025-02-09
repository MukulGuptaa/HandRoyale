using DataManager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UI.GamePlayScreen
{
    /// <summary>
    /// Script attached ti the player section gameobject in the GamePlayScreen.
    /// Responsible for handling the player section operations.
    /// </summary>
    public class PlayerSection : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] private TMP_Text timerText;
        [SerializeField] private GameObject playerChoiceSelectSection;
        [SerializeField] private GameObject playerChoiceResultSection;
        [SerializeField] private Button[] moveChoicesButtons;
        [SerializeField] private RectTransform playerChoiceResultSectionRt;
        [SerializeField] private CanvasGroup playerChoiceSelectSectionCg;
        [SerializeField] private RectTransform finalChoiceMovePos;
        [SerializeField] private TMP_Text youPlayedText;

        
        
        #endregion
        /*-------------------------------------------------------------------------*/

        
        #region ANIMATION_REFERENCES

        [SerializeField] private Image timerImage;
        [SerializeField] private RectTransform barImage;

        #endregion
        /*-------------------------------------------------------------------------*/
        
        #region PRIVATE_VARIABLES

        private Sequence _timerSequence;
        private Sequence _playerChoiceSequence;

        #endregion
        /*-------------------------------------------------------------------------*/

        /// <summary>
        /// As soon as the timer is finished, we will be showing the menu screen.
        /// </summary>
        private void OnTimerFinished()
        {
            ScreenManager.Instance.ShowMenuScreen(GameResultStatus.LOSS_BY_TIMEOUT);
        }

        private void OnDisable()
        {
            TimeManager.OnTimerUpdated -= UpdateTimerUI;
            TimeManager.OnTimerFinished -= OnTimerFinished;
            _timerSequence?.Kill();
            _playerChoiceSequence?.Kill();
            TimeManager.Instance.StopTimer();
        }

        public void Initialize()
        {
            TimeManager.OnTimerUpdated += UpdateTimerUI;
            TimeManager.OnTimerFinished += OnTimerFinished;
            TimeManager.Instance.StartTimer(GameManager.PLAYER_MOVE_TIME_LIMIT);
            
            InitUi();
        }

        private void InitUi()
        {
            for (int i = 0; i < moveChoicesButtons.Length; i++)
            {
                Move move = (Move)i;
                var choiceRt = (moveChoicesButtons[i].gameObject.transform) as RectTransform;
                moveChoicesButtons[i].onClick.AddListener(() => OnPlayerMoveSelected(move, choiceRt));
            }

            PlayTimerAnimation();
        }

        /// <summary>
        /// Called when the player selects its move choice in the ui.
        /// Here, we first stop the timer, the play the animation for showing the player their selected move.
        /// Sound is also played for the option selection.
        /// Also an event is raised that tells that the player has played their move.
        /// </summary>
        /// <param name="move"></param>
        /// <param name="choiceRt"></param>
        private void OnPlayerMoveSelected(Move move, RectTransform choiceRt)
        {
            StopTimer();
            choiceRt.SetParent(playerChoiceResultSectionRt);
            
            _playerChoiceSequence = DOTween.Sequence();
            
            _playerChoiceSequence.InsertCallback(0f, () =>
            {
                SoundManager.Instance.PlayPlayerOptionSelectSound();
                playerChoiceSelectSectionCg.DOFade(0f, 0.5f);
                choiceRt.DOMove(finalChoiceMovePos.position, 0.5f).SetEase(Ease.OutQuad);
                choiceRt.DOScale(1.2f, 0.5f);
            });
            
            _playerChoiceSequence.InsertCallback(0.5f, () =>
            {
                youPlayedText.text = "You Played";
                playerChoiceResultSection.SetActive(true);
                playerChoiceSelectSection.SetActive(false);
                GameEvents.RaiseOnPlayerMoveChoiceSelected(move);
                HapticsManager.Instance.PlayHaptics();
            });

        }

        private void StopTimer()
        {
            TimeManager.Instance.StopTimer();
            barImage.gameObject.SetActive(false);
            timerImage.gameObject.SetActive(false);
            timerText.gameObject.SetActive(false);
        }

        private void UpdateTimerUI(int timeLeft)
        {
            timerText.text = $"Time left : {timeLeft} seconds";
        }

        private void PlayTimerAnimation()
        {
            _timerSequence = DOTween.Sequence();
            _timerSequence.InsertCallback(0f, () =>
            {
                barImage.DOMoveX(barImage.position.x - timerImage.rectTransform.rect.width + 20, GameManager.PLAYER_MOVE_TIME_LIMIT);
                timerImage.DOFillAmount(0f, GameManager.PLAYER_MOVE_TIME_LIMIT);
            });
        }
        
    }
}
