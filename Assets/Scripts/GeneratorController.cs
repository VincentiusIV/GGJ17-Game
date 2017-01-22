using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour {

    [SerializeField] private string[] teamTags;         //Array of all teamTags 
    [SerializeField] public Color[] teamColors;        //Array of all teamColor (Wave Color) by index teamTags
    [SerializeField] private GameObject wavePrefab;     //Wave Prefab

    [SerializeField] private float baseSpeed;           //Default Starting speed
    [SerializeField] private float baseRadius;          //Default Base Radius
    [SerializeField] private float maxRadius;           //Max  Radius of the wave
    [SerializeField] private string colliderTagPrefix;  //Editor Name prefix for all colliders
    private bool error = false;
    void Start()
    {
        //Loop though all teams and spawn waves 
        for (int i = 0; i < teamTags.Length; i++)
        {
            Debug.Log(i);
            //SpawnWave(teamTags[i], i, true);
        }

    }


    void Update()
    {
        //Development
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
            SpawnWave("Team1", -1, false);
        }
    }

    void SpawnWave(string _team,int num, bool _repeat)
    {

        GameObject _waveCollider = Instantiate(wavePrefab, transform.position, Quaternion.identity);    //Spawn wavePrefab at Controller location
        _waveCollider.transform.name = colliderTagPrefix + _team;                                       //Set name in editor

        ColliderController _wcc = _waveCollider.GetComponent<ColliderController>();                     //Call to ColliderController attatched to _waveCollider   b 
        //* Setup all variables for the wave *//
        _wcc.targetTeamTag = _team;                  //Setup Collider Target Tag
        _wcc.baseSpeed = baseSpeed;                  //Set Collider baseSpeed
        _wcc.baseRadius = baseRadius;                //Set Collider base Radius
        _wcc.maxRadius = maxRadius;                  //Set collider max Radius
        _waveCollider.transform.parent = transform;  //Set parent to this object.
        _wcc.repeat = _repeat;                       //Set if the waves should repeat
        _wcc.id = num;                               //Set ID of the objects
            
        //Set wave color based on teamColors arry (index of teamTags)
        try
        {
            _wcc.color = teamColors[num];
        }
        catch (System.IndexOutOfRangeException)
        {
            //Debug.Log("Looks like there is something missing in the GeneratorController! Missing teamColor!");
            _wcc.color = Color.white;
        }
       

        //Run Setup in ColliderController
        _wcc.Setup();                               //Apply all settings
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        //Spawn new wave.
        SpawnWave("hitGenerator", -1, false);
    }
}
