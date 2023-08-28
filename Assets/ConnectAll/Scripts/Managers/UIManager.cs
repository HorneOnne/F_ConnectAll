using UnityEngine;

namespace ConnectAll
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public UIMainMenu UIMainMenu;
        public UILevel UILevel;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CloseAll();
            DisplayMainMenu(true);
        }

        public void CloseAll()
        {
            DisplayMainMenu(false);
            DisplayLevelMenu(false);
        }

        public void DisplayMainMenu(bool isActive)
        {
            UIMainMenu.DisplayCanvas(isActive);
        }


        public void DisplayLevelMenu(bool isActive)
        {
            UILevel.DisplayCanvas(isActive);
        }

    }
}