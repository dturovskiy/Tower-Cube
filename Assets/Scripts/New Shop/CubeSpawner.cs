using System.Collections;
using UnityEngine;

namespace TowerCube.Scripts.Utils
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnDelay = 0.3f;

        [SerializeField] private GameObject _cubePrefab;

        [SerializeField] private GameObject _swipeDetectorObject;


        private Coroutine _spawnRoutine;


        private void Start()
        {

        }

        private void OnSwipeEnd(Vector2 delta)
        {
            if (_spawnRoutine == null)
                _spawnRoutine = StartCoroutine(SpawnWithDelay());
        }

        private IEnumerator SpawnWithDelay()
        {
            yield return null;
            yield return new WaitForSeconds(_spawnDelay);
            var instance = Instantiate(_cubePrefab, transform.position, Quaternion.identity);
            _spawnRoutine = null;
        }
    }
}
