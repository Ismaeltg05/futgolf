using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    public Transform target;
    //public Transform effect;
    private bool shooted = false;

    public static Transform position;

    [SerializeField] private float launchForce = 0;
    [SerializeField] private float launchPitch = 2;
    [SerializeField] private float launchRoll = 0;
    [SerializeField] private float launchYaw = 0;
    [SerializeField] private float paraboleTime = 0;
    [SerializeField] private Vector3 launchPosition;

    [SerializeField] private float speed;
    [SerializeField] private Slider slider;

    [SerializeField] private TurnManager turnManager;
    [SerializeField] private bool barIncreasing = true;

    private ParticleSystem particle;
    [SerializeField] private GameObject endScreen;

    private float stoppedTime = 1;
    [SerializeField] private LayerMask floorLayer;

    enum State
    {
        Idle,
        Parabolic,
        Moving
    }

    [SerializeField]  private State state = State.Idle;
    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        turnManager.StartTurn();
        slider.maxValue = 200;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        particle = GetComponent<ParticleSystem>();

    }
    private void Shoot()
    {
        state = State.Parabolic;
        turnManager.AddPointsToCurrentPlayer(10);
        launchPosition = transform.position;
        paraboleTime = 0;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }


    void Update()
    {

        speed = Mathf.RoundToInt(rb.velocity.magnitude * 3600 / 50000);

        if (shooted && state == State.Moving && speed < 0.1 && Physics.Raycast(transform.position, Vector3.down, 15f))
        {
            if (stoppedTime <= 0)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
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

        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward,Time.deltaTime * 60f,Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward,- Time.deltaTime * 60f, Space.Self);
        }


        if (Input.GetKey(KeyCode.Space) && !shooted)
        {
            if (barIncreasing)
            {
                launchForce += Time.deltaTime * 100;
                slider.value = launchForce;
                if (launchForce >= slider.maxValue)
                {
                    barIncreasing = false;
                }
            }
            else
            {
                launchForce -= Time.deltaTime * 100;
                slider.value = launchForce;
                if (launchForce <= 0)
                {
                    barIncreasing = true;
                }
            }
        }

        if (Input.GetKeyDown("e"))
        {
            if (!shooted)
            {
                turnManager.GetCurrentPlayer().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                Shoot();
                shooted = true;
            }
        }

        switch (state)
        {
            case State.Parabolic:
                paraboleTime += Time.deltaTime;


                Vector2 v0 = new Vector2(-launchForce * Mathf.Cos(launchPitch), launchForce * Mathf.Sin(launchPitch));
                Vector2 v = new Vector2(v0.x,v0.y + Physics.gravity.y * paraboleTime);

                transform.Translate(new Vector3(0f,v.y,v.x) * Time.deltaTime);
                
                if(Physics.Raycast(transform.position, new Vector3(0f, v.y, v.x), 5f))
                {
                    state = State.Moving;
                    rb.velocity = rb.velocity = transform.TransformDirection(new Vector3(0f, v.y, v.x));
                    rb.useGravity = true;
                }

                break;
            default:
                break;
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
