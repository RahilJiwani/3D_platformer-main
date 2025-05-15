using UnityEngine;
using UnityEngine.AI;
public class EnemyFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public NavMeshAgent enemy; // the enemy's NavMeshAgent component
    public Transform player; // the player's transform

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.position); // sets the enemy's destination to the player's position
    }
}
