using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ConnectAll
{
    public class UIGameplay : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _backBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _levelText;

        private void Start()
        {
            LoadLevelText();

            _backBtn.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.MenuScene);

                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            _backBtn.onClick.RemoveAllListeners();
        }

        private void LoadLevelText()
        {
            _levelText.text = $"LEVEL {GameManager.Instance.PlayingLevelData.Level}";
        }
    }
}
