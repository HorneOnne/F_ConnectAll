using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ConnectAll
{
    public class LevelBtn : MonoBehaviour
    {
        [SerializeField] private Button _levelBtn;
        [SerializeField] private Image _levelImage;
        [SerializeField] private TextMeshProUGUI _levelText;

        [Header("Data")]
        public LevelData LevelData;

        private void Start()
        {
            _levelBtn.onClick.AddListener(() =>
            {
                if (LevelData.IsLocking == false)
                {
                    GameManager.Instance.PlayingLevelData = LevelData;
                    Loader.Load(Loader.Scene.GameplayScene);

                    SoundManager.Instance.PlaySound(SoundType.Button, false);
                }
            });
        }


        private void OnDestroy()
        {
            _levelBtn.onClick.RemoveAllListeners();
        }


        public void LoadLevelDataState()
        {
            if (LevelData == null) return;
            if (LevelData.IsLocking)
                SetOpacity(0.4f);
            else
                SetOpacity(1.0f);

            _levelText.text = LevelData.Level.ToString();
        }


        private void SetOpacity(float alpha)
        {
            Color currentImageColor = _levelImage.color;
            currentImageColor.a = alpha;
            _levelImage.color = currentImageColor;

            Color currentTextColor = _levelText.color;
            currentTextColor.a = alpha;
            _levelText.color = currentTextColor;
        }
    }
}
