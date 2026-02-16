using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent agent;

    RaycastHit hit;
    Ray ray;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                agent.SetDestination(hit.point);
            }
        }
    }
}
