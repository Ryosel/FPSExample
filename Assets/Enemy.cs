using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{   
    public enum State{
        Idle,
        Move,
        Attack
    }
    public State state;

    public float attackRange = 3f;
    public float speed = 5;
    public GameObject target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case State.Idle: UpdateIdle(); break;
            case State.Move: UpdateMove(); break;
            case State.Attack: UpdateAttack(); break;
        }

        //target을 향하는 dir
        Vector3 dir = target.transform.position - this.transform.position;
        dir.Normalize(); //Normalize

        //dir 방향으로 이동(p += vt, p = p0 + vt)
        //transform.position += dir * speed * Time.deltaTime; 

        //Using Agent
    }

    private void UpdateIdle(){
        target = GameObject.Find("Player");
        if(target != null){
            state = State.Move;
        }
    }
    private void UpdateMove(){
        agent.destination = target.transform.position;  //class 변수 객체는 reference, 나머지는 모두 value로 전달. (pos는 reference로 전달?)
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        if(distance < attackRange){
            state = State.Attack;
        }
    }
    
    //SubState 'attackSubState', attackSubState ? Attack 대기 : Attack 중

    enum AttackSubState{
        Attack,
        Wait
    }
    AttackSubState attackSubState;

    //bool attackSubState;
    bool isAttackHit;
    float currTime;
    float attackHitTime = 0.5f, attackFinishedTime = 2f, attackWaitTime = 1f;

    
    private void UpdateAttack(){
        currTime += Time.deltaTime;

        if(attackSubState == AttackSubState.Wait){ //Attack 대기
            if(currTime >= attackWaitTime){
                attackSubState = AttackSubState.Attack;
                currTime = 0;
            }
        }
        else if(attackSubState == AttackSubState.Attack){ //Attack 중
            if(currTime >= attackHitTime){
                if(isAttackHit == false){
                    isAttackHit = true;
                }
                if(currTime > attackFinishedTime){
                    attackSubState = AttackSubState.Wait;
                    currTime = 0;
                }
            }
        }
    }
}
