using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Transform transform; 
    private float y;

    private float z;

   private void Start()
   {
    transform = GetComponent<Transform>();
   }

    private void Update()
    {
        y = Input.GetAxisRaw("Vertical");
        z = Input.GetAxisRaw("Horizontal");
        transform.position = new Vector3(Ball.position.position.x,Ball.position.position.y,Ball.position.position.z-2.5f);
        
        transform.Rotate(new Vector3(0,y,z));
    }
}
