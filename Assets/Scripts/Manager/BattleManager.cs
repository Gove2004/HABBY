using System.Collections.Generic;
using GoveKits.Runtime.Core;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    public Character playerCharacter { get; private set; }
    private List<Character> enemyCharacters = new List<Character>();


    public void RegisterPlayer(Character player)
    {
        playerCharacter = player;
    }

    
}
