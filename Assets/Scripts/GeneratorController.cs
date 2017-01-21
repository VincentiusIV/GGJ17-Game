using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour {
    [SerializeField] private string[] teamTags;
    [SerializeField] private CircleCollider2D[] teamWaveColliders;
    [SerializeField] private float defaultWaveSize;
    [SerializeField] private float defaultWaveSpeed;
    [SerializeField] private float maxRadius;
    [SerializeField] private float minRadius;
    [SerializeField] private GameObject particle;

    void Start()
    {
        //Setup all wavColliders
        for (int i = 0; i < teamWaveColliders.Length; i++)
        {
            teamWaveColliders[i].GetComponent<WaveController>().Setup();
            teamWaveColliders[i].GetComponent<WaveController>().isActive = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            teamWaveColliders[Random.Range(0, teamWaveColliders.Length)].GetComponent<WaveController>().SetRandomSpeed();
        }

    }
    public float GetDefaultWaveSize()
    {
        return defaultWaveSize;
    }
    public float GetDefaultWaveSpeed()
    {
        return defaultWaveSpeed;
    }

    public float GetMaxRadius()
    {
        return maxRadius;
    }

    public float GetMinRadius()
    {
        return minRadius;
    }

    public GameObject GetHitParticle()
    {
        return particle;
    }
}
