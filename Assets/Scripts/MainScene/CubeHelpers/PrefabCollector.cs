using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class PrefabCollector : MonoBehaviour 
{
    public List<GameObject> cubesToCreate = new List<GameObject>();

    private void Awake()
    {
        string pattern = @"Cube[1-9]$"; // ������ ��� "Cube" � ���� ����� �� 1 �� 9

        GameObject[] allPrefabs = Resources.LoadAll<GameObject>("Prefabs");
        cubesToCreate = allPrefabs
            .Where(prefab => Regex.IsMatch(prefab.name, pattern))
            .ToList();

        if (cubesToCreate.Count > 0)
        {
            //Debug.Log("�������� �������:");
            cubesToCreate.ForEach(prefab => Debug.Log("- " + prefab.name));
        }
        //else
        //{
        //    //Debug.LogWarning("������� �� ��������. �������� ���� �� ����� � ���������.");
        //}
    }
}
