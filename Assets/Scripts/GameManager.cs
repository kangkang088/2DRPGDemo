using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public static GameManager Instance => instance;
    //Luna����
    public int lunaHP;//�������ֵ
    public float lunaCurrentHP;//��ǰ����ֵ
    public int lunaMP;
    public float lunaCurrentMP;
    //��������
    public int monsterCurrentHP;//���ﵱǰѪ��

    public GameObject battleGo;//ս���������

    public int dialogInfoIndex;//ÿһ�ε�����
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
        lunaCurrentHP = lunaHP;//��ʼʱ����ֵΪ��ֵ
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
    //�޸�Luna����ֵ
    //public void ChangeHP(int amount) {
    //    if(amount > 0) {
    //        lunaCurrentHP = Mathf.Clamp(lunaCurrentHP + amount,0,lunaHP);
    //        Debug.Log(lunaCurrentHP + "/" + lunaHP);
    //    }
    //}
    #endregion
    //�޸�Luna����ֵ
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
    //�޸�Lunaħ��ֵ
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
    //�Ƿ����ʹ����ؼ���
    public bool CanUsePlayerMP(int value) {
        return lunaCurrentMP >= value;
    }
    //�޸Ĺ���Ѫ��
    public int AddOrDecreaseMonsterHP(int value) {
        monsterCurrentHP += value;
        return monsterCurrentHP;
    }
    //����ս������
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
