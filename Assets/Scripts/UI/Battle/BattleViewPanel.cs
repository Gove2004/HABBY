

using System.ComponentModel;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class BattleViewPanel : ViewPanel<BattleViewModel>
{


    protected override void OnDataChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ViewModel.BattleTime):
                TMPTexts["BattleTime"].text = $"{FormatTime(ViewModel.BattleTime)}";
                break;
        }
    }


    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }


    protected override void OnButtonClicked(string btnName)
    {
        switch (btnName)
        {
            
        }
    }





    private void Update()
    {
        ViewModel.UpdateBattle(Time.deltaTime);
    }

}
