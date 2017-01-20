using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    [SerializeField] private GeneratorController gc;
    [SerializeField] private  CircleCollider2D collider;
    [SerializeField] private float waveSpeed;
    [SerializeField] private float currentRadius;
    public bool isActive = false;


    public void Setup()
    {
        waveSpeed = gc.GetDefaultWaveSpeed();
        currentRadius = gc.GetDefaultWaveSize();
        collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (isActive && collider.radius >= gc.GetMaxRadius())
        {
            collider.radius = gc.GetMinRadius();
        }

        if (isActive && collider.radius <= gc.GetMaxRadius())
        {
            collider.radius += waveSpeed;
        }
    }

    public void SetRandomSpeed()
    {
        waveSpeed = Random.Range(0.01f, 0.25f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        //
    }
}
