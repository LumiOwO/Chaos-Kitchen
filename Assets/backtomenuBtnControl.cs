using UnityEngine;
using UnityEngine.SceneManagement;

public class backtomenuBtnControl : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("Welcome");
    }
}
