using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public bool destroy;
    public float destroyDelay;

    // Use this for initialization
    void Start()
    {
        ChangeToRandomColor();

        if(destroy)
        {
            Destroy(this.gameObject, destroyDelay);
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(rotateSpeed, 0.0f, rotateSpeed));
    }

    void ChangeToRandomColor()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.material.color = new Color(Random.value, 0.0f, 0.0f);
    }
}
