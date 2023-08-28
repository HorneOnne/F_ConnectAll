using UnityEngine;
using UnityEngine.UI;

namespace ConnectAll
{
    public class UIGameplay : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _jumpBtn;



        private void Start()
        {
            _jumpBtn.onClick.AddListener(() =>
            {
           
                SoundManager.Instance.PlaySound(SoundType.Button, false);
            });
        }

        private void OnDestroy()
        {
            _jumpBtn.onClick.RemoveAllListeners();
        }
    }
}
