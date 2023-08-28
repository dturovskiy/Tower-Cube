using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Text cubeNameText;
    public Text cubePriceText;

    public CubeListData cubeListData;
    public Container cubeContainer;
    public GameObject panel;

    void Awake()
    {
        PanelController panelController = panel.GetComponent<PanelController>();

        panelController.InitializePanelController(cubeNameText, cubePriceText);
    }

    private void Start()
    {


        List<Cube> cubeWithPrices = cubeListData.cubesToCreate;

        int price = 0;
        foreach (Cube cube in cubeWithPrices)
        {
            cube.cubePrice = price;
            price += 40;
        }

        cubeContainer.AddCubeToPanel(cubeWithPrices);
    }
}
