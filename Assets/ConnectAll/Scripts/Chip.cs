using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

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
            Null, Red, Green
        }

        public ChipType Type;

        private Connector _up;
        private Connector _down;
        private Connector _left;
        private Connector _right;

        [Header("Prefabs")]
        [SerializeField] private Transform _connectorRoot;
        [SerializeField] private GameObject _vConnectorPrefab;
        [SerializeField] private GameObject _hConnectorPrefab;

        [Header("Spawn Properties")]
        [SerializeField] private float _connectorOffset;


        #region Properties
        public Connector UpConnectors { get => _up; }
        public Connector DownConnectors { get => _down; }
        public Connector LeftConnectors { get => _left; }
        public Connector RightConnectors { get => _right; }
        #endregion

        public void LoadChipData(ChipData chipData)
        {
            _up = chipData.Up;
            _down = chipData.Down;
            _left = chipData.Left;
            _right = chipData.Right;

            GenerateConnectors(_up);
            GenerateConnectors(_down);
            GenerateConnectors(_left);
            GenerateConnectors(_right);
        }


        private void GenerateConnectors(Connector connector)
        {
            var points = GeneratePoints(GetConnectorCenterPoint(connector.Direction), connector.Direction, connector.NumOfConnector);

            for (int i = 0; i < points.Count; i++)
            {
                Instantiate(GetConnectorPrefab(connector.Direction), points[i], Quaternion.identity, _connectorRoot);
            }
        }


        private GameObject GetConnectorPrefab(Direction direction)
        {
            switch (direction)
            {
                default:
                case Direction.Up:
                case Direction.Down:
                    return _vConnectorPrefab;
                case Direction.Left:
                case Direction.Right:
                    return _hConnectorPrefab;
            }
        }


        public bool CanConnect(Direction direction, Chip chip)
        {
            bool canConnect = true;

            switch(direction)
            {
                default:
                    canConnect = false;
                    break;
                case Direction.Down:
                    if (_down.NumOfConnector > 0)
                    {
                        //Debug.Log("Check down");
                        canConnect = _down.NumOfConnector == chip.UpConnectors.NumOfConnector;
                        if (canConnect == false)
                            return false;
                    }
                    else
                    {
                        canConnect = false;
                        break;
                    }
                    break;
                case Direction.Up:
                    if (_up.NumOfConnector > 0)
                    {
                        //Debug.Log("Check up");

                        canConnect = _up.NumOfConnector == chip.DownConnectors.NumOfConnector;
                        if (canConnect == false)
                            return false;
                    }
                    else
                    {
                        canConnect = false;
                        break;
                    }
                    break;
                case Direction.Left:
                    if (_left.NumOfConnector > 0)
                    {
                        //Debug.Log("Check left");

                        canConnect = _left.NumOfConnector == chip.RightConnectors.NumOfConnector;
                        if (canConnect == false)
                            return false;
                    }
                    else
                    {
                        canConnect = false;
                        break;

                    }
                    break;
                case Direction.Right:
                    if (_right.NumOfConnector > 0)
                    {
                        //Debug.Log("Check right");

                        canConnect = _right.NumOfConnector == chip.LeftConnectors.NumOfConnector;
                        if (canConnect == false)
                            return false;
                    }
                    else
                    {
                        canConnect = false;
                        break;
                    }
                    break;
            }

          
            return canConnect;
        }

        private Vector2 GetConnectorCenterPoint(Direction direction)
        {
            Vector2 offsetVector;
            switch (direction)
            {
                default:
                case Direction.Up:
                    offsetVector = new Vector2(0, _connectorOffset);
                    break;
                case Direction.Down:
                    offsetVector = new Vector2(0, -_connectorOffset);
                    break;
                case Direction.Left:
                    offsetVector = new Vector2(-_connectorOffset, 0);
                    break;
                case Direction.Right:
                    offsetVector = new Vector2(_connectorOffset, 0);
                    break;
            }

            return (Vector2)transform.position + offsetVector;
        }
        private List<Vector2> GeneratePoints(Vector2 centerPoint, Direction direction, int numberOfPoints, Vector2 vectorOffset = default(Vector2), float spacing = 0.15f)
        {
            Vector2 dirVector;
            List<Vector2> points = new List<Vector2>();
            switch (direction)
            {
                default:
                case Direction.Up:
                    dirVector = Vector2.right;
                    break;
                case Direction.Down:
                    dirVector = Vector2.right;
                    break;
                case Direction.Left:
                    dirVector = Vector2.up;
                    break;
                case Direction.Right:
                    dirVector = Vector2.up;
                    break;
            }

            if (numberOfPoints % 2 == 0)
            {
                // Generate an even number of points
                for (int i = 0; i < numberOfPoints; i++)
                {
                    float offset = (i - numberOfPoints / 2 + 0.5f) * spacing;
                    Vector2 newPoint = centerPoint + dirVector * offset;
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
                    Vector2 leftPoint = centerPoint - dirVector * offset;
                    Vector2 rightPoint = centerPoint + dirVector * offset;

                    points.Add(leftPoint);
                    points.Add(rightPoint);
                }
            }
            return points;
        }
    }
}