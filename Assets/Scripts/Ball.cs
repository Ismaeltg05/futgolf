using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

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
    private bool charge = false;

    private SphereRaycast sphereRaycast;

    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>();
        rbspeed= GetComponent<Rigidbody>();
        sphereRaycast = GetComponent<SphereRaycast>();
        turnManager.StartTurn();
        slider.maxValue = 200;
        rbspeed.constraints = RigidbodyConstraints.FreezeAll;

        particle = GetComponent<ParticleSystem>();

    }
    private void Shoot()
    {
        Vector3 shoot = (target.position - this.transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(shoot * force, ForceMode.Impulse);
        turnManager.AddPointsToCurrentPlayer(10);
    }

    
    void FixedUpdate()
    {

        speed = Mathf.RoundToInt(rbspeed.velocity.magnitude * 3600 /50000);
        
        if(speed < 0.5 && shooted && sphereRaycast.ground)
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
                slider.value = force;
                if(force == 200)
                {
                    charge = true;
                }
            }
            else if (charge ==true)
            {
                force -=1;
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
        if(Input.GetKeyDown("e"))
        {
            if(shooted == false)
            {
                turnManager.GetCurrentPlayer().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                Shoot();
                shooted = true;
                force = 0;
                slider.value = force;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hole")
        {
            particle.Emit(100);
        }
    }
}
