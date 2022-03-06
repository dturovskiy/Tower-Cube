using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Text playerCubes;

    private void Start()
    {
        playerCubes.text = $"Cubes: {PlayerPrefs.GetInt("coins")}";
    }
}
