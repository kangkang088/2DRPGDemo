using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {
    public Animator lunaAnimator;
    public Transform lunaTransform;
    public Transform monsterTransform;
    private Vector3 monsterInitPos;
    private Vector3 lunaInitPos;
    public SpriteRenderer monsterSR;
    public SpriteRenderer lunaSR;
    public GameObject skillEffectGo;
    public GameObject healEffectGo;
    public AudioClip attackSound;
    public AudioClip lunaAttackSound;
    public AudioClip monsterAttackSound;
    public AudioClip skillSound;
    public AudioClip recoverSound;
    public AudioClip hitSound;
    public AudioClip dieSound;
    public AudioClip monsterDieSound;
    private void Awake() {
        monsterInitPos = monsterTransform.localPosition;
        lunaInitPos = lunaTransform.localPosition;
    }
    private void OnEnable() {
        monsterSR.DOFade(1f,0.01f);
        lunaSR.DOFade(1f,0.01f);
        lunaTransform.localPosition = lunaInitPos;
        monsterTransform.localPosition = monsterInitPos;
    }
    //Luna¹¥»÷Âß¼­
    public void LunaAttack() {
        StartCoroutine(PerformAttackLogic());
    }
    IEnumerator PerformAttackLogic() {
        //Ñ¡Ôñ²Ù×÷ºóÒþ²ØÃæ°å
        UIManager.Instance.ShowOrHideBattlePanel(false);
        //¿ªÊ¼ÒÆ¶¯
        lunaAnimator.SetBool("MoveState",true);
        lunaAnimator.SetFloat("MoveValue",-1);
        lunaTransform.DOLocalMove(monsterInitPos + new Vector3(1.2f,0f,0f),0.5f).OnComplete(() => {
            GameManager.Instance.PlayeSound(attackSound);
            GameManager.Instance.PlayeSound(lunaAttackSound);
            lunaAnimator.SetBool("MoveState",false);
            lunaAnimator.SetFloat("MoveValue",0);
            lunaAnimator.CrossFade("Attack",0);
            monsterSR.DOFade(0.3f,0.2f).OnComplete(() => {
                JudgeMonsterHP(-20);
            });
        });
        yield return new WaitForSeconds(1.167f);
        lunaAnimator.SetBool("MoveState",true);
        lunaAnimator.SetFloat("MoveValue",1);
        lunaTransform.DOLocalMove(lunaInitPos,0.5f).OnComplete(() => {
            lunaAnimator.SetBool("MoveState",false);
        });
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MonsterAttack());
    }
    //ÅÐ¶Ï¹ÖÎïÑªÁ¿
    private void JudgeMonsterHP(int value) {

        if(GameManager.Instance.AddOrDecreaseMonsterHP(value) <= 0) {
            GameManager.Instance.PlayeSound(monsterDieSound);
            monsterSR.DOFade(0,0.4f).OnComplete(() => {
                GameManager.Instance.EnterOrExitBattle(false,1);
            });
        }
        else
            monsterSR.DOFade(1f,0.2f);
    }
    //Monster¹¥»÷Âß¼­
    IEnumerator MonsterAttack() {
        monsterTransform.DOLocalMove(lunaInitPos + new Vector3(-1.5f,0f,0f),0.5f);
        yield return new WaitForSeconds(0.5f);
        monsterTransform.DOLocalMove(lunaInitPos,0.2f).OnComplete(() => {
            GameManager.Instance.PlayeSound(monsterAttackSound);
            monsterTransform.DOLocalMove(lunaInitPos + new Vector3(-1.5f,0f,0f),0.2f);
            lunaAnimator.CrossFade("Hit",0);
            GameManager.Instance.PlayeSound(hitSound);
            lunaSR.DOFade(0.3f,0.2f).OnComplete(() => {
                lunaSR.DOFade(1f,0.2f);
            });
            JudgePlayererHP(-20);
        });
        yield return new WaitForSeconds(0.4f);
        monsterTransform.DOLocalMove(monsterInitPos,0.5f).OnComplete(() => { UIManager.Instance.ShowOrHideBattlePanel(true); });
    }
    //ÅÐ¶ÏÍæ¼ÒÑªÁ¿
    private void JudgePlayererHP(int value) {
        GameManager.Instance.AddOrDecreaseHP(value);
        if(GameManager.Instance.lunaCurrentHP <= 0) {
            lunaAnimator.CrossFade("Die",0);
            GameManager.Instance.PlayeSound(dieSound);
            lunaSR.DOFade(0,0.8f).OnComplete(() => {
                GameManager.Instance.EnterOrExitBattle(false);
            });
        }
    }
    //·ÀÓùÂß¼­
    public void LunaDefend() {
        StartCoroutine(PerformDefendLogic());
    }
    IEnumerator PerformDefendLogic() {
        UIManager.Instance.ShowOrHideBattlePanel(false);
        lunaAnimator.SetBool("Defend",true);
        monsterTransform.DOLocalMove(lunaInitPos + new Vector3(-1.5f,0f,0f),0.5f);
        yield return new WaitForSeconds(0.5f);
        monsterTransform.DOLocalMove(lunaInitPos,0.2f).OnComplete(() => {
            monsterTransform.DOLocalMove(lunaInitPos + new Vector3(-1.5f,0f,0f),0.2f);
            lunaTransform.DOLocalMove(lunaInitPos + new Vector3(1f,0,0),0.2f).OnComplete(() => {
                lunaTransform.DOLocalMove(lunaInitPos,0.2f);
            });
        });
        yield return new WaitForSeconds(0.4f);
        monsterTransform.DOLocalMove(monsterInitPos,0.5f).OnComplete(() => {
            UIManager.Instance.ShowOrHideBattlePanel(true);
            GameManager.Instance.PlayeSound(monsterAttackSound);
            lunaAnimator.SetBool("Defend",false);
        });
    }
    //Luna¹¥»÷¼¼ÄÜ
    public void LunaUseSkill() {
        if(!GameManager.Instance.CanUsePlayerMP(30)) {
            return;
        }
        StartCoroutine(PerformSkillLogic());
    }
    IEnumerator PerformSkillLogic() {
        UIManager.Instance.ShowOrHideBattlePanel(false);
        lunaAnimator.CrossFade("Skill",0);
        GameManager.Instance.AddOrDecreaseMP(-30);
        yield return new WaitForSeconds(0.35f);
        GameObject go = Instantiate(skillEffectGo,monsterTransform);
        go.transform.localPosition = Vector3.zero;
        GameManager.Instance.PlayeSound(lunaAttackSound);
        GameManager.Instance.PlayeSound(skillSound);
        yield return new WaitForSeconds(0.4f);
        monsterSR.DOFade(0.3f,0.2f).OnComplete(() => {
            JudgeMonsterHP(-40);
        });
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MonsterAttack());
    }
    //Luna»ØÑª¼¼ÄÜ
    public void LunaRecoverHP() {
        if(!GameManager.Instance.CanUsePlayerMP(50)) {
            return;
        }
        StartCoroutine(PerformRecoverHPLogic());
    }
    IEnumerator PerformRecoverHPLogic() {
        UIManager.Instance.ShowOrHideBattlePanel(false);
        lunaAnimator.CrossFade("RecoverHP",0);
        GameManager.Instance.AddOrDecreaseMP(-50);
        GameManager.Instance.PlayeSound(lunaAttackSound);
        GameManager.Instance.PlayeSound(recoverSound);
        yield return new WaitForSeconds(0.2f);
        GameObject go = Instantiate(healEffectGo,lunaTransform);
        go.transform.localPosition = Vector3.zero;
        GameManager.Instance.AddOrDecreaseHP(40);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MonsterAttack());
    }
    //LunaÌÓÅÜ
    public void LunaEscape() {
        UIManager.Instance.ShowOrHideBattlePanel(false);
        lunaTransform.DOLocalMove(lunaInitPos + new Vector3(5,0,0),0.5f).OnComplete(() => {
            GameManager.Instance.EnterOrExitBattle(false);
        });
        lunaAnimator.SetBool("MoveState",true);
        lunaAnimator.SetFloat("MoveValue",1);
    }
}
