

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
                Description = "危险的战场支援法师。善于在游荡中与玩家保持一个若即若离的安全判定范围。它懂得隔空施法，每隔数秒便会在场上随机挑出一名同伴，向其施加受自身攻击力强化的狂暴、疾行和回复增益状态。"
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
                Description = "犹如神明降临般的终极大光环领主。自带不俗的移速和庞大的血量储备，擅于在极远距离控制大局。每隔一段时间，就能同时向场上最多5名怪物降下神圣恩赐——提供随自身攻击力成长的巨额伤害、极速与渐愈状态。"
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
                Description = "+5点最大生命值"
            },
            new BookItem
            {
                Icon = null,
                Name = "力量果实",
                Description = "+1点攻击力"
            },
            new BookItem
            {
                Icon = null,
                Name = "坚韧果实",
                Description = "+1点防御力"
            },
            new BookItem
            {
                Icon = null,
                Name = "敏捷果实",
                Description = "+10%点速度\n最多叠加10层"
            },
            new BookItem
            {
                Icon = null,
                Name = "暴击果实",
                Description = "+5%点暴击率\n最多叠加10层"
            },
            new BookItem
            {
                Icon = null,
                Name = "暴伤果实",
                Description = "+25%点暴击伤害"
            },
            new BookItem
            {
                Icon = null,
                Name = "攻速果实",
                Description = "+10%点攻速\n最多叠加10层"
            },
            new BookItem
            {
                Icon = null,
                Name = "弹速果实",
                Description = "+10%点弹速\n最多叠加10层"
            },
            new BookItem
            {
                Icon = null,
                Name = "穿透果实",
                Description = "+1点子弹穿透数\n最多叠加5层"
            },
            new BookItem
            {
                Icon = null,
                Name = "多重果实",
                Description = "+1点子弹发射数\n最多叠加5层"
            },
            new BookItem
            {
                Icon = null,
                Name = "弹幕果实",
                Description = "+20%点子弹大小\n最多叠加5层"
            },
            new BookItem
            {
                Icon = null,
                Name = "持续果实",
                Description = "+0.5秒子弹持续时间\n最多叠加5层"
            },
            new BookItem
            {
                Icon = null,
                Name = "刷新果实",
                Description = "获得5次刷新机会"
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