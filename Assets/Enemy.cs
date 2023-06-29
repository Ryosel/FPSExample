using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 태어날 때 agnet에게 목적지(플레이어)를 알려주고싶다.
public class Enemy : MonoBehaviour
{
    public float speed = 5;
    GameObject target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        // 태어날 때 목적지(플레이어)를 찾고싶다.
        target = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
       
    }

    // Update is called once per frame
    void Update()
    {
        // agent야 너의 목적지는 target의 위치야
        agent.destination = target.transform.position;
    }
}
