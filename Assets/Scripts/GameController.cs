using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject[] startPageUI;

    public CubeManager cubeManager;
    public UIManager uiManager;
    public CameraManager cameraManager;
    public PrefabCollector prefabCollector;

    private List<GameObject> cubesToCreate;
    private bool isGameOver;

    private void Awake()
    {
        cubesToCreate = prefabCollector.cubesToCreate;
    }

    private void Start()
    {
        uiManager.UpdateBestScore(PlayerPrefs.GetInt("coins"));
        cubeManager.InitializeCubeManager(cubesToCreate);
        StartCoroutine(cubeManager.ShowCubePlacement());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameOver && !IsPointerOverUIObject())
        {
            uiManager.DestroyUI(startPageUI);
            cubeManager.CreateNewCube();
        }
    }

    private static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void OnGameOver()
    {
        isGameOver = true;
        cubeManager.DestroyCubeToPlace();
    }
}
