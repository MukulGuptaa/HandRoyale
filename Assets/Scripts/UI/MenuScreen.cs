using DataManager;
using TMPro;
using UnityEngine;
using Util;

namespace UI
{

    public enum MenuScreenType
    {
        GameOver,
        MainMenu,
    }
    
    public class MenuScreen : MonoBehaviour
    {
        #region REFERENCES
    
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private GameObject gameOverSection;
        [SerializeField] private RectTransform playButtonRt;
        [SerializeField] private TMP_Text highScoreText;
        
        #endregion
        /*-------------------------------------------------------------------------*/
        
        #region PRIVATE FIELDS

        private MenuScreenType _menuScreenType;
        private GameResultStatus _gameResultStatus;
        
        #endregion
        /*-------------------------------------------------------------------------*/

        #region CONSTANTS

        private const string HIGH_SCORE_TEXT = "Current High Score : {}";

        #endregion
        /*-------------------------------------------------------------------------*/

        public void Initialize(params object[] args)
        {
            if (args != null && args.Length > 0 && args[0] is GameResultStatus) {
                _gameResultStatus = (GameResultStatus)args[0];
                _menuScreenType = MenuScreenType.GameOver;
                
            }else {
                _menuScreenType = MenuScreenType.MainMenu;
            }
            SetupUi();
        }

        private void SetupUi()
        {
            if (_menuScreenType == MenuScreenType.GameOver) {
                switch (_gameResultStatus)
                {
                    case GameResultStatus.WON:
                        resultText.text = "YOU WIN!";
                        SoundManager.Instance.PlayWinSound();
                        break;
                    case GameResultStatus.LOSS:
                        resultText.text = "YOU LOST!";
                        SoundManager.Instance.PlayLossSound();
                        break;
                    case GameResultStatus.LOSS_BY_TIMEOUT:
                        resultText.text = "YOU LOST BY TIMEOUT!";
                        SoundManager.Instance.PlayLossSound();
                        break;
                    case GameResultStatus.TIE:
                        resultText.text = "IT'S A TIE!";
                        SoundManager.Instance.PlayLossSound();
                        break;
                }
            }else {
                gameOverSection.SetActive(false);
                playButtonRt.anchoredPosition = Vector2.zero;
            }

            highScoreText.text = HIGH_SCORE_TEXT.Replace("{}", GameManager.Instance.HighScore.ToString());
        }

        public void PlayButtonClicked()
        {
            ScreenManager.Instance.ShowGamePlayScreen();
            SoundManager.Instance.PlayButtonClickSound();
            HapticsManager.Instance.PlayHaptics();
        }
        
    }
}
