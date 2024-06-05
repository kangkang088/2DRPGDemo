using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool vertical;//�������
    public float moveSpeed;//�ƶ��ٶ�
    private Rigidbody2D rigidBody2D;//ͨ�������ƶ�
    private int direction = 1;//�������
    public float changeTime;//����ı��ʱ����
    private float timer = 0;//��ʱ��
    private Animator animator;//����������
    // Start is called before the first frame update
    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
    }
    private void FixedUpdate() {
        if(GameManager.Instance.enterBattle) {
            return;
        }
        Vector3 pos = rigidBody2D.position;
        if(vertical) {
            //��ֱ�����ƶ�
            animator.SetFloat("LookX",0);
            animator.SetFloat("LookY",direction);
            pos.y = pos.y + moveSpeed * direction * Time.fixedDeltaTime;
        }
        else {
            //ˮƽ�����ƶ�
            animator.SetFloat("LookX",direction);
            animator.SetFloat("LookY",0);
            pos.x = pos.x + moveSpeed * direction * Time.fixedDeltaTime;
        }
        rigidBody2D.MovePosition(pos); 
    }
    // Update is called once per frame
    private void Update()
    {
        if(GameManager.Instance.enterBattle) {
            return;
        }
        timer -= Time.deltaTime;
        if(timer <= 0) {
            direction *= -1;
            timer = changeTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.transform.CompareTag("Luna")) {
            GameManager.Instance.EnterOrExitBattle(true);
            GameManager.Instance.SetMonster(gameObject);
        }
    }
}
