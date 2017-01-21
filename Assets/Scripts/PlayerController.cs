using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    public PlayerIndex playerIndex;
    // Movement
    public Vector2 moveSpeed;
    public AnimationCurve jumpCurve;

    // Shooting
    public GameObject bullet;
    public float bulletSpeed;
    public float destroyTime;
    public float fireSpeed;
    private float nextShot;
    // Combat
    public int hp;
    public int maxHP;

    // Private references
    private Rigidbody2D rb;
    private Transform aim;
    private Transform bullSpawnPos;
    private BulletScript bs;
    private Animator ani;
    private VisibilityScript vs;
    private Transform hpBar;
    private bool switchSide;
    private bool invisible;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aim = transform.FindChild("Aim");

        bs = bullet.GetComponent<BulletScript>();
        bs.speed = bulletSpeed;
        bs.destroyTime = destroyTime;

        bullSpawnPos = transform.FindChild("SpawnPos");

        ani = GetComponent<Animator>();

        vs = GetComponent<VisibilityScript>();
        vs.BecomeInvisible();

        hpBar = transform.FindChild("HealthBar");

        nextShot = fireSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float xPos = Input.GetAxis("L_XAxis_" + (1+(int)playerIndex)) * moveSpeed.x;

        Vector2 movement = new Vector2(xPos, rb.velocity.y);
        rb.velocity = movement;
        if (xPos == 0)
        {
            ani.SetBool("isWalking", false);
            vs.BecomeInvisible();
        }
            
        else if (xPos > 0)
        {
            vs.BecomeVisible();
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            ani.SetBool("isWalking", true);
        }  
        else if (xPos < 0)
        {
            vs.BecomeVisible();
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            ani.SetBool("isWalking", true);
        }
        
        if (Input.GetButtonDown("A_" + ((1+ (int)playerIndex))))
        {
            StartCoroutine(SetVibrations(1f, 0.5f));
            StartCoroutine(Jump());
        }
            
        // Rotation
        float angleRad = Mathf.Atan2(aim.transform.position.y - transform.position.y, aim.transform.position.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        bullSpawnPos.rotation = Quaternion.Euler(0, 0, angleDeg + -90);

        // Aiming
        float aimX = Input.GetAxis("R_XAxis_" + ((int)playerIndex + 1));
        float aimY = Input.GetAxis("R_YAxis_" + ((int)playerIndex + 1));
        Vector3 aimPos = new Vector3(aimX, -aimY, 0f);
        aim.transform.position = aimPos + transform.position;

        // Shooting
        
        if (Time.time > fireSpeed && Input.GetAxisRaw("TriggersR_" + (((int)playerIndex) + 1)) != 0)
        {
            
            fireSpeed = Time.time + nextShot; Debug.Log(fireSpeed);
            Instantiate(bullet, aim.position, bullSpawnPos.rotation);
        }

        // HP bar
        if(hp <= 0)
        {
            Debug.Log("Player " + ((int)playerIndex + 1) + " Died");
            gameObject.SetActive(false);
        }
        hpBar.localScale = new Vector3(hp / maxHP, 1f, 1f);
            
    }

    IEnumerator Jump()
    {
        for (float i = 0; i < 1; i += 0.25f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpCurve.Evaluate(i) * 10);
            yield return new WaitForSeconds(0f);
        }
    }

    IEnumerator SetVibrations(float duration, float intensity)
    {
        GamePad.SetVibration(playerIndex, intensity, intensity);
        yield return new WaitForSeconds(duration);
        GamePad.SetVibration(playerIndex, 0f, 0f);
    }
}
