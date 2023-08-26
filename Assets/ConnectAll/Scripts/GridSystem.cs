using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ConnectAll
{
    public class GridSystem : MonoBehaviour
    {
        public static GridSystem Instance { get; private set; }

        [Header("References")]
        //[SerializeField] private HiddenBlock _hiddenBlockPrefab;

        [Header("Data")]
        private LevelData _levelData;

        [Header("Properties")]
        [SerializeField] private float _gridSpacing = 0.2f;


        // Cached
        //private HiddenBlock[] _gridMap;



        private void Awake()
        {
            Instance = this;
        }



        private void Start()
        {
            //LoadLevelData();
            //CreateGrid();

            //StartCoroutine(Utilities.WaitAfter(1.0f, () =>
            //{
            //    StartCoroutine(PerformToggleWhenStart(() =>
            //    {
            //        GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.PLAYING);
            //    }));
            //}));

        }


        //private void LoadLevelData()
        //{
        //    // Load Levedata from GameManger.
        //    this._levelData = GameManager.Instance.PlayingLevelData;

        //    var mainCam = Camera.main;
        //    mainCam.orthographicSize = _levelData.OrthographicCameraSize;
        //    Vector3 newPosition = new Vector3(mainCam.transform.position.x + _levelData.CameraOffset.x, mainCam.transform.position.y + _levelData.CameraOffset.y, mainCam.transform.position.z);
        //    mainCam.transform.position = newPosition;

        //    _gridMap = new HiddenBlock[_levelData.Width * _levelData.Height];
        //}

        //private void CreateGrid()
        //{
        //    int rows = _levelData.Width;
        //    int columns = _levelData.Height;

        //    // Calculate the center position of the nodes.
        //    Vector3 centerOffset = new Vector3((columns - 1) * _gridSpacing * 0.5f, -(rows - 1) * _gridSpacing * 0.5f, 0f);

        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < columns; j++)
        //        {
        //            Vector3 position = new Vector3(i * _gridSpacing, -j * _gridSpacing, 0f) - centerOffset;
        //            _gridMap[i + rows * j] = Instantiate(_hiddenBlockPrefab, position, Quaternion.identity, this.transform);
        //        }
        //    }
        //}
    }
}