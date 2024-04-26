using UnityEngine;

public class Persecutor : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(turnManager.players[0].GetComponent<Transform>().position.x,turnManager.players[0].GetComponent<Transform>().position.y,turnManager.players[0].GetComponent<Transform>().position.z);
    }
}
