using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {
    public Button btn;

    void Start() {
        btn.onClick.AddListener(() => { OnClickBtn(btn.gameObject); });
    }

    void OnClickBtn(GameObject g) {
        Debug.Log(g.name);
        SceneManager.LoadScene(g.name);
    }
}
