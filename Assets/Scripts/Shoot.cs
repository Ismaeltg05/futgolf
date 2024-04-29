using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Transform transform; 
    private float y;

    private float z;
    [SerializeField] private TurnManager turnManager;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        y = Input.GetAxisRaw("Vertical");
        z = Input.GetAxisRaw("Horizontal");
        transform.position = new Vector3(turnManager.players[turnManager.currentPlayerIndex].GetComponent<Transform>().position.x,turnManager.players[turnManager.currentPlayerIndex].GetComponent<Transform>().position.y+2,turnManager.players[turnManager.currentPlayerIndex].GetComponent<Transform>().position.z);
        
        transform.Rotate(new Vector3(0,y,z));
        //transform.rotation = Quaternion.Euler(90,y,z);
        
    }
}
