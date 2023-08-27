using System.Collections.Generic;
using UnityEngine;


namespace ConnectAll
{
    [CreateAssetMenu(fileName = "LevelData_", menuName = "ConnectAll/LevelData", order = 51)]
    public class LevelData : ScriptableObject
    {
        [Header("Level")]
        public int Level;
        public bool IsLocking;

        [Header("Camera zoom")]
        public float OrthographicCameraSize = 5;
        public Vector2 CameraOffset;

        [Header("Grid size")]
        public int Width;
        public int Height;
        public List<ChipData> ChipData;
    }


    [System.Serializable]
    public class ChipData
    {
        public Chip.ChipType ChipType;
        public Connector Up;
        public Connector Down;
        public Connector Left;
        public Connector Right;
    }
}