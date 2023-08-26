using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text bestScoreText; // �������� ���� ��� ����������� ���������� ����������
    public Text scoreText; // �������� ���� ��� ����������� ��������� ����������

    public GameObject restartButton;

    // ��������� ���������� ����������
    public void UpdateBestScore(int bestScore)
    {
        bestScoreText.text = $"<color=#FFE000>Best: {bestScore}</color>";
    }

    // ��������� UI �������� � ������ objects
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
