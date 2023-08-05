using System;
using UnityEngine;

namespace TowerCube.Scripts.Cube
{
    public class PointsContainer : MonoBehaviour
    {
        [SerializeField] protected long _points;

        public long points
        {
            get
            {
                return _points;
            }

            set
            {
                if (_points == value)
                    return;

                _points = value;
                onPointsChanged?.Invoke(_points);
            }
        }

        public event Action<long> onPointsChanged;
    }
}
