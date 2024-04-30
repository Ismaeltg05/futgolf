using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _PlayerScoresText = new TextMeshProUGUI[4];
    [SerializeField] private TextMeshProUGUI round;
    [SerializeField] private TurnManager _TurnManager;





    private void Update()
    {
        for (int i = 0; i < _TurnManager.players.Length; i++)
        {
            _PlayerScoresText[i].text = _TurnManager.playersName[i] + ": "+_TurnManager.points[i].ToString();
        }
        round.text =  "Round"+_TurnManager.round.ToString();
    }

}
