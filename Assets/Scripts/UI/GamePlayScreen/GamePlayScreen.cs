using UnityEngine;

namespace UI.GamePlayScreen
{
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
