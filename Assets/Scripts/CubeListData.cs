using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CubeListData", menuName = "CubeListData")]
public class CubeListData : ScriptableObject
{
    public List<Cube> cubesToCreate = new List<Cube>();

    public void InitializeCubeList(List<Cube> cubes)
    {
        cubesToCreate.Clear();
        
        foreach (Cube cube in cubes)
        {
            Cube newCube = new Cube();
            newCube.cubeName = cube.cubeName;
            newCube.cubeIndex = cube.cubeIndex;
            newCube.cubePrefab = cube.cubePrefab;

            newCube.name = cube.cubePrefab.name;
            cubesToCreate.Add(newCube);
        }
    }
}
