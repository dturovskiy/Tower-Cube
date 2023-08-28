using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Text cubeNameText;
    public Text cubePriceText;
    public CubeListData cubeListData;
    public GameObject panel;
    public Transform container;
    PanelController panelController;

    void Awake()
    {
        panelController = panel.GetComponent<PanelController>();
        List<Cube> cubeWithPrices = AddPrice(cubeListData.cubesToCreate);

        foreach (Cube cube in cubeWithPrices)
        {
            panel.name = "Panel " + cube.cubeIndex;
            AddPanel(panel.name, cube);
        }

        


    }

    public void AddPanel(string name, Cube cube)
    {


        panel = Instantiate(panel, container.transform);
        
        panelController.InitializePanelController(cubeNameText, cubePriceText);
        panelController.SetCube(cube, cube.cubePrefab);

    }

    public List<Cube> AddPrice(List<Cube> cubeData)
    {
        List<Cube> cubeWithPrices = new List<Cube>();
        int price = 0;

        foreach (Cube cube in cubeData)
        {
            cube.cubePrice = price;
            price += 40;

            cubeWithPrices.Add(cube);
        }

        return cubeWithPrices;
    }
}
