// GameController.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject[] startPageUI;

    [SerializeField] private CubeManager cubeManager;

    public UIManager uiManager;
    public CameraManager cameraManager;
    public PrefabCollector collector;
    private bool isGameOver;
    public CubeSpawner cubeSpawner;

    private void Awake()
    {
        cubeSpawner.InitializeCubeSpawner(collector.cubesToCreate);
    }

    private void Start()
    {
        // ��������� ��������� ��������� ���������� � PlayerPrefs
        uiManager.UpdateBestScore(PlayerPrefs.GetInt("coins"));

        

        // ϳ��������� �� ���� ���, �� �����������, ���� ���� ������������
        ExplodeCubes.OnGameOver += GameOver;

        StartCoroutine(cubeManager.ShowCubePlacement());
    }

    private void Update()
    {
        // ����������, �� ��������� ��� ������ ���� �� �� ��� �� ���������� � ���������� �� �������� �� UI
        if (Input.GetMouseButtonDown(0) && !isGameOver && !IsPointerOverUIObject())
        {
            Debug.Log("GameController: Mouse button clicked. isGameOver: " + isGameOver + ", IsPointerOverUIObject: " + IsPointerOverUIObject());
            // ��������� �������� UI �� ��������� ����� ��� ��� ���������
            uiManager.DestroyUI(startPageUI);
            Debug.Log("Creating new cube...");
            cubeSpawner.CreateNewCube();
        }
    }

    private static bool IsPointerOverUIObject()
    {
        // ����������, �� ������ ���� ����������� ��� ��'����� UI
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void GameOver()
    {
        // �����������, ���� ���� ������������
        isGameOver = true;
        cubeManager.DestroyCubeToPlace();

        cameraManager.ShakeCamera();

        if (uiManager.restartButton != null)
        {
            uiManager.ShowRestartButton();
        }
    }
}
