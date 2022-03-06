using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCube : MonoBehaviour
{
    public Text titleCube;
    public Text locked;
    void OnTriggerEnter(Collider other)
    {
        other.transform.localScale += new Vector3(50f, 50f, 50f);
        titleCube.text = other.name;
        locked.text = $"Need to unlock: {PlayerPrefs.GetInt("needToUnlock")}";
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.localScale -= new Vector3(50f, 50f, 50f);
    }
}
