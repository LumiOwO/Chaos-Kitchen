using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class esc : MonoBehaviour
{
    public void Quitgame()
    {
        SceneManager.LoadScene(0);
    }
}
