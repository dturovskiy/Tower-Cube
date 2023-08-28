// GameController.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject[] startPageUI;

    [SerializeField] private IndicatorManager indicatorManager;

    public UIManager uiManager;

    public CameraManager cameraManager;
    public PrefabCollector collector;
    private bool isGameOver;
    public CubeSpawner cubeSpawner;
    public CubeHandler cubeHandler;
    public CubeListData cubeListData;

    private void Awake()
    {
        cubeListData.InitializeCubeList(collector.cubesToCreate);
        cubeSpawner.InitializeCubeSpawner(collector.cubesToCreate, indicatorManager, cubeHandler, uiManager);
        uiManager.InitializeUI(cubeSpawner);
    }

    private void Start()
    {
        // ϳ��������� �� ���� ���, �� �����������, ���� ���� ������������
        ExplodeCubes.OnGameOver += GameOver;

        StartCoroutine(indicatorManager.ShowCubePlacement());
    }

    private void Update()
    {
        uiManager.UpdateBestScore(PlayerPrefs.GetInt("coins"));
        // ����������, �� ��������� ��� ������ ���� �� �� ��� �� ���������� � ���������� �� �������� �� UI
        if (Input.GetMouseButtonDown(0) && !isGameOver && !IsPointerOverUIObject())
        {
            Debug.Log("GameController: Mouse button clicked. isGameOver: " + isGameOver + ", IsPointerOverUIObject: " + IsPointerOverUIObject());
            // ��������� �������� UI �� ��������� ����� ��� ��� ���������
            uiManager.DestroyUI(startPageUI);
            Debug.Log("Creating new cube...");
            cubeSpawner.CreateNewCube();
            cameraManager.MoveCamera(cubeHandler.possiblePositions, indicatorManager.cubeIndicator.position);
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
        //indicatorManager.DestroyCubeToPlace();

        cameraManager.ShakeCamera();

        if (uiManager.restartButton != null)
        {
            uiManager.ShowRestartButton();
        }
    }
}
