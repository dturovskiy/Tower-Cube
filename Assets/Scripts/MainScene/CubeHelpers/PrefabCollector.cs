using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class PrefabCollector : MonoBehaviour 
{
    public List<Cube> cubesToCreate = new List<Cube>();

    private void Awake()
    {
        string pattern = @"Cube(\d+)$"; // ������ ��� "Cube" � ���� ����� �� 1 �� 9

        GameObject[] allPrefabs = Resources.LoadAll<GameObject>("Prefabs");
        cubesToCreate = allPrefabs
            .Where(prefab => Regex.IsMatch(prefab.name, pattern))
            .Select(prefab =>
            {
                var cube = ScriptableObject.CreateInstance<Cube>();
                cube.cubePrefab = prefab;
                cube.cubeName = prefab.name;
                cube.cubeIndex = GetCubeIndexFromName(prefab.name);

                cube.name = prefab.name;

                return cube;
            })
            .ToList();

        if (cubesToCreate.Count > 0)
        {
            cubesToCreate.Sort((cube1, cube2) => cube1.cubeIndex.CompareTo(cube2.cubeIndex)); // ���������� �� �������� ��������
            cubesToCreate.ForEach(prefab => Debug.Log("- " + prefab.cubeName));
        }
    }

    private int GetCubeIndexFromName(string cubeName)
    {
        // �������������� ���������� ����� ��� ��������� ����� � �����
        Match match = Regex.Match(cubeName, @"Cube(\d+)$");

        if (match.Success)
        {
            string indexString = match.Groups[1].Value;
            int index = int.Parse(indexString);
            return index;
        }
        else
        {
            Debug.LogWarning("Unable to extract cube index from name: " + cubeName);
            return -1; // ���� �� ������� ��������� ������
        }
    }
}
