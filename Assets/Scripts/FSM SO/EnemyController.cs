using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public StatesSO CurrentState;
    public int HP;
    public GameObject target;
    private ChaseBehaviour _chaseB;
    void Start()
    {
        _chaseB = GetComponent<ChaseBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject;
        GoToState<ChaseState>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GoToState<IdleState>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GoToState<AttackState>();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GoToState<ChaseState>();
    }
    public void CheckIfAlife()
    {
        if (HP < 1)
        {
            GoToState<DieState>();
        }
        else if(HP < 5)
        {
            GoToState<RunState>();
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HP--;
            CheckIfAlife();
        }
        CurrentState.OnStateUpdate(this);
    }

    public void GoToState<T>() where T : StatesSO
    {
        if (CurrentState.StatesToGo.Find(state => state is T))
        {
            CurrentState.OnStateExit(this);
            CurrentState = CurrentState.StatesToGo.Find(obj => obj is T);
            CurrentState.OnStateEnter(this);
        }
    }
}
