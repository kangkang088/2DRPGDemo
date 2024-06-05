using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour {
    public GameObject effectGo;
    public AudioClip pickSound;
    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log(collision.name +  " Touched Potion!");
        LunaController lunaController = collision.gameObject.GetComponent<LunaController>();
        if(lunaController != null) {
            if(GameManager.Instance.lunaCurrentHP < GameManager.Instance.lunaHP) {
                GameManager.Instance.AddOrDecreaseHP(40);
                Instantiate(effectGo,transform.position,Quaternion.identity);//被拾起时生成特效
                GameManager.Instance.PlayeSound(pickSound);
                Destroy(gameObject);
            }
            
        }   
    }
}
