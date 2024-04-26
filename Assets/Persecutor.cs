using UnityEngine;

public class Persecutor : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;

    [SerializeField] private int ghost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(turnManager.players[ghost].GetComponent<Transform>().position.x,turnManager.players[ghost].GetComponent<Transform>().position.y,turnManager.players[ghost].GetComponent<Transform>().position.z);
    }
}
