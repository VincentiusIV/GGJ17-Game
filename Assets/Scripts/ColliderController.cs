using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ColliderController : MonoBehaviour {

    [SerializeField] private CircleCollider2D circleCollider;

    public string targetTeamTag { get; set; }
    public string colliderTag { get;set; }
    public float baseSpeed { get; set; }
    public float baseRadius { get; set; }
    public float maxRadius { get; set; }
    public Color color { get; set; }
    public bool isActive { get; set; }
    public bool repeat { get; set; }
    public int id { get; set; }


    private GeneratorController gc;
    private DrawCircle drawCircle;


    [SerializeField] private LineRenderer lr;


    public void Setup()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        lr = GetComponent<LineRenderer>();

        circleCollider.radius = baseRadius;


        if (!repeat)
        {
            lr.startColor = new Color(Random.value, Random.value, Random.value, 1.0f);
            lr.endColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        }else
        {
            lr.startColor = color;
            lr.endColor = color;
        }


        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;

        drawCircle = GetComponent<DrawCircle>();
        drawCircle._horizRadius = 0.1f;
        drawCircle._vertRadius = 0.1f;


        isActive = true;

        Debug.Log(color);
        Debug.Log(targetTeamTag);
    }

    void Update()
    {
        
        if(isActive && circleCollider.radius >= maxRadius)
        {
            if (!repeat) {
                isActive = false;
                Destroy(this.gameObject, 0.5f);
            }

            drawCircle.random = Random.value;
            drawCircle._horizRadius = baseRadius;
            drawCircle._vertRadius = baseRadius;
            circleCollider.radius = baseRadius;
        }

        if(isActive && circleCollider.radius <= maxRadius)
        {
            drawCircle._horizRadius += baseSpeed * 2;
            drawCircle._vertRadius += baseSpeed * 2;
            circleCollider.radius += baseSpeed;
        }
    }


    IEnumerator OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.tag == targetTeamTag)
        {
            VisibilityScript vs = _col.GetComponent<VisibilityScript>();
            //Do stuff when player get hit by the "wave"
            vs.StopAllCoroutines();
            vs.isChanging = false;
            yield return StartCoroutine(vs.Visible());
            yield return StartCoroutine(vs.Invisible());
        }
    }


    public void SetSpeed(float _speed)
    {
        baseSpeed = _speed;
    }
}
