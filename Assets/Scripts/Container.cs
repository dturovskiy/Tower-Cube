using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public GameObject panelPrefab;

    public void AddCubeToPanel(List<Cube> cubeList)
    {
        foreach (Cube cube in cubeList)
        {
            GameObject panel = Instantiate(panelPrefab, transform);

            PanelController panelController = panel.GetComponent<PanelController>();

            panelController.SetCube(cube, cube.cubePrefab);
        }
    }
}
