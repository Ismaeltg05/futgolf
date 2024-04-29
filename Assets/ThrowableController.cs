using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MainMenuGoalController mainMenuGoalController;
    // Start is called before the first frame update
    void Start()
    {
        Launch();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            mainMenuGoalController.generateParticles(transform.position, -rb.velocity);
            Launch();
        }
    }

    private void Launch()
    {
        transform.position= Camera.main.transform.position + Vector3.up * 5 + Vector3.right * Random.Range(-10,10);
        rb.velocity = Vector3.zero;

        rb.AddForce(Camera.main.transform.forward * 10, ForceMode.Impulse);
    }
}