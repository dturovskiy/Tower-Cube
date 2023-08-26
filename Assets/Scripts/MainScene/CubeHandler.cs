using System.Collections.Generic;
using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    public Transform cubeIndicator;
    internal List<Vector3> possiblePositions = new List<Vector3>
    {
        new Vector3(0, 0, 0),
        new Vector3(1, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3(0, 1, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 0, -1),
        new Vector3(1, 0, 1),
        new Vector3(-1, 0, -1),
        new Vector3(-1, 0, 1),
        new Vector3(1, 0, -1),
    };



    // Перевірка, чи позиція вільна (не зайнята іншим кубом)
    public bool IsPositionEmpty(Vector3 targetPos)
    {
        // Позиція не вільна, якщо y-координата дорівнює 0
        if (targetPos.y == 0)
        {
            return false;
        }

        // Пошук позиції в списку можливих позицій
        int index = possiblePositions.FindIndex(pos => pos.x == targetPos.x && pos.y == targetPos.y && pos.z == targetPos.z);
        return index < 0;
    }
}
