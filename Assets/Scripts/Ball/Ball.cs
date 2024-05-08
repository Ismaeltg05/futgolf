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

    private State state = State.Idle;
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

                Vector2 parablePosition2d = new Vector2(
                    launchForce * Mathf.Cos(launchPitch) * paraboleTime,
                    launchForce * Mathf.Sin(launchPitch) * paraboleTime - 0.5f * - Physics.gravity.y * Mathf.Pow(paraboleTime, 2)
                    );
                Vector2 vDirection = new Vector2(
                       launchForce * Mathf.Cos(launchPitch),
                       launchForce * Mathf.Sin(launchPitch) - (-Physics.gravity.y * paraboleTime)
                    
                                                                             );

                Vector3 yRotatedParablePosition3d = new Vector3(parablePosition2d.x * Mathf.Sin(launchYaw) , parablePosition2d.y, parablePosition2d.x * Mathf.Cos(launchYaw)) + launchPosition;
                

                transform.position = yRotatedParablePosition3d;

                paraboleTime += Time.deltaTime;

                if (vDirection.y<0 && Physics.Raycast(transform.position, Vector3.down,vDirection.y+10))
                {
                    rb.useGravity = true;
                    state = State.Moving;
                    rb.velocity = new Vector3(vDirection.x * Mathf.Sin(launchYaw), vDirection.y, vDirection.x * Mathf.Cos(launchYaw));
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
