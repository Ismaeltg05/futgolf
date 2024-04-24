using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform target;

    public static Transform position;

    public static float force;

    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>();
    }
    private void Shoot()
    {
        Vector3 shoot = (target.position - this.transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(shoot * force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(force <= 100)
            {
            force += 1;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
             Shoot();
             force = 0;
        }
    }
}
