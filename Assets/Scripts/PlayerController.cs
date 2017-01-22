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
    private Animator bloodAnimator;

    // Private references
    private Rigidbody2D rb;
    private Transform aim;
    private Transform bullSpawnPos;
    private BulletScript bs;
    private Animator ani;
    private VisibilityScript vs;
    private Transform hpBar;
    private GameController gc;

    private bool switchSide;
    private bool invisible;
    private bool isShooting;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aim = transform.FindChild("Aim");

        bs = bullet.GetComponent<BulletScript>();
        bs.speed = bulletSpeed;
        bs.destroyTime = destroyTime;

        bullSpawnPos = transform.FindChild("SpawnPosAxis");

        ani = GetComponent<Animator>();

        vs = GetComponent<VisibilityScript>();
        vs.BecomeInvisible();

        hpBar = transform.FindChild("HealthBar");
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        nextShot = fireSpeed;

        bloodAnimator = transform.FindChild("BloodSpatter").GetComponent<Animator>();
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
            if(!isShooting && vs.isVisible)
                vs.BecomeInvisible();
        }
            
        else if (xPos > 0)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            aim.localScale = new Vector3(1f, aim.localScale.y, aim.localScale.z);
            ani.SetBool("isWalking", true);
        }  
        else if (xPos < 0)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            aim.localScale = new Vector3(-1f, aim.localScale.y, aim.localScale.z);
            ani.SetBool("isWalking", true);
        }

        if (Input.GetAxisRaw("TriggersR_" + (((int)playerIndex) + 1)) < 0)
        {
            StartCoroutine(Jump());
        }

        // Rotation
        float angleRad = Mathf.Atan2(aim.transform.position.y - transform.position.y, aim.transform.position.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        bullSpawnPos.rotation = Quaternion.Euler(0, 0, angleDeg + -90);
        aim.rotation = Quaternion.Euler(0, 0, angleDeg);

        // Aiming
        float aimX = Input.GetAxis("R_XAxis_" + ((int)playerIndex + 1));
        float aimY = Input.GetAxis("R_YAxis_" + ((int)playerIndex + 1));
        Vector3 aimPos = new Vector3(aimX, -aimY, 0f);
        aim.transform.position = aimPos + transform.position;

        // Shooting
        if (Time.time > fireSpeed && Input.GetAxisRaw("TriggersR_" + (((int)playerIndex) + 1)) > 0)
        {
            isShooting = true;
            if(vs.isInvisible)
                vs.BecomeVisible();
            vs.isChanging = true;
            fireSpeed = Time.time + nextShot;

            if (gameObject.CompareTag("Team1"))
                bullet.GetComponent<BulletScript>().team1 = true;
            else if (gameObject.CompareTag("Team2"))
                bullet.GetComponent<BulletScript>().team1 = false;

            Instantiate(bullet, bullSpawnPos.GetChild(0).position, bullSpawnPos.rotation);
        }
        else if (Input.GetAxisRaw("TriggersR_" + (((int)playerIndex) + 1)) == 0)
            isShooting = false;
        // HP bar
        if (hp <= 0)
        {
            Debug.Log("Player " + ((int)playerIndex + 1) + " Died");
            StartCoroutine(SetVibrations(1f, 0.5f));
            StartCoroutine(gc.RespawnPlayer(this));
            
        }
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

    public void SpawnBlood()
    {
        StartCoroutine(BloodWait());
    }

    IEnumerator BloodWait()
    {
        bloodAnimator.SetBool("showBlood", true);
        yield return new WaitForSeconds(1ff);
        bloodAnimator.SetBool("showBlood", false);
    }
}
