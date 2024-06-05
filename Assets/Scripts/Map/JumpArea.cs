using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpArea : MonoBehaviour {
    public Transform jumpPointA;
    public Transform jumpPointB;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Luna")) {
            LunaController lunaController = collision.transform.GetComponent<LunaController>();
            lunaController.Jump(true);
            float disA = Vector3.Distance(lunaController.transform.position, jumpPointA.position);
            float disB = Vector3.Distance(lunaController.transform.position,jumpPointB.position);
            Transform targetPos;
            targetPos = disA > disB? jumpPointA : jumpPointB;
            lunaController.transform.DOMove(targetPos.position,0.5f).SetEase(Ease.Linear).OnComplete(() => { JumpOverCallback(lunaController); });
            Transform lunaLocalTrans = lunaController.transform.GetChild(0);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(lunaLocalTrans.DOLocalMoveY(1.5f,0.25f)).SetEase(Ease.InOutSine);
            sequence.Append(lunaLocalTrans.DOLocalMoveY(0.6f,0.25f)).SetEase(Ease.InOutSine);
            
        }
    }
    private void JumpOverCallback(LunaController lunaController) {
        lunaController.Jump(false);
    }
}
