

using GoveKits.Runtime.UI;
using UnityEngine;



public class BookViewModel : ViewModel
{
    public BookItem[] GetAllEnemys()
    {
        return BookConfig.GetAllEnemys();
    }

    public BookItem[] GetAllBuffs()
    {
        return BookConfig.GetAllBuffs();
    }

    public BookItem[] GetAllHeros()
    {
        return BookConfig.GetAllHeros();
    }
}




public class BookItem
{
    public Sprite Icon;
    public string Name;
    public string Description;
}