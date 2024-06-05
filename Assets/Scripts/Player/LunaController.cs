using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaController : MonoBehaviour {
    private Rigidbody2D rigidBody;
    public float moveSpeed;
    private Animator animator;
    private Vector2 lookDirection = new Vector2(0,0);
    private float moveScale = 0;
    private Vector2 move;
    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

    }
    private void FixedUpdate() {
        if(GameManager.Instance.enterBattle) {
            return;
        }
        Vector2 position = transform.position;
        //position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
        //position.y = position.y + moveSpeed * vertical * Time.deltaTime;
        position += moveSpeed * move * Time.fixedDeltaTime;
        rigidBody.MovePosition(position);
    }
    // Update is called once per frame
    void Update() {
        if(GameManager.Instance.enterBattle) {
            return;
        }
        if(!GameManager.Instance.canControlLuna) {
            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        move = new Vector2(horizontal,vertical);
        //animator.SetFloat("MoveValue",0);
        //确定Lua的看向
        if(!Mathf.Approximately(move.x,0) || !Mathf.Approximately(move.y,0)) {
            lookDirection.Set(move.x,move.y);
            lookDirection.Normalize();//单位化向量
            //animator.SetFloat("MoveValue",1);
        }
        animator.SetFloat("Look X",lookDirection.x);
        animator.SetFloat("Look Y",lookDirection.y);
        moveScale = move.magnitude;
        if(move.magnitude > 0) {
            if(Input.GetKey(KeyCode.LeftShift)) {
                moveScale = 1;
                moveSpeed = 4;
            }
            else {
                moveScale = 2;
                moveSpeed = 6;
            }
        }
        animator.SetFloat("MoveValue",moveScale);

        if(Input.GetKeyDown(KeyCode.Space)) {
            Talk();
        }
    }
    public void Climb(bool start) {
        animator.SetBool("Climb",start);
    }
    public void Jump(bool start) {
        animator.SetBool("Jump",start);
        rigidBody.simulated = !start;
    }
    public void Talk() {
        Collider2D collider2D = Physics2D.OverlapCircle(rigidBody.position,0.5f,LayerMask.GetMask("NPC"));
        if(collider2D != null) {
            if(collider2D.name == "Nala") {
                GameManager.Instance.canControlLuna = false;
                collider2D.GetComponent<NPCDialog>().DisplayDialog();
            }
            else if(collider2D.name == "Dog" && !GameManager.Instance.hasPetTheDog && GameManager.Instance.dialogInfoIndex == 2) {
                PetTheDog();
                GameManager.Instance.canControlLuna = false;
                collider2D.GetComponent<Dog>().BeHappy();
            }
        }
    }
    public void PetTheDog() {
        animator.CrossFade("PetTheDog",0);
        transform.position = new Vector3(-1.19f,-7.83f,0);
    }
}
