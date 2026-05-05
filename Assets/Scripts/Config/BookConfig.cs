

using UnityEngine;

public static class BookConfig
{
    public static BookItem[] GetAllEnemys()
    {
        int size = 64;
        Color purple = new Color(0.5f, 0f, 0.5f);
        return new[]
        {
            new BookItem
            {
                // 紫色方形
                Icon = MyStatic.GenerateSquareTexture(size, purple),
                Name = "普通敌人",
                Description = "基础近战怪物，行动直接纯粹。发现目标后会无脑持续逼近并输出普通近战伤害。面板各项属性较为均衡，但在后期波次中往往数量众多，极易对玩家造成围堵之势。"
            },
            new BookItem
            {
                // 紫色圆形
                Icon = MyStatic.GenerateCircleTexture(size, purple),
                Name = "远程敌人",
                Description = "狡猾的远程攻击者。会有意识地与玩家保持在一定的安全射击距离，驻足后发射子弹，并在攻击后停留休息。一旦被玩家抛下过远，会主动重调走位跟上；身板与防御都较为脆弱。"
            },
            new BookItem
            {
                // 紫色三角形
                Icon = MyStatic.GenerateTriangleTexture(size, purple),
                Name = "巫术敌人",
                Description = "烦人的战场支援法师。善于在游荡中与玩家保持安全的施法距离。它懂得隔空施法，每隔一段时间便会在场上随机挑出一名同伴，为其施加以下之一的强化状态：短时间内生命回复、移速加成，或是防御力提升。"
            },
            new BookItem
            {
                // 紫色方形
                Icon = MyStatic.GenerateSquareTexture(size, purple),
                Name = "巨大领主",
                Description = "犹如叹息之墙般笨重但坚硬的肉盾型首领。其生命条会随波数呈指数级恐怖成长。虽然基础移速缓慢，但若玩家过于靠近，它就有几率突然爆发出双倍移速发动短距离舍身冲锋，拥有极高的近身压迫感。"
            },
            new BookItem
            {
                // 紫色圆形
                Icon = MyStatic.GenerateCircleTexture(size, purple),
                Name = "射手领主",
                Description = "专注且精准掌控弹幕的射手首领。生命值成长极高，但防御偏低。它锁定目标后，会连续释放3波覆盖150度大视角的扇形密集弹幕。在狂轰滥炸之后领主会陷入数秒的虚弱休整期，这是反击的至佳时刻。"
            },
            new BookItem
            {
                // 紫色三角形
                Icon = MyStatic.GenerateTriangleTexture(size, purple),
                Name = "恩赐领主",
                Description = "犹如神明降临般的终极大光环领主。自带不俗的移速和庞大的血量储备，擅长在远距离控制大局。每隔一段时间秒，向场上所有的怪物降下神圣恩赐，随机给予生命回复、速度加成，或者防御力提升。"
            }
        };
    }

    public static BookItem[] GetAllBuffs()
    {
        return new[]
        {
            new BookItem
            {
                Icon = null,
                Name = "生命果实",
                Description = "提升最大生命值 (数值受局外天赋影响)\n可无限叠加\n<color=#E0E0E0>属性说明：增强容错率与生存能力。</color>"
            },
            new BookItem
            {
                Icon = null,
                Name = "力量果实",
                Description = "提升攻击力 (数值受局外天赋影响)\n可无限叠加\n<color=#E0E0E0>属性说明：提升角色造成的所有基础伤害。</color>"
            },
            new BookItem
            {
                Icon = null,
                Name = "坚韧果实",
                Description = "提升防御力 (数值受局外天赋影响)\n可无限叠加\n<color=#E0E0E0>属性说明：减少受到敌人攻击时扣除的生命值。</color>"
            },
            new BookItem
            {
                Icon = null,
                Name = "敏捷果实",
                Description = "+20%点速度\n最多叠加5层\n<color=#E0E0E0>属性说明：提升角色的移动速度，更容易拉扯与躲避敌人的攻击。</color>"
            },
            new BookItem
            {
                Icon = null,
                Name = "暴击果实",
                Description = "+5%点暴击率\n最多叠加5层\n<color=#E0E0E0>属性说明：增加每一次攻击触发暴击（多倍伤害）的概率。</color>"
            },
            new BookItem
            {
                Icon = null,
                Name = "暴伤果实",
                Description = "+25%点暴击伤害\n可无限叠加\n<color=#E0E0E0>属性说明：提升触发暴击时的伤害加成倍率，让暴击收益更高。</color>"
            },
            new BookItem
            {
                Icon = null,
                Name = "攻速果实",
                Description = "+20%点攻速\n最多叠加5层\n<color=#E0E0E0>属性说明：缩短连续发射子弹的间隔时间，显著提升整体DPS。</color>"
            },
            new BookItem
            {
                Icon = null,
                Name = "弹速果实",
                Description = "+20%点弹速\n最多叠加5层\n<color=#E0E0E0>属性说明：提高子弹飞行的速度，降低敌人躲避命中判定的可能。</color>"
            },
            new BookItem
            {
                Icon = null,
                Name = "穿透果实",
                Description = "+1点子弹穿透数\n最多叠加5层\n<color=#E0E0E0>属性说明：子弹击中敌人后不被立刻销毁，能对后方敌人继续造成伤害，应对虫海的利器。</color>"
            },
            new BookItem
            {
                Icon = null,
                Name = "多重果实",
                Description = "+1点子弹发射数\n最多叠加5层\n<color=#E0E0E0>属性说明：每次攻击时会分裂发射更多的子弹，形成大范围火力覆盖。</color>"
            },
            new BookItem
            {
                Icon = null,
                Name = "弹幕果实",
                Description = "+20%点子弹大小\n最多叠加5层\n<color=#E0E0E0>属性说明：增大子弹的模型及碰撞判定范围，更容易擦边命中目标。</color>"
            },
            new BookItem
            {
                Icon = null,
                Name = "持续果实",
                Description = "+1.0秒子弹持续时间\n最多叠加3层\n<color=#E0E0E0>属性说明：延长子弹存在于场上的衰减时间，变相提高子弹的最远射程。</color>"
            },
            new BookItem
            {
                Icon = null,
                Name = "刷新果实",
                Description = "增加商店刷新机会 (数值受局外天赋影响)\n可无限叠加\n<color=#E0E0E0>属性说明：可以在战斗内遇到不想要的果实时消耗次数刷新备选项。</color>"
            }
        };
    }

    public static BookItem[] GetAllHeros()
    {
        int size = 64;
        Color write = Color.white;
        return new[]
        {
            new BookItem
            {
                Icon = MyStatic.GenerateCircleTexture(size, write),
                Name = "射手座",
                Description = "擅长游击与远程压制的射手。初始依靠单发弹幕在安全距离外消灭敌人，面板属性较为均衡。收集叠加各类果实增益，能逐步解锁多重射击、子弹穿透与巨型弹幕等质变能力，最终化身为掌控全场的移动炮台。"
            },
        };
    }
}