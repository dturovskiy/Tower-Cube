using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IsEnabled : MonoBehaviour
{
    public int needToUnlock;
    public Material blackMaterial;

    public static int cubeIndex;

    public void Start()
    {
        PlayerPrefs.SetInt("needToUnlock", needToUnlock);
        if (needToUnlock == 0)
        {
            PlayerPrefs.SetString("Cube", "open");
        }
        if (PlayerPrefs.GetInt("coins") < needToUnlock)
        {
            GetComponent<MeshRenderer>().material = blackMaterial;
        }
    }
}
