using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    private int currentPlayerIndex = 0;
    public GameObject[] ghost;
    private int[] points;
    public string[] playersName;
    public int turnsCount = 0;
    public ScoreTable scoreTable;




    void Start()
    {
        playersName = new string[players.Length];

        for (int i = 0; i < playersName.Length; i++)
        {
            playersName[i] = players[i].name;
        }

        points = new int[players.Length];

        StartTurn();
    }

    public void StartTurn()
    {
        
        turnsCount++;
        scoreTable.setRoundTo((int)Mathf.Floor(turnsCount / players.Length));
        //Activate current player

        GetCurrentPlayer().SetActive(true);
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

    public GameObject GetCurrentPlayer()
    {
        return players[currentPlayerIndex];
    }

    public GameObject GetNthPlayer(int n)
    {
        return players[n];
    }

    public int numberOfPlayers()
    {
        return players.Length;
    }
    public int PlayerIndex()
    {
        return currentPlayerIndex;
    }

    public int GetCurrentPlayerPoints()
    {
        return points[currentPlayerIndex];
    }

    public int getNthPlayerPoints(int n)
    {
        return points[n];
    }

    public void AddPointsToCurrentPlayer(int n)
    {
        points[currentPlayerIndex] += n;
    }
    public int[] getPoints()
    {
        return points;
    }
    public string getPlayer(int playerIndex)
    {
        return playersName[playerIndex];
    }
}