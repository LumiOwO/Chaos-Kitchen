using UnityEngine;

public class Main : MonoBehaviour
{
    int[,] grid = new int[5, 4];

    int prev_x = -1;
    int prev_y = -1;

    
    // Start is called before the first frame update
    void Start()
    {
        // initialize
    }

    public void UpdateHighlight(int x, int y, int direct) {
        if(HasObject(x, y)) {
            //grid[x, y]->update();
        }

        prev_x = x;
        prev_y = y;
    }

    bool HasObject(int x, int y) {
        return false;
    }
}
