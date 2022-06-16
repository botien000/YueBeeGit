using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float forceJump;

    private Animator animator;
    private Rigidbody2D rgbody;
    private bool isHitted;
    private GameManager instanceGM;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rgbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        instanceGM = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {


        if (transform.position.y <= -6)
        {
            Die();
            return;
        }
        if (instanceGM.CurStateGame == GameManager.StateGame.OverGame)
            return;
        if (instanceGM.CurStateGame == GameManager.StateGame.MenuGame || instanceGM.CurStateGame == GameManager.StateGame.QuestionPanelGame)
        {
            rgbody.velocity = Vector2.zero;
            return;
        }
        Fly();
    }
    private void FixedUpdate()
    {

    }
    private void Fly()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rgbody.velocity = Vector2.up * forceJump;
        }
    }
    private void Die()
    {
        instanceGM.SetState(GameManager.StateGame.OverGame);
        Destroy(gameObject);
    }
    private void SetAnimation()
    {
        animator.SetBool("isHitted", isHitted);
    }
    /// <summary>
    /// Thay đổi trọng lực
    /// </summary>
    /// <param name="scale"></param>
    public void SetGravityScale(int scale)
    {
        rgbody.gravityScale = scale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Answer"))
        {
            AnswerManager answerManager = FindObjectOfType<AnswerManager>();
            answerManager.Interact(collision.gameObject);
        }

    }
}
