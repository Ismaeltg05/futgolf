using UnityEngine;

public class Effects : MonoBehaviour
{
    private Transform transform; 
    private float y;

   private void Start()
   {
    transform = GetComponent<Transform>();
   }

    private void Update()
    {
        if(Input.GetKey("k"))
        {
            y -= 1;
        }
         if(Input.GetKey("l"))
        {
            y += 1;
        }
        transform.position = new Vector3(y,Ball.position.position.y,Ball.position.position.z+30f);

        transform.Rotate(new Vector3(0,0,0));

    }
}
