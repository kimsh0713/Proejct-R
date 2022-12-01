using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScripts : MonoBehaviour
{
    [Header("Player Stauts")]
    [Tooltip("�÷��̾� �̵��ӵ�")]
    public float MoveSpeed;
    [Tooltip("�⺻ �̵��ӵ�")]
    public float NormalSpeed;
    [Tooltip("�޸��� �̵��ӵ�")]
    public float SprintSpeed;
    [Tooltip("�뽬 �̵��ӵ�")]
    public float DashSpeed;
    [Tooltip("�뽬 ���ӽð�"), Range(0.01f, 0.5f)]
    public float DashTime;

    [Header("Others")]
    private Camera cam;
    private Rigidbody2D rigid;
    private bool isDash;
    private CircleCollider2D body_Col;

    IEnumerator Dash_Coroutine;

    float h, v;
    Vector2 dir;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        body_Col = GetComponent<CircleCollider2D>();

        cam = Camera.main;
    }

    private void Start()
    {
        MoveSpeed = NormalSpeed;
        cam.transform.SetParent(transform);
    }

    private void Update()
    {
        SetDirection();
        Sprint();
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (Dash_Coroutine != null)
                StopCoroutine(Dash_Coroutine);

            Dash_Coroutine = E_Dash();
            StartCoroutine(Dash_Coroutine);
        }
    }

    private void SetDirection()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        dir = new Vector2(h, v).normalized;
    }

    private void Sprint()
    {
        if (!isDash)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                MoveSpeed = SprintSpeed;
            }
            else
            {
                MoveSpeed = NormalSpeed;
            }
        }
    }

    private IEnumerator E_Dash()
    {
        isDash = true;
        body_Col.enabled = !isDash;
        MoveSpeed = DashSpeed;

        yield return new WaitForSeconds(DashTime);

        isDash = false;
        body_Col.enabled = !isDash;
        MoveSpeed = NormalSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigid.velocity = dir * MoveSpeed;
    }
}