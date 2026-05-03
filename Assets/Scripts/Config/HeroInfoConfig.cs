


using System.Collections.Generic;

public static class HeroInfoConfig
{
    public static readonly Dictionary<string, List<(int value, int price)>> ActorInfo = new()
    {
        {
            "生命", new List<(int value, int price)>()
            {
                (5, 1), (6, 10), (7, 50), (8, 250), (9, 1000), (10, 5000)
            }
        },
        {
            "攻击", new List<(int value, int price)>()
            {
                (1, 1), (2, 10), (3, 50), (4, 250), (5, 1000), (6, 5000)
            }
        },
        {
            "防御", new List<(int value, int price)>()
            {
                (0, 1), (1, 10), (2, 50), (3, 250), (4, 1000), (5, 5000)
            }
        },
        {
            "暴击", new List<(int value, int price)>()
            {
                (0, 1), (5, 10), (10, 50), (15, 250), (20, 1000), (25, 5000)
            }
        },
        {
            "暴伤", new List<(int value, int price)>()
            {
                (0, 1), (10, 10), (20, 50), (30, 250), (40, 1000), (50, 5000)
            }
        },
        {
            "攻速", new List<(int value, int price)>()
            {
                (0, 1), (5, 10), (10, 50), (15, 250), (20, 1000), (25, 5000)
            }
        },
    };
}