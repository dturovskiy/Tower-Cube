using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    public Transform cubeIndicator;    // �������� �� �������, �� ���� ��������� ���
    public CubePosition currentCubePosition = new CubePosition(0, 1, 0);    // ������� ������� ����

    private const float CHANGE_PLACE_SPEED = 0.5f;    // �������� ���� ���� ���������

    public CubeHandler cubeHandler;

    // ��������� ����, ���� ���� ��������
    public void DestroyCubeToPlace()
    {
        if (cubeIndicator != null)
        {
            cubeIndicator.gameObject.SetActive(false);
        }
    }

    // ����� ������� ��� ��������� ����
    public IEnumerator ShowCubePlacement()
    {
        while (cubeIndicator != null)
        {
            ShowPossiblePositions();
            yield return new WaitForSeconds(CHANGE_PLACE_SPEED);
        }
    }

    // ³���������� �������� ������� ��������� ����-����������
    private void ShowPossiblePositions()
    {
        // ��������� ������ ��� ��������� �������� �������
        List<Vector3> positions = new List<Vector3>();

        // ������ ������ �������, ���� ���� ����
        AddPositionIfEmptyAndDifferent(new Vector3(currentCubePosition.x + 1, currentCubePosition.y, currentCubePosition.z), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCubePosition.x - 1, currentCubePosition.y, currentCubePosition.z), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCubePosition.x, currentCubePosition.y + 1, currentCubePosition.z), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCubePosition.x, currentCubePosition.y - 1, currentCubePosition.z), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCubePosition.x, currentCubePosition.y, currentCubePosition.z + 1), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCubePosition.x, currentCubePosition.y, currentCubePosition.z - 1), positions);

        // ����������, �� � ������� ������� ��� ���������
        if (positions.Count > 0)
        {
            // ���� ��������� ������� � ������ �������� ������� ��� ��������� ���������� ����
            int randomIndex = Random.Range(0, positions.Count);

            // ��������� ������� ���������� ���� � CubeHandler ��� ������������
            cubeHandler.cubeIndicator.position = positions[randomIndex];
        }
    }

    // ��������� ������� �� ������, ���� ���� ����� �� �� � �������� ����-����������
    private void AddPositionIfEmptyAndDifferent(Vector3 position, List<Vector3> positions)
    {
        // ����������, �� ������� ����� �� �� �� � �� ������� ����-����������
        if (cubeHandler.IsPositionEmpty(position) && position != cubeHandler.cubeIndicator.position)
        {
            // ������ ������� �� ������ �������� �������
            positions.Add(position);
        }
    }
}
