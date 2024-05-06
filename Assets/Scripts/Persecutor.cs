using UnityEngine;

public class Persecutor : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;

    [SerializeField] private int ghost; //?????

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = turnManager.GetNthPlayer(ghost).transform.position;
    }
}
