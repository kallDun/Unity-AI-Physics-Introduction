using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell_ : MonoBehaviour
{
    public float speed = 15;
    float mass = 10;
    float force = 500;
    float gravity = -9.8f;
    float gAccel, speedZ, speedY;
    float acceleration;
    public GameObject explosion;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(gameObject);
        }
    }
    private void LateUpdate()
    {
        acceleration = force / mass;
        speedZ += acceleration * Time.deltaTime;
        gAccel = gravity / mass;
        speedY += gAccel * Time.deltaTime;

        transform.Translate(0, speedY, speedZ);
        force = 0;
    }
}