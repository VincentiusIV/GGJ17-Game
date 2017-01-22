using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int base1HP, base2HP;
    public GameObject defSpawnPosTeam1, defSpawnPosTeam2;
    public GameObject[] spawnPositions;
    public float respawnTime;

    public int team1DeathCount, team2DeathCount;

    void Start()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            spawnPositions[i].GetComponent<SpawnPosition>().spawnState = "nobody";
        }
    }
    void Update()
    {
        if (base1HP <= 0)
            Debug.Log("Team 1 died, 2 wins!");
        else if (base2HP <= 0)
            Debug.Log("Team 2 died, 1 wins!");
    }
    public void CapturePoint(int pointID, string team)
    {
        spawnPositions[pointID].GetComponent<SpawnPosition>().spawnState = team;
    }

	void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Team1") || col.CompareTag("Team2"))
        {
            col.GetComponent<PlayerController>().hp = 0;
        }
    }

    public IEnumerator RespawnPlayer(PlayerController player)
    {
        player.enabled = false;
        
        if (player.CompareTag("Team1"))
            team1DeathCount += 1;
        else if (player.CompareTag("Team2"))
            team2DeathCount += 1;

        yield return new WaitForSeconds(respawnTime);
        player.hp = player.maxHP;
        player.enabled = true;
        Debug.Log("Respawning player: " + player.name);

        Vector3 spawn = new Vector3();
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            if(spawnPositions[i].GetComponent<SpawnPosition>().spawnState == player.tag)
            {
                spawn = spawnPositions[i].transform.position;
                break;
            }
        }
        player.transform.position = spawn;
    }
}
