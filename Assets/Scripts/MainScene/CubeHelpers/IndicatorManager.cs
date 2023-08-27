using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    public Transform cubeIndicator;    // Вказівник на позицію, де буде розміщений куб
    public CubePosition currentCubePosition = new CubePosition(0, 1, 0);    // Поточна позиція куба

    private const float CHANGE_PLACE_SPEED = 0.5f;    // Швидкість зміни місця розміщення

    public CubeHandler cubeHandler;

    // Видалення куба, який буде розміщено
    public void DestroyCubeToPlace()
    {
        if (cubeIndicator != null)
        {
            cubeIndicator.gameObject.SetActive(false);
        }
    }

    // Показ позицій для розміщення куба
    public IEnumerator ShowCubePlacement()
    {
        while (cubeIndicator != null)
        {
            ShowPossiblePositions();
            yield return new WaitForSeconds(CHANGE_PLACE_SPEED);
        }
    }

    // Відображення можливих позицій розміщення куба-індикатора
    private void ShowPossiblePositions()
    {
        // Створюємо список для зберігання можливих позицій
        List<Vector3> positions = new List<Vector3>();

        // Додаємо можливі позиції, якщо вони вільні
        AddPositionIfEmptyAndDifferent(new Vector3(currentCubePosition.x + 1, currentCubePosition.y, currentCubePosition.z), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCubePosition.x - 1, currentCubePosition.y, currentCubePosition.z), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCubePosition.x, currentCubePosition.y + 1, currentCubePosition.z), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCubePosition.x, currentCubePosition.y - 1, currentCubePosition.z), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCubePosition.x, currentCubePosition.y, currentCubePosition.z + 1), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCubePosition.x, currentCubePosition.y, currentCubePosition.z - 1), positions);

        // Перевіряємо, чи є доступні позиції для розміщення
        if (positions.Count > 0)
        {
            // Вибір випадкової позиції зі списку можливих позицій для розміщення індикатора куба
            int randomIndex = Random.Range(0, positions.Count);

            // Оновлення позиції індикатора куба в CubeHandler для синхронізації
            cubeHandler.cubeIndicator.position = positions[randomIndex];
        }
    }

    // Додавання позиції до списку, якщо вона вільна та не є позицією куба-індикатора
    private void AddPositionIfEmptyAndDifferent(Vector3 position, List<Vector3> positions)
    {
        // Перевіряємо, чи позиція вільна та чи не є це позиція куба-індикатора
        if (cubeHandler.IsPositionEmpty(position) && position != cubeHandler.cubeIndicator.position)
        {
            // Додаємо позицію до списку можливих позицій
            positions.Add(position);
        }
    }
}
