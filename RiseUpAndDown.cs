using NUnit.Framework;
using UnityEngine;

public class RiseUpAndDown : MonoBehaviour
{
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private float moveDist = 1f;

    private float countdown;

    void Start()
    {
        
    }

    void Update()
    {
        UpAndDown();
    }

    private void UpAndDown() {
        if(CanMove()) {
            transform.position = new Vector3(transform.position.x, moveDist, transform.position.z);
        }

        if(CanMove() && transform.position.y == -moveDist) {
            transform.position = new Vector3(transform.position.x, -moveDist, transform.position.z);
        }
    }

    private bool CanMove() {
        countdown -= Time.deltaTime;
        if(countdown < 0) {
            countdown = cooldown;
            return true;
        }
        return false;
    }

    private void OnDrawGizmos() {
            Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
