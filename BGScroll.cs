using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    // スクロールするスピード
    public float speed = 0.1f;
    public Transform CamSpeed;
    public float DeltaSpeed = 0;

    private void Start()
    {

    }
    void Update()
    {
        DeltaSpeed = CamSpeed.transform.position.x * speed * 0.01f;

        // 時間によってXの値が0から1に変化していく。1になったら0に戻り、繰り返す。
        float x = Mathf.Repeat( DeltaSpeed, 1);

        // Xの値がずれていくオフセットを作成
        Vector2 offset = new Vector2(x, 0);

        // マテリアルにオフセットを設定する
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);

    }
}
