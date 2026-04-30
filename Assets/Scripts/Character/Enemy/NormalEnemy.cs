using UnityEngine;

public class NormalEnemy : Character
{
    Character player;

    private void Start()
    {
        Setup(1);

        player = BattleManager.Instance.playerCharacter;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(moveComponent.MoveSpeed * Time.deltaTime * direction);
        }
    }


    public override void Setup(int level)
    {
        base.Setup(level);

        hpComponent.OnDeath += MyDestroy;
    }


    private void MyDestroy()
    {
        Destroy(this.gameObject);
    }
}
