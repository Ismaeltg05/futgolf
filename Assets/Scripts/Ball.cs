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

    private ParticleSystem particle;
    [SerializeField] private GameObject endScreen;

    private float stoppedTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>();
        rbspeed= GetComponent<Rigidbody>();
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
        
        if(shooted && speed < 0.2 && Physics.Raycast(transform.position,Vector3.down,1f))
        {
            if(stoppedTime <= 0 )
            {
                rbspeed.constraints = RigidbodyConstraints.FreezeAll;
                turnManager.EndTurn();
                shooted = false;

                stoppedTime = 1;
            }
            else
            { 
                stoppedTime -= Time.deltaTime;
            }
            
        }
        else
        {
           stoppedTime = 1;
        }


        if(Input.GetKey(KeyCode.Space) && !shooted)
        { 
            if(barIncreasing)
            {
                force += Time.deltaTime * 100;
                slider.value = force;
                if(force == 200)
                {
                    barIncreasing = false;
                }
            }
            else 
            {
                force -= Time.deltaTime * 100;
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
            
            endScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
