using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool vertical;//轴向控制
    public float moveSpeed;//移动速度
    private Rigidbody2D rigidBody2D;//通过刚体移动
    private int direction = 1;//方向控制
    public float changeTime;//方向改变的时间间隔
    private float timer = 0;//计时器
    private Animator animator;//动画控制器
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
            //垂直轴向移动
            animator.SetFloat("LookX",0);
            animator.SetFloat("LookY",direction);
            pos.y = pos.y + moveSpeed * direction * Time.fixedDeltaTime;
        }
        else {
            //水平轴向移动
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
