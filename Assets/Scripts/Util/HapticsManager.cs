using UnityEngine;

namespace Util
{
    public class HapticsManager : MonoBehaviour
    {
        
        #region SINGELTION

        private static HapticsManager _instance;
        public static HapticsManager Instance => _instance;

        #endregion
        /*-------------------------------------------------------------------------*/

        private void Awake()
        {
            _instance = this;
        }

        public void PlayHaptics()
        {
            Handheld.Vibrate();
        }
    
    }
}