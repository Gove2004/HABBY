using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public LevelComponent levelComponent;
    public HPComponent hpComponent;
    public RecoverComponent recoverComponent;
    public AttackComponent attackComponent;
    public DefenseComponent defenseComponent;
    public MoveComponent moveComponent;

    public virtual void Setup(int level)
    {
        levelComponent = new LevelComponent(level);
        hpComponent = new HPComponent(5);
        recoverComponent = new RecoverComponent(0);
        attackComponent = new AttackComponent(2, 1.0f, 0.0f, 2.0f);
        defenseComponent = new DefenseComponent(0);
        moveComponent = new MoveComponent(2.5f);
    }

    protected virtual void Update()
    {
        recoverComponent.Update(Time.deltaTime);
    }
}
