using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public Text cubeNameText;
    public Text cubePriceText;

    public void InitializePanelController(Text cubeName, Text cubePrice)
    {
        cubeNameText = cubeName;
        cubePriceText = cubePrice;
    }

    public void SetCube(Cube cube, GameObject cubePrefab)
    {
        cubeNameText.text = cube.cubeName + "Scotobaza";
        cubePriceText.text = "Price: " + cube.cubePrice.ToString();
        // Встановіть зображення куба на cubeImage, якщо ви плануєте використовувати зображення для панелі.

        GameObject cubeInPanel = Instantiate(cubePrefab);
        //cubeInPanel.transform.localScale = new Vector3(500f, 500f, 500f);
        cubeInPanel.transform.localRotation = Quaternion.Euler(45f, 0, 45f);
        cubeInPanel.transform.SetParent(transform);
    }
}
