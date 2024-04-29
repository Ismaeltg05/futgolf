using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private Rigidbody rbspeed;
    public Transform target;
    //public Transform effect;
    private bool shooted = false;

    public static Transform position;

    public float force;

    [SerializeField] private float speed;
    [SerializeField] private Slider slider;

    [SerializeField] private TurnManager turnManager;
    [SerializeField] private bool charge;

    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>();
        rbspeed= GetComponent<Rigidbody>();
        slider.maxValue = 200;
        turnManager.StartTurn();
        rbspeed.constraints = RigidbodyConstraints.FreezeAll;

        

    }
    private void Shoot()
    {
        Vector3 shoot = (target.position - this.transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(shoot * force, ForceMode.Impulse);
    }

    
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
            if (charge == false)
            {
                force += 1;
                slider.value = force;
                if (force == 200)
                {
                    charge = true;
                }
            }
            else
            {
                force -= 1;
                slider.value = force;
                if (force <= 0)
                {
                    charge = false;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
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
                    slider.value = 0;
                }
            }
    }

}
