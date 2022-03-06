using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSkinCube : MonoBehaviour
{
    public static int selectedSkin;
    public int skinIndex;
    public Button buyButton;
    public Material iLock;
    public int price;

    public GameObject[] skins;
    public void Start()
    {
        if(PlayerPrefs.GetInt("cube1" + "buy") == 0)
        {
            foreach(GameObject cube in skins)
            {
                if("cube1" == cube.name)
                {
                    PlayerPrefs.SetInt("cube1" + "buy", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(GetComponent<Material>().name + "buy", 0);
                }
            }
        }
    }

    public void Update()
    {

    }
}
