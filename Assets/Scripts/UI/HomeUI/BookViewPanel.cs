using System.ComponentModel;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class BookViewPanel : ViewPanel<BookViewModel>
{


    protected override void OnDataChanged(object sender, PropertyChangedEventArgs e)
    {
        
    }


    protected override void OnButtonClicked(string btnName)
    {
        switch (btnName)
        {
            case "Back":
                OnBackBtnClicked();
                break;
        }
    }


    private void OnBackBtnClicked()
    {
        Controller.Back();
    }

}
