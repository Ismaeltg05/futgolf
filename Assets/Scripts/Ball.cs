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
    [SerializeField] private bool barIncreasing = true;

    private SphereRaycast sphereRaycast;

    private ParticleSystem particle;
    [SerializeField] private GameObject End;

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

    
    void Update()
    {

        speed = Mathf.RoundToInt(rbspeed.velocity.magnitude * 3600 /50000);
        
        if(speed < 0.5 && shooted && sphereRaycast.ground)
        {
            rbspeed.constraints = RigidbodyConstraints.FreezeAll;
            turnManager.EndTurn();
            shooted = false;
        }
        if(Input.GetKey(KeyCode.Space) && !shooted)
        { 
            if(barIncreasing)
            {
                force += Time.deltaTime*100;
                slider.value = force;
                if(force == 200)
                {
                    barIncreasing = false;
                }
            }
            else 
            {
                force -= Time.deltaTime;
                slider.value = force;
                if (force <= 0)
                {
                    barIncreasing = true;
                }
            }
        }
        
        if(Input.GetKeyDown("e"))
        {
            if(!shooted)
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
            
            End.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
