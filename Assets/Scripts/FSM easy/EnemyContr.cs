using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum States
{
    Attack,
    Idle,
    Chase,
    Die,
    Run
}
public class EnemyContr : MonoBehaviour
{
    public int HP;
    public GameObject target;
    private ChaseBehaviour _chaseB;
    public States Currentstate;

    void Start()
    {
        _chaseB = GetComponent<ChaseBehaviour>();
        Currentstate = States.Idle;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(CheckIfAlife())
        {
            target = collision.gameObject;
            Currentstate = States.Chase;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CheckIfAlife())
        {
            Currentstate = States.Idle;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(CheckIfAlife())
            Currentstate = States.Attack;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(CheckIfAlife())
            Currentstate = States.Chase;
    }
    public bool CheckIfAlife()
    {
        if (HP < 1)
        {
            Currentstate = States.Die;
            return false;
        }
        return true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HP--;
            CheckIfAlife();
        }
        switch (Currentstate)
        {
            case States.Attack:
                Attack();
                break;
            case States.Idle:
                Idle();
                break;
            case States.Chase:
                Chase();
                break;
            case States.Die:
                Die();
                break;
        }
    }
    public void Attack()
    {
        GameManager.gm.UpdateText("Te reviento a chancletaso");
        _chaseB.StopChasing();
    }
    public void Idle()
    {
        GameManager.gm.UpdateText("Here chillin");
        _chaseB.StopChasing();
    }
    public void Run()
    {
        GameManager.gm.UpdateText("CoSorro");
        _chaseB.Run(target.transform, transform);
    }
    public void Die()
    {
        GameManager.gm.UpdateText("Abandoné este mundo de miseria y desesperación");
        _chaseB.StopChasing();
    }
    public void Chase()
    {
        GameManager.gm.UpdateText("Ven que te quiero decir una cosa");
        _chaseB.Chase(target.transform, transform);
    }
}
