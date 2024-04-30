using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject[] players;
    public int currentPlayerIndex = 0;
    public GameObject[] ghost;
    public int[] points;
    public string[] playersName ;
    public int turnsCount = 0;
    public ScoreTable scoreTable;



    void Start()
    {
        playersName =new string[]{ "a","AA","aaa","AAAAAAA"};
        
        points = new int[players.Length];

        StartTurn();
    }

    public void StartTurn()
    {
        turnsCount++;
        scoreTable.setRoundTo((int)Mathf.Floor(turnsCount/players.Length));
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
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        StartTurn();

    }
}