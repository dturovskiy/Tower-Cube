using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] canvasStartPage;

    private CubePos nowCube = new CubePos(0, 1, 0);
    public float cubeChangePlaceSpeed = 0.5f;
    public Transform cubeToPlace;
    private Rigidbody allCubesRB;
    private float camMoveToYPosition = 5.9f, camMoveSpeed = 2f;

    public Text highScoreTxt;
    public Text scoreTxt;

    public GameObject[] cubesToCreate;
    public GameObject allCubes, cubeToCreate;
    private bool isLose;

    public int prevCountMaxHorizontal;
    private List<Vector3> allCubesPositions = new List<Vector3>
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

    private Transform mainCam;

    private static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void Start()
    {
        highScoreTxt.text = $"<color=#FFE000>Best:{PlayerPrefs.GetInt("coins")}</color>";
        mainCam = Camera.main.transform;
        allCubesRB = allCubes.GetComponent<Rigidbody>();
        StartCoroutine(ShowCubePlace());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cubeToPlace != null && allCubes != null && !IsPointerOverUIObject())
        {
            DestroyUI();
            Debug.Log("Touch screen");
            GameObject newCube = Instantiate(cubesToCreate[IsEnabled.cubeIndex], cubeToPlace.position, Quaternion.identity) as GameObject;

            newCube.transform.SetParent(allCubes.transform);
            nowCube.setVector(cubeToPlace.position);
            allCubesPositions.Add(nowCube.getVector());

            allCubesRB.isKinematic = true;
            allCubesRB.isKinematic = false;

            SpawnPosition();
            MoveCAmeraChangeBg();
        }

        if (!isLose && allCubesRB.velocity.magnitude > 0.2f)
        {
            Destroy(cubeToPlace.gameObject);
            isLose = true;
        }

        mainCam.localPosition = Vector3.MoveTowards(mainCam.localPosition, new Vector3(mainCam.localPosition.x, camMoveToYPosition, mainCam.localPosition.z), camMoveSpeed * Time.deltaTime);
    }
    IEnumerator ShowCubePlace()
    {
        while (cubeToPlace != null)
        {
            SpawnPosition();
            yield return new WaitForSeconds(cubeChangePlaceSpeed);
        }
    }

    public void DestroyUI()
    {
        foreach (GameObject objUI in canvasStartPage)
        {
            Destroy(objUI);
        }
    }

    private void SpawnPosition()
    {
        List<Vector3> positions = new List<Vector3>();

        if (IsPositionEmpty(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z)) && nowCube.x + 1 != cubeToPlace.position.x)
        {
            positions.Add(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z));
        }
        if (IsPositionEmpty(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z)) && nowCube.x - 1 != cubeToPlace.position.x)
        {
            positions.Add(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z));
        }
        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z)) && nowCube.y + 1 != cubeToPlace.position.y)
        {
            positions.Add(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z));
        }
        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z)) && nowCube.y - 1 != cubeToPlace.position.y)
        {
            positions.Add(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z));
        }
        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1)) && nowCube.z + 1 != cubeToPlace.position.z)
        {
            positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1));
        }
        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1)) && nowCube.z - 1 != cubeToPlace.position.z)
        {
            positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1));
        }

        if (positions.Count > 1)
        {
            cubeToPlace.position = positions[UnityEngine.Random.Range(0, positions.Count)];
        }
        else if (positions.Count == 0)
        {
            isLose = true;
        }
        else
        {
            cubeToPlace.position = positions[0];
        }
    }

    private bool IsPositionEmpty(Vector3 targetPos)
    {
        if (targetPos.y == 0)
        {
            return false;
        }
        foreach (Vector3 pos in allCubesPositions)
        {
            if (pos.x == targetPos.x && pos.y == targetPos.y && pos.z == targetPos.z)
            {
                return false;
            }
        }
        return true;

    }


    private void MoveCAmeraChangeBg()
    {
        int maxX = 0, maxY = 0, maxZ = 0, maxHorizontal;

        foreach (Vector3 pos in allCubesPositions)
        {
            if (Mathf.Abs((int)pos.x) > maxX)
            {
                maxX = (int)pos.x;
            }
            if ((int)pos.y > maxY)
            {
                maxY = (int)pos.y;
            }
            if (Mathf.Abs((int)pos.z) > maxZ)
            {
                maxZ = (int)pos.z;
            }
        }

        if (PlayerPrefs.GetInt("coins") < maxY - 1)
        {
            PlayerPrefs.SetInt("coins", maxY - 1);
        }


        scoreTxt.text = $"<color=#E90000>Score:{maxY - 1}</color>";
        highScoreTxt.text = $"<color=#FFE000>Best:{PlayerPrefs.GetInt("coins")}</color>";

        camMoveToYPosition = 5.9f + nowCube.y - 1f;

        maxHorizontal = maxX > maxZ ? maxX : maxZ;
        if (maxHorizontal % 2 == 0 && prevCountMaxHorizontal != maxHorizontal)
        {
            mainCam.localPosition += new Vector3(0, 0, -2.5f);
            prevCountMaxHorizontal = maxHorizontal;
        }
    }
}