using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {
    public Button btn;

    void Start() {
        btn.onClick.AddListener(() => { OnClickBtn(btn.gameObject); });
    }

    void OnClickBtn(GameObject g) {
        SceneManager.LoadScene("Welcome");
    }
}

