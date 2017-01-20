using UnityEngine;
using System.Collections;

public class FloorSpawner : MonoBehaviour
{
    public GameObject buildingBlock;
    public GameObject platform;
    public float length;
    public bool invertPosition;

    private Vector3 startPos;
    private float xPos;
    private float yPos;
    private float zPos;

	// Use this for initialization
	void Start ()
    {
        getVectors();
        createStructure();
	}
	
	void createStructure()
    {
        for (int i = 0; i < length; i++)
        {
            Instantiate(buildingBlock, startPos, Quaternion.identity);
            startPos.z += 1;
        }
    }

    void getVectors()
    {
        xPos = (platform.transform.lossyScale.y / 2);

        if (invertPosition)
        {
            xPos *= -1;
        }

        yPos = (platform.transform.position.y + (buildingBlock.transform.lossyScale.y / 2));

        zPos = platform.transform.position.z + (platform.transform.lossyScale.x / -2);

        startPos = new Vector3(xPos, yPos, zPos);
    }
}
