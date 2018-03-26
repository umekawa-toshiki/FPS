using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour {

    //目的地の格納場所
    public Transform[] target;
    //次の目的地の番号
    private int nextPoint = 0;
    //NavMesh
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        Roop();
    }
    void Update()
    {
        if(agent.remainingDistance < 0.5f)
            Roop();
    }

    void Roop()
    {
        if(target.Length == 0)
        {
            return;
        }

        agent.destination = target[nextPoint].position;

        nextPoint = (nextPoint + 1) % target.Length;
    }
}
