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
    private bool charge = false;

    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>();
        rbspeed= GetComponent<Rigidbody>();
        turnManager.StartTurn();
        rbspeed.constraints = RigidbodyConstraints.FreezeAll;
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
        
        if(speed < 0.5 && shooted)
        {
            rbspeed.constraints = RigidbodyConstraints.FreezeAll;
            turnManager.EndTurn();
            shooted = false;
        }
        if(Input.GetKey(KeyCode.Space))
        { 
            if(charge == false)
            {
                force += 1;
                if(force == 200)
                {
                    charge = true;
                }
            }
            else if (charge ==true)
            {
                force -=1;
                if(force <= 0)
                {
                    charge = false;
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            force = force;
        }
        if(Input.GetKey("e"))
            {
                if(shooted == false)
                {
                    turnManager.players[turnManager.currentPlayerIndex].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    Shoot();
                    shooted = true;
                    force = 0;
                }
            }
    }

}
