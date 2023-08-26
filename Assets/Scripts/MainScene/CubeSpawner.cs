using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Transform allCubesOnScene;
    [SerializeField] private List<GameObject> _cubesToCreate;
    [SerializeField] private List<Rigidbody> allCubesRigidbodies = new List<Rigidbody>();
    [SerializeField] private CubeHandler cubeHandler;
    [SerializeField] private CubeManager cubeManager;

    public void InitializeCubeSpawner(List<GameObject> cubesToCreate)
    {
        allCubesRigidbodies.AddRange(FindObjectsOfType<Rigidbody>());
        _cubesToCreate = cubesToCreate;
        Debug.Log(_cubesToCreate.Count);
    }

    // Створення нового куба
    public bool CreateNewCube()
    {
        if (_cubesToCreate.Count != 0)
        {
            // Перевіряємо, чи позиція куба індикатора порожня та не зайнята іншим кубом
            if (cubeHandler.IsPositionEmpty(cubeHandler.cubeIndicator.position))
            {
                GameObject newCube = Instantiate(_cubesToCreate[0], cubeHandler.cubeIndicator.position, Quaternion.identity);   // Створюємо новий куб за обраним префабом і розміщуємо його на позиції куба індикатора
                newCube.transform.SetParent(allCubesOnScene);   // Встановлюємо батьківський об'єкт для нового куба в контейнері всіх кубів на сцені
                cubeManager.currentCubePosition.SetVector(cubeHandler.cubeIndicator.position);  // Оновлюємо поточну позицію куба на позицію куба індикатора
                cubeHandler.possiblePositions.Add(cubeManager.cubeIndicator.position);   // Видаляємо позицію куба індикатора зі списку можливих позицій для розміщення
                SetCubesKinematic(true);    // Встановлюємо кінематичність для всіх кубів на сцені, щоб вони не реагували на фізичні взаємодії
                SetCubesKinematic(false, newCube.GetComponent<Rigidbody>());    // Встановлюємо кінематичність нового куба в значення false, щоб він міг взаємодіяти з іншими об'єктами

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

        foreach (Rigidbody cubeRigidbody in allCubesRigidbodies)
        {
            if (cubeRigidbody != null && cubeRigidbody != excludeRigidbody)
            {
                cubeRigidbody.isKinematic = isKinematicValue;
            }
        }

        Debug.Log("Cubes' kinematic state changed!");
    }
}
