using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject cubeToPlacePrefab;
    public Transform allCubes;
    public Transform cubeToPlace;

    public CubePosition currentCube = new CubePosition(0, 1, 0);
    private bool isLose;
    public List<GameObject> cubesToCreate;
    public float cubeChangePlaceSpeed = 0.5f;
    public Rigidbody allCubesRigidbody;


    public List<Vector3> allCubesPositions = new List<Vector3>
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

    private void Awake()
    {
        allCubesRigidbody = allCubes.GetComponent<Rigidbody>();

    }

    public void InitializeCubeManager(List<GameObject> cubes)
    {
        cubesToCreate = cubes;
    }

    public void CreateNewCube()
    {
        if (cubeToPlace != null && cubesToCreate != null && cubesToCreate.Count > 0)
        {
            GameObject newCube = Instantiate(cubesToCreate[IsEnabled.cubeIndex], cubeToPlace.position, Quaternion.identity);
            newCube.transform.SetParent(allCubes);
            currentCube.setVector(cubeToPlace.position);
            allCubesPositions.Add(currentCube.getVector());

            allCubesRigidbody.isKinematic = true;
            allCubesRigidbody.isKinematic = false;
            SpawnPosition();
        }
    }

    public void DestroyCubeToPlace()
    {
        if (cubeToPlace != null)
        {
            Destroy(cubeToPlace.gameObject);
        }
    }

    public IEnumerator ShowCubePlacement()
    {
        while (cubeToPlace != null)
        {
            SpawnPosition();
            yield return new WaitForSeconds(cubeChangePlaceSpeed);
        }
    }

    private void SpawnPosition()
    {
        List<Vector3> positions = new List<Vector3>();

        AddPositionIfEmptyAndDifferent(new Vector3(currentCube.x + 1, currentCube.y, currentCube.z), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCube.x - 1, currentCube.y, currentCube.z), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCube.x, currentCube.y + 1, currentCube.z), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCube.x, currentCube.y - 1, currentCube.z), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCube.x, currentCube.y, currentCube.z + 1), positions);
        AddPositionIfEmptyAndDifferent(new Vector3(currentCube.x, currentCube.y, currentCube.z - 1), positions);

        if (positions.Count > 0)
        {
            int randomIndex = Random.Range(0, positions.Count);
            cubeToPlace.position = positions[randomIndex];
        }
        else
        {
            isLose = true;
        }
    }

    private void AddPositionIfEmptyAndDifferent(Vector3 position, List<Vector3> positions)
    {
        if (IsPositionEmpty(position) && position != cubeToPlace.position)
        {
            positions.Add(position);
        }

    }

    private bool IsPositionEmpty(Vector3 targetPos)
    {
        if (targetPos.y == 0)
        {
            return false;
        }

        // ѕерев≥р€Їмо, чи Ї в списку allCubesPositions елемент, €кий маЇ т≥ сам≥ (x, y, z)-координати, що й у targetPos.
        // якщо такий елемент ≥снуЇ, метод FindIndex поверне його ≥ндекс, в ≥ншому випадку поверне -1.
        int index = allCubesPositions.FindIndex(pos => pos.x == targetPos.x && pos.y == targetPos.y && pos.z == targetPos.z);

        // якщо index б≥льше або дор≥внюЇ 0, то позиц≥€ зайн€та, повертаЇмо false, ≥накше - true.
        return index < 0;
    }
}