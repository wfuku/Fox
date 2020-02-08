using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLoop : MonoBehaviour
{
    public GameObject[] ground;
    public float sizeX;
    public int loop;
    public Vector3 startPos;

    //public bool random;
    // Start is called before the first frame update
    void Start()
    {
        sizeX = ground[0].GetComponent<SpriteRenderer>().bounds.size.x;
        for (int i = 0; i < loop; i++)
        {

            Instantiate(ground[i % ground.Length], new Vector3(sizeX * i + startPos.x, startPos.y, startPos.z), Quaternion.identity);
        }

    }
}