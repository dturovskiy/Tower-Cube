using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCube : MonoBehaviour
{
    public Text titleCube;
    public Text unlockPrice;
    public GameObject selectCube, buyCube;
    public string nowCube;

    private void Start()
    {
        if(PlayerPrefs.GetString("Cube 1") != "open")
        {
            PlayerPrefs.SetString("Cube","open");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        nowCube = other.gameObject.name;
        other.transform.localScale += new Vector3(50f, 50f, 50f);
        titleCube.text = other.name;
        unlockPrice.text = PlayerPrefs.GetString(nowCube);
        if(PlayerPrefs.GetString(other.gameObject.name) == "open")
        {
            selectCube.SetActive(true);
            buyCube.SetActive(false);
        }
        else
        {
            selectCube.SetActive(false);
            buyCube.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.localScale -= new Vector3(50f, 50f, 50f);
    }
}
