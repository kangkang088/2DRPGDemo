using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour {
    public GameObject effectGo;
    public AudioClip pickClip;
    private void OnTriggerEnter2D(Collider2D collision) {
        GameManager.Instance.candleNum++;
        Instantiate(effectGo,transform.position,Quaternion.identity);//��ʰ��ʱ������Ч
        if(GameManager.Instance.candleNum >= 5) {
            GameManager.Instance.SetContentIndex();
        }
        GameManager.Instance.PlayeSound(pickClip);
        Destroy(gameObject);
    }
}
