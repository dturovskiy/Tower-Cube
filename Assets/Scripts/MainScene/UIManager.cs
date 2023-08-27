using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text bestScore; // Текстове поле для відображення найкращого результату
    public Text score; // Текстове поле для відображення поточного результату

    public GameObject restartButton;
    private CubeSpawner _cubeSpawner;

    public void InitializeUI(CubeSpawner cubeSpawner)
    {
        _cubeSpawner = cubeSpawner;
    }

    // Видалення UI елементів зі списку objects
    public void DestroyUI(GameObject[] objects)
    {
        foreach (GameObject objUI in objects)
        {
            Destroy(objUI);
        }
    }

    public void UpdateBestScore(int best)
    {
        bestScore.text = $"<color=#FFE000>Best:{PlayerPrefs.GetInt("coins")}</color>";
    }

    public void UpdateScore(int best)
    {
        if(_cubeSpawner.Score > PlayerPrefs.GetInt("coins"))
        {
            PlayerPrefs.SetInt("coins", _cubeSpawner.Score);
        }
        score.text = $"<color=#E90000>Score:{_cubeSpawner.Score}</color>";
    }

    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }
}
