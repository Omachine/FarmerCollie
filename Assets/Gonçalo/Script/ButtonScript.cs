using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public void StartScene(){
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void CreditsScene(){
        SceneManager.LoadScene("CreditsScene");
    }
    public void MenuScene(){
        SceneManager.LoadScene("MenuInicial");
    }
}