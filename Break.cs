using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour {

    public float timeOut;
    private float timeElapsed;
    float x, y;
    void Start () {
        x= Random.Range(-0.1f, 0.1f);
        y = Random.Range(-0.1f, 0.1f);

    }

    void Update () {
        timeElapsed += Time.deltaTime;
        transform.position += new Vector3(x, y, 0);

        if (timeElapsed >= timeOut)
        {
            Destroy(this.gameObject);
            timeElapsed = 0.0f;

        }

    }
}
