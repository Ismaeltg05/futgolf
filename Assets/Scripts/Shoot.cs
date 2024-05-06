using UnityEngine;

public class Shoot : MonoBehaviour
{ 
    private float y;
    private float z;

    [SerializeField] private TurnManager turnManager;

    private void Update()
    {
        y = Input.GetAxisRaw("Vertical");
        z = Input.GetAxisRaw("Horizontal");

        Vector3 playerPos = turnManager.GetCurrentPlayer().transform.position;

        transform.position = new Vector3(playerPos.x,playerPos.y+2,playerPos.z);
        
        transform.Rotate(new Vector3(0,y,z));
        //transform.rotation = Quaternion.Euler(90,y,z);
        
    }
}
