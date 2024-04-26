using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject[] players;
    public int currentPlayerIndex = 0;
    public GameObject[] camera;

    void Start()
    {
        StartTurn();
    }

    public void StartTurn()
    {
        //Activate current player
        //players[currentPlayerIndex].SetActive(true);
        camera[currentPlayerIndex].SetActive(true);
    }

    public void EndTurn()
    {
        //Deactivate current player
        //players[currentPlayerIndex].SetActive(false);
        camera[currentPlayerIndex].SetActive(false);

        //Move to next player
        currentPlayerIndex=(currentPlayerIndex+1)%players.Length;

        //Start next turn
        StartTurn();
    }
}
