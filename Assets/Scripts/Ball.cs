using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private Rigidbody rbspeed;
    public Transform target;
    //public Transform effect;
    private bool shooted = false;

    public static Transform position;

    public static float force;

    [SerializeField] private float speed;
    [SerializeField] private Slider slider;

    [SerializeField] private TurnManager turnManager;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = Mathf.RoundToInt(rbspeed.velocity.magnitude * 3600 /50000);
        
        if(speed < 0.5 && shooted)
        {
            //turnManager.players[turnManager.currentPlayerIndex].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            rbspeed.constraints = RigidbodyConstraints.FreezeAll;
            turnManager.EndTurn();
            shooted = false;
        }
        if(Input.GetKey(KeyCode.Space))
        { 
            turnManager.players[turnManager.currentPlayerIndex].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            if(force <= 200)
            {
            force += 1;
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

}
