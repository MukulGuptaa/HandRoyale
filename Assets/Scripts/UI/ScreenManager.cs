using DataManager;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Script responsible for handling the screens show tasks.
    /// </summary>
    public class ScreenManager : MonoBehaviour
    {
        #region GAMEOBJECT_REFERENCES

        [SerializeField] private Transform canvasTransform;
        [SerializeField] private GameObject gamePlayScreen;
        [SerializeField] private GameObject menuScreen;

        #endregion
        /*-------------------------------------------------------------------------*/

        #region PRIVATE_VARIABLES

        private GameObject _currentlyOpenScreen;

        #endregion
        /*-------------------------------------------------------------------------*/

        #region SINGELTON

        public static ScreenManager Instance { get; private set; }

        #endregion
        /*-------------------------------------------------------------------------*/
        
        
        private void Awake()
        {
            Instance = this;
            GameManager.OnGameInitialized += ShowScreenPostGameInitialize;
        }

        private void OnDestroy()
        {
            GameManager.OnGameInitialized -= ShowScreenPostGameInitialize;
        }

        /// <summary>
        /// Called post the initial initialize operation post game launch is complete.
        /// </summary>
        private void ShowScreenPostGameInitialize()
        {
            ShowMenuScreen();
        }

        public void ShowMenuScreen(params object[] args)
        {
            if (_currentlyOpenScreen) {
                Destroy(_currentlyOpenScreen);
            }
            _currentlyOpenScreen = Instantiate(menuScreen, canvasTransform);
            _currentlyOpenScreen.GetComponent<MenuScreen>().Initialize(args);
        }

        public void ShowGamePlayScreen()
        {
            if (_currentlyOpenScreen) {
                Destroy(_currentlyOpenScreen);
            }
            _currentlyOpenScreen = Instantiate(gamePlayScreen, canvasTransform);
        }
        
    }
}
