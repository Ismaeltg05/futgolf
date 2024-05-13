using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreTable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _PlayerScoresText = new TextMeshProUGUI[4];
    [SerializeField] private TextMeshProUGUI roundLabel;
    [SerializeField] private TurnManager _TurnManager;



    private void Update()
    {
        for (int i = 0; i < _TurnManager.numberOfPlayers(); i++)
        {
            if  (i == _TurnManager.PlayerIndex())
            {
                _PlayerScoresText[i].text = _TurnManager.playersName[i] + ": " + _TurnManager.getNthPlayerPoints(i).ToString();
                _PlayerScoresText[i].color = Color.red;
            }
            else
            {
                _PlayerScoresText[i].text = _TurnManager.playersName[i] + ": " + _TurnManager.getNthPlayerPoints(i).ToString();
                _PlayerScoresText[i].color = Color.white;
            }

        }

    }

    public void setRoundTo(int round)
    {
        roundLabel.text = "Round " + round.ToString();
    }
    public string getWinner()
    {
        int greater = 0;
        int WinnerIndex = 0;
        for (int i = 0; i < _TurnManager.getPoints().Length; i++)
        {
            if(_TurnManager.getPoints()[i] >= greater)
            {
                greater = _TurnManager.getPoints()[i];
                WinnerIndex = i;
            }
        }
       
        return _TurnManager.getPlayer(WinnerIndex);
    }
    
    
}
