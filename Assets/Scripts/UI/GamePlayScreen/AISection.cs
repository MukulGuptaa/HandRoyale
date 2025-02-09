using System.Collections.Generic;
using DataManager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UI.GamePlayScreen
{
    public class AISection : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] private TMP_Text aiFinalText;
        [SerializeField] private Sprite[] moveSprites;
        [SerializeField] private Image finalMoveImage;
        [SerializeField] private GameObject aiPlaySection;
        [SerializeField] private GameObject aiWaitingTextSection;
        
        #endregion
        /*-------------------------------------------------------------------------*/

        #region PRIVATE_VARIABLE

        private Dictionary<Move, Sprite> _moveToSpriteMap;

        #endregion
        /*-------------------------------------------------------------------------*/
        
        public void Initialize()
        {
            GameEvents.OnPlayerMoveChoiceSelected += OnPlayerMoveChoiceSelected;
            
            _moveToSpriteMap = new Dictionary<Move, Sprite>();
            for (int i = 0; i < moveSprites.Length; i++) {
                _moveToSpriteMap.Add((Move)i, moveSprites[i]);
            }
        }

        private void OnPlayerMoveChoiceSelected(Move _)
        {
            aiWaitingTextSection.SetActive(false);
            aiPlaySection.SetActive(true);
            var aiMove = GameManager.Instance.AiMove;
            aiFinalText.text = "Sit tight, AI's move was..";
            
            Sequence s = DOTween.Sequence();
            float iterationDelay = 0.4f; // Time between each sprite change
            
            // Add sprite changes to sequence
            for (int i = 0; i < moveSprites.Length; i++)
            {
                int currentSpriteIndex = i;
                s.AppendCallback(() => finalMoveImage.sprite = moveSprites[currentSpriteIndex]).AppendInterval(iterationDelay);
            }

            s.InsertCallback(1.2f, () => SoundManager.Instance.PlaySelectionRevealSound());

            // Add final state
            s.AppendCallback(() => 
            {
                aiFinalText.text = "AI Played";
                finalMoveImage.sprite = _moveToSpriteMap[aiMove];
                GameEvents.RaiseOnAiMoveChoiceShownInUi();
                HapticsManager.Instance.PlayHaptics();
            });
            s.AppendInterval(3f);
            s.AppendCallback(() =>
            {
                ScreenManager.Instance.ShowMenuScreen(GameManager.Instance.LastGameResultStatus);
            });

        }
        
        private void OnDestroy()
        {
            GameEvents.OnPlayerMoveChoiceSelected -= OnPlayerMoveChoiceSelected;
        }
        
    }
}
