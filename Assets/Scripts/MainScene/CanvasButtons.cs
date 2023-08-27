using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CanvasButtons : MonoBehaviour
{
    public Sprite sound_on, sound_off;

    private void Start()
    {
        if (PlayerPrefs.GetString("sound") == "Off" && gameObject.name == "Sound")
        {
            GetComponent<Image>().sprite = sound_off;
        }
    }
    public void RestartGame()
    {
        if (PlayerPrefs.GetString("sound") != "Off")
        {
           // GetComponent<AudioSource>().Play();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadShop()
    {
        Debug.Log("Shop");
        if (PlayerPrefs.GetString("sound") != "Off")
        {
            // GetComponent<AudioSource>().Play();
        }
        SceneManager.LoadScene("ShopScene");
    }

    public void CloseShop()
    {
        if (PlayerPrefs.GetString("sound") != "Off")
        {
            // GetComponent<AudioSource>().Play();
        }
        SceneManager.LoadScene("MainScene");
    }

    public void LoadInstagram()
    {
        Debug.Log("Instagram");
        if (PlayerPrefs.GetString("sound") != "Off")
        {
           // GetComponent<AudioSource>().Play();
        }
        Application.OpenURL("https://www.instagram.com/denysprog/");
    }

    public void SoundWork()
    {
        Debug.Log("Sound");
        if(PlayerPrefs.GetString("sound") == "Off")
        {
            //GetComponent<AudioSource>().Play();
            PlayerPrefs.SetString("sound", "On");
            GetComponent<Image>().sprite = sound_on;
        }
        else
        {
            PlayerPrefs.SetString("sound", "Off");
            GetComponent<Image>().sprite = sound_off;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
