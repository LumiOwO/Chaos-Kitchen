using UnityEngine;

public class pausebtnControl : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject panel;
    GameObject playerObj;
    void Start()
    {
        panel = GameObject.FindGameObjectWithTag("pausepanel");
        panel.SetActive(false);

        playerObj = GameObject.FindGameObjectWithTag("Player");
        
    }

    public void ShowPausePanel()
    {
        panel.SetActive(!panel.activeInHierarchy);
        
        SimpleSampleCharacterControl control = playerObj.GetComponentInChildren<SimpleSampleCharacterControl>();
        control.PAUSE = 1 - control.PAUSE;
    }
}
