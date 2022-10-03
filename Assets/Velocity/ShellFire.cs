using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellFire : MonoBehaviour
{
    public float speed = 1;

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
