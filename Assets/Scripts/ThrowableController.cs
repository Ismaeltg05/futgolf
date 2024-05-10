using UnityEngine;

public class ThrowableController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    ParticleSystem ballParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        ballParticleSystem = GetComponent<ParticleSystem>();
        Launch();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Ground"))
        {
            transform.LookAt(Camera.main.transform);
            ballParticleSystem.Emit(50);
            Invoke("Launch", Random.Range(0f, 2f));
        }
    }

    private void Launch()
    {
        transform.position = Camera.main.transform.position + Vector3.up * 5 + Vector3.right * Random.Range(-10, 10);
        rb.velocity = Vector3.zero;

        rb.AddForce(Camera.main.transform.forward * 10, ForceMode.Impulse);
    }
}
