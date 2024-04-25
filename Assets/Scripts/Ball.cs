using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rbspeed;
    public Transform target;

    public Transform effect;

    public static Transform position;

    public static float force;

    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>();
        rbspeed= GetComponent<Rigidbody>();
    }
    private void Shoot()
    {
        Vector3 shoot = (target.position - this.transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(shoot * force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = Mathf.RoundToInt(rbspeed.velocity.magnitude * 3600 /50000);
        
        if(speed < 1)
        {
        if(Input.GetKey(KeyCode.Space))
        {
            if(force <= 200)
            {
            force += 1;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Shoot();
            force = 0;
        }
        }
    }
    
    private void OnCollisionEnter (Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if(speed > 0)
            {
            
            }
        }
    }
}
