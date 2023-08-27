using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CubeData", menuName = "Cubes")]
public class Cube : ScriptableObject
{
    public string cubeName;
    public int cubeIndex;
    public GameObject cubePrefab;

    public string indexedCubeName => $"{cubeName} {cubeIndex}";
}
