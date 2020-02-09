using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {
    public Transform CharPosition;
    public float HoldX, HoldY, HoldZ;
    public float LeftLimit;
    public float x;
    public float wallWidthHalf;
    public float wallHeightHalf;
    public float backLimit;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Use this for initialization
    void Start () {
        x = CharPosition.transform.position.x;
        wallWidthHalf = Camera.main.orthographicSize * Camera.main.aspect;
        wallHeightHalf = Camera.main.orthographicSize;

        
    }

    // Update is called once per frame
    void Update () {

        GetComponent<Transform>().position= new Vector3(  x+HoldX,HoldY,HoldZ);
      // if (LeftLimit < CharPosition.transform.position.x )
        //{
            LeftLimit = CharPosition.transform.position.x ;
            x = CharPosition.transform.position.x;

        //}
        
        
       
        if (LeftLimit > CharPosition.transform.position.x)
        {
            x =LeftLimit;

        }

        if (backLimit > x)
        {
            x = backLimit;
        }
        

    }
}
