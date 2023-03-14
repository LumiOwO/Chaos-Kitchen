using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jeepControl : MonoBehaviour
{
    float v = 8f;
    float dist = 0f;

    float[] checkpoint = new float[] { 23f, 17.5f, 50f, 1f};
    int state = 0;

    Vector3 origin;
    Vector3 velocity;
    Quaternion rotate;
    private void Start() {
        origin = transform.position;
        rotate = transform.rotation;
        velocity = new Vector3(0f, 0f, v);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(state);
        if (dist > checkpoint[state]) {
            dist = 0;
            if(state == 3) {
                transform.position = origin;
                transform.rotation = rotate;
                velocity = new Vector3(0f, 0f, v);
                state = 0;
                return;
            }

            Vector3 r = new Vector3(0, 90, 0);
            velocity = Quaternion.Euler(r) * velocity;
            transform.Rotate(r);
            state++;
        }

        Vector3 delta = velocity * Time.deltaTime;
        transform.position += delta;
        dist += v * Time.deltaTime;
    }
}
