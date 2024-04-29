using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject[] players;
    public int currentPlayerIndex = 0;
    public GameObject[] ghost;

    void Start()
    {
        StartTurn();
    }

    public void StartTurn()
    {
        //Activate current player
        players[currentPlayerIndex].SetActive(true);
        ghost[currentPlayerIndex].SetActive(false);
    }

    public void EndTurn()
    {
        //Deactivate current player
        players[currentPlayerIndex].SetActive(false);
        ghost[currentPlayerIndex].SetActive(true);

        //Move to next player
        currentPlayerIndex=(currentPlayerIndex+1)%players.Length;

        //Start next turn
        StartTurn();
    }
}
