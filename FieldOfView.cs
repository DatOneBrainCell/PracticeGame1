using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float viewRadius;
    [SerializeField] private float viewAngle;

    [SerializeField] private GameObject player;

    [SerializeField] private LayerMask whatIsPlayer, whatIsObstacle;

    private bool canSeePlayer;

    private void Start() {
        player = GameObject.Find("Player");

        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine() {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true) {
            yield return null;
            FOVCheck();
        }
    }

    private void FOVCheck() {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, viewRadius, whatIsPlayer);

        if (colliderArray.Length > 0) {
            Transform target = colliderArray[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2) {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, whatIsObstacle))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}
