using UnityEngine;

namespace Util
{
    public class SoundManager : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] private AudioSource audioSource;
        
        [SerializeField] private AudioClip aiSelectionRevealSound;
        [SerializeField] private AudioClip lossSound;
        [SerializeField] private AudioClip winSound;
        [SerializeField] private AudioClip playerOptionSelectSound;
        [SerializeField] private AudioClip buttonClickSound;

        #endregion
        /*-------------------------------------------------------------------------*/

        #region SINGELTION

        private static SoundManager _instance;
        public static SoundManager Instance => _instance;

        #endregion
        /*-------------------------------------------------------------------------*/

        private void Awake()
        {
            _instance = this;
        }

        public void PlaySelectionRevealSound()
        {
            audioSource.PlayOneShot(aiSelectionRevealSound);
        }

        public void PlayLossSound()
        {
            audioSource.PlayOneShot(lossSound);
        }

        public void PlayWinSound()
        {
            audioSource.PlayOneShot(winSound);
        }

        public void PlayPlayerOptionSelectSound()
        {
            audioSource.PlayOneShot(playerOptionSelectSound);
        }

        public void PlayButtonClickSound()
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    
    }
}
