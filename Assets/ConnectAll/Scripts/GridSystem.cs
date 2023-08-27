using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;


namespace ConnectAll
{
    public class GridSystem : MonoBehaviour
    {
        public static GridSystem Instance { get; private set; }

        [Header("References")]
        [SerializeField] private GridSlot _gridSlotPrefab;
        [SerializeField] private Chip _redChip;
        [SerializeField] private Chip _greenChip;

        [Header("Data")]
        [SerializeField] private LevelData _levelData;

        [Header("Properties")]
        [SerializeField] private float _gridSpacing = 0.2f;


        // Cached
        private GridSlot[] _gridMap;
        [SerializeField] private List<GridSlot> _listSlotHasChip;



        private void Awake()
        {
            Instance = this;
        }



        private void Start()
        {
            //LoadLevelData();
            CreateGrid();
            LoadGridData();

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


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                bool canWin = CheckWinCondition();
                Debug.Log(canWin);
            }
        }

        private void CreateGrid()
        {
            int rows = _levelData.Width;
            int columns = _levelData.Height;
            _gridMap = new GridSlot[rows * columns];

            // Calculate the center position of the nodes.
            Vector3 centerOffset = new Vector3((columns - 1) * _gridSpacing * 0.5f, -(rows - 1) * _gridSpacing * 0.5f, 0f);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Vector3 position = new Vector3(i * _gridSpacing, -j * _gridSpacing, 0f) - centerOffset;
                    _gridMap[i + rows * j] = Instantiate(_gridSlotPrefab, position, Quaternion.identity, this.transform);
                }
            }
        }

        private void LoadGridData()
        {
            int rows = _levelData.Width;
            int columns = _levelData.Height;


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Chip chip = CreateChip(_levelData.ChipData[i + rows * j].ChipType, _gridMap[i + rows * j].transform.position);

                    if(_levelData.ChipData[i + rows * j].ChipType != Chip.ChipType.Null)
                    {
                        chip.LoadChipData(_levelData.ChipData[i + rows * j]);
                        _gridMap[i + rows * j].SetChip(chip);
                    }
                    
                }
            }
        }
        private Chip CreateChip(Chip.ChipType chipType, Vector2 position)
        {
            Chip chip;
            switch(chipType)
            {
                case Chip.ChipType.Red:
                    chip =  Instantiate(_redChip, position, Quaternion.identity);
                    break;
                case Chip.ChipType.Green:
                    chip = Instantiate(_greenChip, position, Quaternion.identity);
                    break;
                default:
                case Chip.ChipType.Null:
                    chip = null;
                    break;
            }

            return chip;
        }


        public bool CheckWinCondition()
        {
            bool canWin = false;
   

            _listSlotHasChip.Clear();

            int rows = _levelData.Width;
            int columns = _levelData.Height;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (_gridMap[i + rows * j].HasChip())
                    {
                        _listSlotHasChip.Add(_gridMap[i + rows * j]);
                    }
                }
            }
           

            foreach (var slot in _listSlotHasChip)
            {
                GridSlot nb;
                if (slot.Chip.UpConnectors.NumOfConnector > 0)
                {
                    nb = GetDirectionNB(slot, Direction.Up);
                    if (nb != null && nb.HasChip())
                    {
                    
                        canWin = slot.Chip.CanConnect(Direction.Up, nb.Chip);

                        if (canWin == false)
                            break;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (slot.Chip.DownConnectors.NumOfConnector > 0)
                {
                    nb = GetDirectionNB(slot, Direction.Down);

                    if (nb != null && nb.HasChip())
                    {
                        canWin = slot.Chip.CanConnect(Direction.Down, nb.Chip);

                       if (canWin == false)
                            break;
                    }
                    else
                    {
                        return false;
                    }

                }


                if (slot.Chip.LeftConnectors.NumOfConnector > 0)
                {
                    nb = GetDirectionNB(slot, Direction.Left);
                    if (nb != null && nb.HasChip())
                    {
                        canWin = slot.Chip.CanConnect(Direction.Left, nb.Chip);

                        if (canWin == false)
                            break;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (slot.Chip.RightConnectors.NumOfConnector > 0)
                {         
                    nb = GetDirectionNB(slot, Direction.Right);
                    if (nb != null && nb.HasChip())
                    {
                        canWin = slot.Chip.CanConnect(Direction.Right, nb.Chip);

                        if (canWin == false)
                            break;
                    }
                    else
                    {
                        return false;
                    }
                }
                
               
            }

 
            return canWin;
        }


      

        private GridSlot GetDirectionNB(GridSlot slot, Direction direction)
        {
            GridSlot slotNB;

            int rows = _levelData.Width;
            int columns = _levelData.Height;

            int slotIndex = Array.IndexOf(_gridMap, slot);

            if (slotIndex == -1)
            {
                Debug.LogError("Slot not found in grid.");
                return null;
            }

            int row = slotIndex / rows;
            int column = slotIndex % rows;

            // Check neighboring slots in the four cardinal directions
            switch(direction)
            {
                default:
                    slotNB = null;
                    break;
                case Direction.Up:
                    slotNB = GetNB(row - 1, column); 
                    break;
                case Direction.Down:
                    slotNB = GetNB(row + 1, column); 
                    break;
                case Direction.Left:
                    slotNB = GetNB(row, column - 1); 
                    break;
                case Direction.Right:
                    slotNB = GetNB(row, column + 1); 
                    break;
            }
            
            return slotNB;
        }
   
        private GridSlot GetNB(int row, int column)
        {
            int rows = _levelData.Width;
            int columns = _levelData.Height;

            if (row >= 0 && row < columns && column >= 0 && column < rows)
            {
                int neighbourIndex = row * rows + column;
                GridSlot neighbourSlot = _gridMap[neighbourIndex];
                return neighbourSlot;
            }

            return null;
        }
    }
}