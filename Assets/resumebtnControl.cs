using UnityEngine;

public class resumebtnControl : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject panel;
    GameObject playerObj;
    void Start() {
        panel = GameObject.FindGameObjectWithTag("pausepanel");

        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    public void Resume() {
        panel.SetActive(false);
        SimpleSampleCharacterControl control = playerObj.GetComponentInChildren<SimpleSampleCharacterControl>();
        control.PAUSE = 1 - control.PAUSE;
    }
}
