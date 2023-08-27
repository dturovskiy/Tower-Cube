using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Transform _allCubesOnScene;
    [SerializeField] private List<GameObject> _cubesToCreate;
    [SerializeField] private List<Rigidbody> _allCubesRigidbodies = new List<Rigidbody>();
    [SerializeField] private CubeHandler _cubeHandler;
    [SerializeField] private CubeManager _cubeManager;
    [SerializeField] private UIManager _uiManager;

    public int Score { get; private set; } = 0;

    public void InitializeCubeSpawner(List<GameObject> cubesToCreate, CubeManager cubeManager, CubeHandler cubeHandler, UIManager uIManager)
    {
        _cubeManager = cubeManager;
        _cubeHandler = cubeHandler;
        _uiManager = uIManager;
        _allCubesRigidbodies.AddRange(FindObjectsOfType<Rigidbody>());
        _cubesToCreate = cubesToCreate;
        Debug.Log(_cubesToCreate.Count);
    }

    // Створення нового куба
    public bool CreateNewCube()
    {
        if (_cubesToCreate.Count != 0)
        {
            // Перевіряємо, чи позиція куба індикатора порожня та не зайнята іншим кубом
            if (_cubeHandler.IsPositionEmpty(_cubeHandler.cubeIndicator.position))
            {
                GameObject newCube = Instantiate(_cubesToCreate[0], _cubeHandler.cubeIndicator.position, Quaternion.identity);   // Створюємо новий куб за обраним префабом і розміщуємо його на позиції куба індикатора
                newCube.transform.SetParent(_allCubesOnScene);   // Встановлюємо батьківський об'єкт для нового куба в контейнері всіх кубів на сцені
                _cubeManager.currentCubePosition.SetVector(_cubeHandler.cubeIndicator.position);  // Оновлюємо поточну позицію куба на позицію куба індикатора
                _cubeHandler.possiblePositions.Add(_cubeManager.cubeIndicator.position);   // Видаляємо позицію куба індикатора зі списку можливих позицій для розміщення
                
                SetCubesKinematic(true);    // Встановлюємо кінематичність для всіх кубів на сцені, щоб вони не реагували на фізичні взаємодії
                SetCubesKinematic(false);    // Встановлюємо кінематичність нового куба в значення false, щоб він міг взаємодіяти з іншими об'єктами

                Score++;
                _uiManager.UpdateScore(Score);

                return true;    // Повертаємо значення true, означаючи, що куб було успішно створено
            }
            else
            {
                // Виводимо повідомлення про помилку у випадку, якщо позиція для розміщення куба зайнята або не порожня
                Debug.Log("Cube cannot be created. Position is not empty or position is occupied by another cube.");
                return false;
            }
        }
        // Повертаємо значення false, означаючи, що створення куба неможливе
        return false;
    }

    // Встановлення кінематичності всіх кубів
    private void SetCubesKinematic(bool isKinematicValue, Rigidbody excludeRigidbody = null)
    {
        Debug.Log("SetCubesKinematic method called!");

        foreach (Rigidbody cubeRigidbody in _allCubesRigidbodies)
        {
            if (cubeRigidbody != null && cubeRigidbody != excludeRigidbody)
            {
                cubeRigidbody.isKinematic = isKinematicValue;
            }
        }

        Debug.Log("Cubes' kinematic state changed!");
    }
}
