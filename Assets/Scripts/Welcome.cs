using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Debug.Log("quit game");
        Application.Quit();
    }
}
