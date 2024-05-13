using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Winner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private ScoreTable ScoreTable;
    
    void Update()
    {
        textMeshProUGUI.text = ScoreTable.getWinner();
    }
}
