using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shellPrefab;
    public GameObject shellSpawnPos;
    public GameObject target;
    public GameObject parent;
    public float reloadTime = 0.25f;
    float speed = 15;
    float turnSpeed = 2;
    bool canShoot = true;

    void Fire()
    {
        if (!canShoot) return;
        GameObject shell = Instantiate(shellPrefab, shellSpawnPos.transform.position, shellSpawnPos.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * transform.forward;
        canShoot = false;
        Invoke("ShootReload", reloadTime);
    }
    void ShootReload() => canShoot = true;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (target.transform.position - parent.transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, lookRot, Time.deltaTime * turnSpeed);
        float? angle = RotateTurret();

        if (angle != null && Vector3.Angle(dir, parent.transform.forward) < 10)
        {
            Fire();
        }
    }

    float? RotateTurret()
    {
        float? angle = CalcAngle(false);
        if (angle != null)
        {
            transform.localEulerAngles = new Vector3(360f - (float)angle, 0, 0);
        }
        return angle;
    }

    float? CalcAngle(bool low)
    {
        Vector3 targetDir = target.transform.position - transform.position;
        float y = targetDir.y;
        targetDir.y = 0;
        float x = targetDir.magnitude;
        float gravity = 9.81f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);
        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;
            if (low) return Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg;
            else return Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg;
        }
        else return null;
    }
}