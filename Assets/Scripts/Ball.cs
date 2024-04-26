using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rbspeed;
    public Transform target;

    //public Transform effect;
    private bool shooted = false;

    public static Transform position;

    public static float force;

    [SerializeField] private float speed;

    [SerializeField] private TurnManager turnManager;
    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>();
        rbspeed= GetComponent<Rigidbody>();
        turnManager.StartTurn();
        
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
        
        if(speed < 0.5 && shooted)
        {
            rbspeed.constraints = RigidbodyConstraints.FreezeAll;
            turnManager.EndTurn();
            shooted = false;
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
            shooted = true;
        }
        }

}
