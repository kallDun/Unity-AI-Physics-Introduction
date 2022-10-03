using UnityEngine;

public class Shell_physics : MonoBehaviour
{
    public GameObject explosion;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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
        transform.forward = rb.velocity;
    }
}