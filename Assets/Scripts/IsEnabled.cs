using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IsEnabled : MonoBehaviour
{
    public int needToUnlock;
    public Material blackMaterial;
    
    public GameObject[] cubes;
    public static int cubeIndex;
    public Transform activePanels;

    public void Start()
    {
        PlayerPrefs.SetInt("needToUnlock", needToUnlock);


        if (PlayerPrefs.GetInt("coins") < needToUnlock)
        {
            GetComponent<MeshRenderer>().material = blackMaterial;
        }
    }
}
