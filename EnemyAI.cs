using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Material mat;
    [SerializeField] private LayerMask whatIsPlayer, whatIsGround;
    private NavMeshAgent agent;

    [Header("AgentSpeed")]
    [SerializeField] private float chaseSpd;
    [SerializeField] private float patrolSpd;

    [Header ("Patrolling")]
    private Vector3 patrolPoint;
    private bool isPatrolPointSet = false;
    [SerializeField] private float patrolDistance;

    [Header ("Interaction with Player")]
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    private bool playerInSightRange, playerInAttackRange;

    //Time to move stuff
    private float timeTillMove;
    private float timeTillMoveCounter = 0f;

    [Header ("FollowPoints")]
    [SerializeField] private Transform[] followPts;
    private int followPointIndex = 0;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        //mat = GetComponent<Material>();
    }

    private void Update() {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) {
            mat.color = Color.green;
            Patrol();
        }
        if (playerInSightRange && !playerInAttackRange) {
            mat.color = Color.yellow;
            FollowPlayer();
        }
        if(playerInSightRange && playerInAttackRange) {
            mat.color = Color.red;
            AttackPlayer();
        }
    }

    private void Patrol() {

        agent.speed = patrolSpd;

        if (!isPatrolPointSet) {

            SetFollowPoint(followPointIndex);
            followPointIndex++;

            if (followPointIndex >= followPts.Length) {
                followPointIndex = 0;
            }
        }

        if (isPatrolPointSet) {
            
            Vector3 distanceToPatrolPoint = transform.position - patrolPoint;

            agent.SetDestination(patrolPoint);

            Debug.Log(distanceToPatrolPoint.magnitude);

            if (distanceToPatrolPoint.magnitude < 1f) {
                isPatrolPointSet = false;
            }
        }
    }

    //Set the random point to move to
    private void SetPatrolPoint() {
        float randomX = Random.Range(-patrolDistance, patrolDistance);
        float randomZ = Random.Range(-patrolDistance, patrolDistance);

        patrolPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(transform.position, -transform.up, 2f, whatIsGround))
            isPatrolPointSet = true;
    }

    private void FollowPlayer() {

        agent.speed = chaseSpd;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer() {
        agent.SetDestination(transform.position);

        Vector3 lookAtPlayer = new Vector3(player.position.x, transform.position.y, player.position.z);

        transform.LookAt(lookAtPlayer);
    }

    //We could prob use this to control how frequently it should move
    private bool TimeToMove() {

        timeTillMove = 1f;

        timeTillMoveCounter += Time.deltaTime;

        Debug.Log(timeTillMoveCounter);

        if(timeTillMove <= timeTillMoveCounter) {
            timeTillMoveCounter = 0;
            return true;
        }
        return false;
    }

    private void SetFollowPoint(int ind) {

        patrolPoint = followPts[ind].position;

        if (Physics.Raycast(transform.position, -transform.up, 2f, whatIsGround))
            isPatrolPointSet = true;
    }
}
