using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rbspeed;
    public Transform target;

    //public Transform effect;

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
    void Update()
    {
        speed = Mathf.RoundToInt(rbspeed.velocity.magnitude * 3600 /50000);
        
        if(speed < 0.5)
        {
            rbspeed.constraints = RigidbodyConstraints.FreezeAll;
        }
        if(Input.GetKey(KeyCode.Space))
        {
            rbspeed.constraints = RigidbodyConstraints.None;
            if(force <= 200)
            {
            force += 1;
            }
        }
        else if(force > 0)
        {
            force -= 1;
        }
        if(Input.GetKeyDown("e"))
        {
            Shoot();
            //rbspeed.constraints = RigidbodyConstraints.None;
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
