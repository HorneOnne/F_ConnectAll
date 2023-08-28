using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace ConnectAll
{
    public class UIMainMenu : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _playBtn;
        [SerializeField] private Button _musicBtn;
        [SerializeField] private Button _soundBtn;
        [SerializeField] private Button _informationBtn;

        [SerializeField] private Image _musicBtnIcon;
        [SerializeField] private Image _soundBtnIcon;


        private const string PRIVARY_URL = "https://doc-hosting.flycricket.io/connect-all-privacy-policy/933a6da5-0f9d-4a7e-98a0-dfe36b67828d/privacy";


        private void Start()
        {
            UpdateMusicUI();
            UpdateSoundFXUI();

            _playBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayLevelMenu(true);
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _musicBtn.onClick.AddListener(() =>
            {
                ToggleMusic();
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _soundBtn.onClick.AddListener(() =>
            {
                ToggleSFX();
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });

            _informationBtn.onClick.AddListener(() =>
            {
                Application.OpenURL(PRIVARY_URL);
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            _playBtn.onClick.RemoveAllListeners();
            _musicBtn.onClick.RemoveAllListeners();
            _soundBtn.onClick.RemoveAllListeners();
            _informationBtn.onClick.RemoveAllListeners();
        }


        private void ToggleSFX()
        {

            SoundManager.Instance.MuteSoundFX(SoundManager.Instance.isSoundFXActive);
            SoundManager.Instance.isSoundFXActive = !SoundManager.Instance.isSoundFXActive;

            UpdateSoundFXUI();
        }


        private void UpdateSoundFXUI()
        {
            if (SoundManager.Instance.isSoundFXActive)
            {
                //_soundBtn.image.sprite = _unmuteBtnSprite;
                SetImageOpacity(_soundBtnIcon, 1.0f);
                SetImageOpacity(_soundBtn.image, 1.0f);
            }
            else
            {
                //_soundBtn.image.sprite = _muteBtnSprite;
                SetImageOpacity(_soundBtnIcon, 0.4f);
                SetImageOpacity(_soundBtn.image, 0.4f);
            }
        }

        private void ToggleMusic()
        {
            SoundManager.Instance.MuteBackground(SoundManager.Instance.isMusicActive);
            SoundManager.Instance.isMusicActive = !SoundManager.Instance.isMusicActive;

            UpdateMusicUI();
        }

        private void UpdateMusicUI()
        {
            if (SoundManager.Instance.isMusicActive)
            {
                //_musicBtn.image.sprite = _unmuteBtnSprite;
                SetImageOpacity(_musicBtnIcon, 1.0f);
                SetImageOpacity(_musicBtn.image, 1.0f);
            }
            else
            {
                //_musicBtn.image.sprite = _muteBtnSprite;
                SetImageOpacity(_musicBtnIcon, 0.4f);
                SetImageOpacity(_musicBtn.image, 0.4f);
            }
        }

        public void SetImageOpacity(Image image, float alpha)
        {
            Color currentImageColor = image.color;
            currentImageColor.a = alpha;
            image.color = currentImageColor;
        }
    }
}
