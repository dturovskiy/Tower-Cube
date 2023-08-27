using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    public Transform highestCube;  // Найвищий куб на сцені

    public float camMoveSpeed = 2f;
    public float maxCameraDistance = 10f; // Максимальна відстань камери

    private float initialCamMoveSpeed;
    private float initialMaxCameraDistance;

    private int prevCountMaxHorizontal;
    private float camMoveToYPosition = 5.9f;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
        initialCamMoveSpeed = camMoveSpeed;
        initialMaxCameraDistance = maxCameraDistance;
    }

    public void MoveCamera(List<Vector3> allCubesPositions, Vector3 currentCube)
    {
        (int maxX, int maxY, int maxZ) = CalculateMaxValues(allCubesPositions);
        camMoveToYPosition = 5.9f + currentCube.y - 1f;

        int maxHorizontal = Mathf.Max(maxX, maxZ);
        AdjustCameraPosition(maxHorizontal);

        float distanceToHighestCube = highestCube.position.y - mainCamera.position.y;
        AdjustCameraSpeed(distanceToHighestCube);

        AdjustCameraDistance(distanceToHighestCube);
    }

    private (int, int, int) CalculateMaxValues(List<Vector3> positions)
    {
        int maxX = 0, maxY = 0, maxZ = 0;

        foreach (Vector3 pos in positions)
        {
            maxX = Mathf.Max(maxX, Mathf.Abs((int)pos.x));
            maxY = Mathf.Max(maxY, (int)pos.y);
            maxZ = Mathf.Max(maxZ, Mathf.Abs((int)pos.z));
        }

        return (maxX, maxY, maxZ);
    }

    private void AdjustCameraPosition(int maxHorizontal)
    {
        if (maxHorizontal % 2 == 0 && prevCountMaxHorizontal != maxHorizontal)
        {
            mainCamera.localPosition += new Vector3(0, 0, -2.5f);
            prevCountMaxHorizontal = maxHorizontal;
        }
    }

    private void AdjustCameraSpeed(float distanceToHighestCube)
    {
        camMoveSpeed = Mathf.Lerp(initialCamMoveSpeed, 0f, Mathf.Clamp01(distanceToHighestCube / maxCameraDistance));
    }

    private void AdjustCameraDistance(float distanceToHighestCube)
    {
        maxCameraDistance = Mathf.Lerp(initialMaxCameraDistance, 0f, Mathf.Clamp01(distanceToHighestCube / initialMaxCameraDistance));
    }

    private void Update()
    {
        Vector3 targetPosition = new Vector3(mainCamera.localPosition.x, camMoveToYPosition, mainCamera.localPosition.z);
        mainCamera.localPosition = Vector3.MoveTowards(mainCamera.localPosition, targetPosition, camMoveSpeed * Time.deltaTime);
        maxCameraDistance = Mathf.Lerp(maxCameraDistance, initialMaxCameraDistance, Time.deltaTime * camMoveSpeed);
    }

    public void ShakeCamera()
    {
        Camera.main.gameObject.AddComponent<CameraShake>();
    }
}
