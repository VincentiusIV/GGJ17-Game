using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int playerID;

    // Shooting
    public GameObject bullet;
    public float bulletSpeed;
    public float destroyTime;

    // Private references
    private Rigidbody2D rb;
    private Transform aim;
    private Transform bullSpawnPos;
    private BulletScript bs;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aim = transform.FindChild("Aim");

        bs = bullet.GetComponent<BulletScript>();
        bs.speed = bulletSpeed;
        bs.destroyTime = destroyTime;

        bullSpawnPos = transform.FindChild("SpawnPos");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("A_"+playerID))
            Debug.Log("Pressed A_"+playerID);

        // Movement
        float xPos = Input.GetAxis("L_XAxis_" + playerID);
        float yPos = 0f;
        Vector3 movement = new Vector3(xPos, yPos, 0f);
        rb.velocity = movement;

        // Rotation
        float angleRad = Mathf.Atan2(aim.transform.position.y - transform.position.y, aim.transform.position.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        bullSpawnPos.rotation = Quaternion.Euler(0, 0, angleDeg + -90);

        // Aiming
        float aimX = Input.GetAxis("R_XAxis_" + playerID);
        float aimY = Input.GetAxis("R_YAxis_" + playerID);
        Vector3 aimPos = new Vector3(aimX, -aimY, 0f);
        aim.transform.position = aimPos + transform.position;

        // Shooting
        if (Input.GetAxisRaw("TriggersR_" + playerID) > 0)
            Instantiate(bullet, bullSpawnPos.position, bullSpawnPos.rotation);
    }
}
