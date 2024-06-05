using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public static GameManager Instance => instance;
    //Luna属性
    public int lunaHP;//最大生命值
    public float lunaCurrentHP;//当前生命值
    public int lunaMP;
    public float lunaCurrentMP;
    //怪物属性
    public int monsterCurrentHP;//怪物当前血量

    public GameObject battleGo;//战斗界面对象

    public int dialogInfoIndex;//每一段的索引
    public bool canControlLuna;

    public bool hasPetTheDog;
    public int candleNum;
    public int killNum;
    public GameObject monstersGo;

    public NPCDialog npc;
    public bool enterBattle;

    public GameObject battleMonsterGo;

    public AudioSource audioSource;
    public AudioClip normalClip;
    public AudioClip battleClip;
    private void Awake() {
        instance = this;
        lunaCurrentHP = lunaHP;//开始时生命值为满值
    }
    private void Update() {
        if(!enterBattle) {
            if(lunaCurrentHP <= 100) {
                AddOrDecreaseHP(Time.deltaTime);
            }
            if(lunaCurrentMP <= 100) {
                AddOrDecreaseMP(Time.deltaTime);
            }
        }
    }
    #region Unuseful
    //修改Luna生命值
    //public void ChangeHP(int amount) {
    //    if(amount > 0) {
    //        lunaCurrentHP = Mathf.Clamp(lunaCurrentHP + amount,0,lunaHP);
    //        Debug.Log(lunaCurrentHP + "/" + lunaHP);
    //    }
    //}
    #endregion
    //修改Luna生命值
    public void AddOrDecreaseHP(float value) {
        lunaCurrentHP += value;
        if(lunaCurrentHP >= lunaHP) {
            lunaCurrentHP = lunaHP;
        }
        if(lunaCurrentHP <= 0) {
            lunaCurrentHP = 0;
        }
        UIManager.Instance.SetHPValue((float)lunaCurrentHP / lunaHP);
    }
    //修改Luna魔法值
    public void AddOrDecreaseMP(float value) {
        lunaCurrentMP += value;
        if(lunaCurrentMP >= lunaMP) {
            lunaCurrentMP = lunaMP;
        }
        if(lunaCurrentMP <= 0) {
            lunaCurrentMP = 0;
        }
        UIManager.Instance.SetMPValue((float)lunaCurrentMP / lunaMP);
    }
    //是否可以使用相关技能
    public bool CanUsePlayerMP(int value) {
        return lunaCurrentMP >= value;
    }
    //修改怪物血量
    public int AddOrDecreaseMonsterHP(int value) {
        monsterCurrentHP += value;
        return monsterCurrentHP;
    }
    //显隐战斗界面
    public void EnterOrExitBattle(bool enter = true,int addKillNum = 0) {
        UIManager.Instance.ShowOrHideBattlePanel(enter);
        battleGo.SetActive(enter);
        if(!enter) {
            killNum += addKillNum;
            if(addKillNum > 0) {
                DestoryMonster();
            }
            if(killNum >= 5) {
                SetContentIndex();
            }
            monsterCurrentHP = 50;
            PlayMusic(normalClip);
            if(lunaCurrentHP <= 0) {
                lunaCurrentHP = 100;
                lunaCurrentMP = 0;
                battleMonsterGo.transform.position += new Vector3(0,2,0);
            }
        }
        else {
            PlayMusic(battleClip);
        }
        enterBattle = enter;
    }
    public void ShowMonsters() {
        if(!monstersGo.activeSelf) {
            monstersGo.SetActive(true);
        }
    }
    public void SetContentIndex() {
        npc.SetContentIndex();
    }
    public void DestoryMonster() {
        Destroy(battleMonsterGo);
    }
    public void SetMonster(GameObject go) {
        battleMonsterGo = go;
    }
    public void PlayMusic(AudioClip audioClip) {
        if(audioSource.clip != audioClip) {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
    public void PlayeSound(AudioClip audioClip) {
        if(audioClip) {
            audioSource.PlayOneShot(audioClip);
        }
    }

}
