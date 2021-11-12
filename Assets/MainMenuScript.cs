using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayTheGame(){
        SceneManager.LoadScene("MainScene");
    }
    public void QuitTheGame(){
        Debug.Log("Quit");
        Application.Quit();
    }
}
