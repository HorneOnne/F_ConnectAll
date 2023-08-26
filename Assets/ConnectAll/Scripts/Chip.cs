using UnityEngine;
using System.Collections.Generic;

namespace ConnectAll
{

    [System.Serializable]
    public class Connector
    {
        public Direction Direction;
        public int NumOfConnector;
    }

    public class Chip : MonoBehaviour
    {
        public enum ChipType
        {
            Red, Green
        }

        public ChipType Type;

        [SerializeField] private Connector _top;
        [SerializeField] private Connector _down;
        [SerializeField] private Connector _left;
        [SerializeField] private Connector _right;

        [Header("Prefabs")]
        [SerializeField] private Transform _connectorRoot;
        [SerializeField] private GameObject _vConnectorPrefab;
        [SerializeField] private GameObject _hConnectorPrefab;

        [Header("Spawn Properties")]
        [SerializeField] private float _connectorOffset;


        private void Start()
        {
            //Instantiate(_vConnectorPrefab, transform.position + new Vector3(0, _connectorOffset, 0), Quaternion.identity);
            centerPoint = transform.position + new Vector3(0, _connectorOffset, 0);
            GeneratePoints();
        }



        public int numberOfPoints = 2;
        public float spacing = 0.1f;
        public Vector2 centerPoint;
        public List<Vector2> points = new List<Vector2>();
        private void GeneratePoints()
        {
          
            if (numberOfPoints % 2 == 0)
            {
                // Generate an even number of points
                for (int i = 0; i < numberOfPoints; i++)
                {
                    float offset = (i - numberOfPoints / 2 + 0.5f) * spacing;
                    Vector2 newPoint = centerPoint + Vector2.up * offset;
                    points.Add(newPoint);
                }
            }
            else
            {
                // Generate an odd number of points
                int middleIndex = numberOfPoints / 2;
                points.Add(centerPoint);

                for (int i = 1; i <= middleIndex; i++)
                {
                    float offset = i * spacing;
                    Vector2 leftPoint = centerPoint - Vector2.up * offset;
                    Vector2 rightPoint = centerPoint + Vector2.up * offset;

                    points.Add(leftPoint);
                    points.Add(rightPoint);
                }
            }
        }
    }
}