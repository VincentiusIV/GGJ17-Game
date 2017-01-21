using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject defSpawnPosTeam1, defSpawnPosTeam2;

    public GameObject[] spawnPositions;
    public float respawnTime;

    public int team1DeathCount, team2DeathCount;

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
        player.enabled = true;
        Debug.Log("Respawning player: " + player.name);
        int arrayPos = ReturnSpawnForTeam(player.gameObject.tag);
        Debug.Log("arraypos"+arrayPos);
        if (arrayPos == -1)
        {
            if (player.CompareTag("Team1"))
                player.transform.position = defSpawnPosTeam1.transform.position;
            else if (player.CompareTag("Team2"))
                player.transform.position = defSpawnPosTeam1.transform.position;
        }  
        else
            player.transform.position = spawnPositions[arrayPos].transform.position;
    }

    int ReturnSpawnForTeam(string tag)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            if (spawnPositions[i].CompareTag(tag))
                return i;
        }
        return -1;
    }
}
