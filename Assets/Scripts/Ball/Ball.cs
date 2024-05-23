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
    [SerializeField] private GoalGeneratorController goalGeneratorController;
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
    [SerializeField] private static LayerMask floorLayer;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int LRpps = 5; //points per second
    [SerializeField] private int LRTime = 20;
    private int lineRendPoints;

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

        lineRendPoints = LRpps * LRTime;
        lineRenderer.positionCount = lineRendPoints;

        launchPosition = transform.position;

    }
    private void Shoot()
    {
        
        state = State.Parabolic;
        launchPosition = transform.position;
        paraboleTime = 0;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }


    void Update()
    {
        var points = new Vector3[lineRendPoints];

        for (int i = 0; i < lineRendPoints; i++)
        {
            float t = i / (float)LRpps;
            points[i] = launchPosition + transform.TransformDirection(ParablePosAtT(t));
        }
        lineRenderer.SetPositions(points);

        speed = Mathf.RoundToInt(rb.velocity.magnitude * 3600 / 50000);

        if (shooted && state == State.Moving && speed < 0.1 && Physics.Raycast(transform.position, Vector3.down, 15f))
        {
            if (stoppedTime <= 0)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                turnManager.EndTurn();
                slider.value = 0;
                launchPosition = transform.position;
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

        lineRenderer.enabled = !shooted;

        if (Input.GetKey(KeyCode.A) && launchRoll < 80) {
            launchRoll += Time.deltaTime * 60f;
        }
        if (Input.GetKey(KeyCode.D) && launchRoll > -80)
        {
            launchRoll -= Time.deltaTime * 60f;
        }

        if (!shooted) { 
            transform.localEulerAngles = new Vector3(0, -angleFromY(transform.position, Camera.main.transform.position) - 90f, launchRoll);
        }

        if (Input.GetKey(KeyCode.W) && launchPitch < Mathf.PI / 2)
        {
            launchPitch += Time.deltaTime * 0.5f;
        }
        if (Input.GetKey(KeyCode.S) && launchPitch > 0)
        {
            launchPitch -= Time.deltaTime * 0.5f;
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

                transform.position = launchPosition + transform.TransformDirection(ParablePosAtT(paraboleTime));

                Vector3 dir = (ParablePosAtT(paraboleTime + Time.deltaTime) - ParablePosAtT(paraboleTime)).normalized;


                if (Physics.Raycast(transform.position,dir, 5f))
                {
                    state = State.Moving;
                    rb.velocity = transform.TransformDirection(dir) * launchForce;
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
            
            turnManager.AddPointsToCurrentPlayer(10);
            goalGeneratorController.changeGoalPosition();
        }
    }

    private Vector3 ParablePosAtT(float t)
    {
        Vector2 v0 = new Vector2(launchForce * Mathf.Cos(launchPitch), launchForce * Mathf.Sin(launchPitch));
       
        return new Vector3(
            0,
            v0.y * t + 0.5f* Physics.gravity.y * Mathf.Pow(t,2),
            v0.x * t
            );
    }

    private float angleFromY(Vector3 a, Vector3 b)
    {
        Vector3 v = b - a;
        return Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
    }
  

}
