using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public Transform mainCam;
    public float camMoveToYPosition = 5.9f, camMoveSpeed = 2f;

    public int prevCountMaxHorizontal;

    public void MoveCameraChangeBg(List<Vector3> allCubesPositions, Text scoreTxt, Text highScoreTxt, CubePosition nowCube)
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

    public void ShakeCamera()
    {
        Camera.main.gameObject.AddComponent<CameraShake>();
    }
}
