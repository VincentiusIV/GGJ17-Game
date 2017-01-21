using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour {
    [SerializeField] private string[] teamTags;
    [SerializeField] private GameObject wavePrefab;

    [SerializeField] private float baseSpeed;
    [SerializeField] private float baseRadius;
    [SerializeField] private float maxRadius;
    [SerializeField] private string colliderTagPrefix;

    void Start()
    {
        foreach (var _team in teamTags)
        {
            SpawnWave(_team, true);
        }

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject _comp = GameObject.Find("CC_TEAM_01");

            _comp.GetComponent<ColliderController>().baseSpeed += 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject _comp = GameObject.Find("CC_TEAM_02");
            _comp.GetComponent<ColliderController>().baseSpeed += 0.05f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnWave("test", false);
        }
    }

    void SpawnWave(string _team, bool _repeat)
    {
        GameObject _waveCollider = Instantiate(wavePrefab, transform.position, Quaternion.identity);
        _waveCollider.transform.name = colliderTagPrefix + _team;

        ColliderController _wcc = _waveCollider.GetComponent<ColliderController>();

        _wcc.targetTeamTag = _team;                  //Setup Collider Target Tag
        _wcc.baseSpeed = baseSpeed;                  //Set Collider baseSpeed
        _wcc.baseRadius = baseRadius;                //Set Collider base Radius
        _wcc.maxRadius = maxRadius;                  //Set collider max Radius
        _waveCollider.transform.parent = transform;  //Set parent to this object.
        _wcc.repeat = _repeat;

        switch (_team)
        {
            case "TEAM_01":
                _wcc.color = Color.red;
                break;
            case "TEAM_02":
                _wcc.color = Color.blue;
                break;
        }

        _wcc.Setup();                               //Apply all settings
    }
}
