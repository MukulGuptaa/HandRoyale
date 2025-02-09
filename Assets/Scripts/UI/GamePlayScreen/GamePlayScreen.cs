using UnityEngine;

namespace UI.GamePlayScreen
{
    /// <summary>
    /// Main script that is attached to the GamePlayScreen prefab.
    /// </summary>
    public class GamePlayScreen : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] private AISection aiSection;
        [SerializeField] private PlayerSection playerSection;
        
        #endregion
        /*-------------------------------------------------------------------------*/

        private void OnEnable()
        {
            playerSection.Initialize();
            aiSection.Initialize();
        }
    }
}
