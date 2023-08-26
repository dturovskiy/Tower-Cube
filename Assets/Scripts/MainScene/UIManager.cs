using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text bestScoreText; // Текстове поле для відображення найкращого результату
    public Text scoreText; // Текстове поле для відображення поточного результату

    public GameObject restartButton;

    // Оновлення найкращого результату
    public void UpdateBestScore(int bestScore)
    {
        bestScoreText.text = $"<color=#FFE000>Best: {bestScore}</color>";
    }

    // Видалення UI елементів зі списку objects
    public void DestroyUI(GameObject[] objects)
    {
        foreach (GameObject objUI in objects)
        {
            Destroy(objUI);
        }
    }

    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }
}
