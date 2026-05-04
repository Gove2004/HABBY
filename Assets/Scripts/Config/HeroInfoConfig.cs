


using System.Collections.Generic;

public static class HeroInfoConfig
{
    public static readonly Dictionary<string, List<(float value, long price)>> ActorInfo = new()
    {
        {
            PlayerAttributeType.MaxHP, new List<(float value, long price)>()
            {
                (5f, 1), (6f, 5), (7f, 25), (8f, 100), (9f, 500), (10f, 1000),
                (12f, 2500), (14f, 5000), (16f, 10000), (18f, 50000), (20f, 100000),
                (25f, 250000), (30f, 500000), (35f, 1000000), (40f, 5000000), (50f, 10000000),
                (60f, 25000000), (70f, 50000000), (80f, 100000000), (90f, 500000000), (100f, 1000000000)
            }
        },
        {
            PlayerAttributeType.AttackPower, new List<(float value, long price)>()
            {
                (1f, 1), (2f, 5), (3f, 25), (4f, 100), (5f, 500), (6f, 1000),
                (6f, 2500), (7f, 5000), (8f, 10000), (9f, 50000), (10f, 100000),
                (11f, 250000), (12f, 500000), (13f, 1000000), (14f, 5000000), (15f, 10000000),
                (16f, 25000000), (17f, 50000000), (18f, 100000000), (19f, 500000000), (20f, 1000000000)
            }
        },
        {
            PlayerAttributeType.DefensePower, new List<(float value, long price)>()
            {
                (0f, 1), (1f, 10), (2f, 50), (3f, 250), (4f, 1000), (5f, 5000),
                (6f, 25000), (7f, 100000), (8f, 500000), (9f, 2500000), (10f, 10000000)
            }
        },
        {
            PlayerAttributeType.CritRate, new List<(float value, long price)>()
            {
                (0.05f, 10), (0.10f, 50), (0.15f, 250), (0.20f, 1000), (0.25f, 5000), 
                (0.30f, 25000), (0.35f, 100000), (0.40f, 500000), (0.45f, 2500000), (0.50f, 10000000)
            }
        },
        {
            PlayerAttributeType.CritDamageMultiplier, new List<(float value, long price)>()
            {
                (1.50f, 10), (1.55f, 50), (1.60f, 250), (1.65f, 1000), (1.70f, 5000), 
                (1.75f, 25000), (1.80f, 100000), (1.85f, 500000), (1.90f, 2500000), (2.00f, 10000000)
            }
        },
        {
            PlayerAttributeType.AttackSpeed, new List<(float value, long price)>()
            {
                (1.0f, 10), (1.05f, 50), (1.1f, 250), (1.15f, 1000), (1.2f, 5000), 
                (1.25f, 25000), (1.3f, 100000), (1.35f, 500000), (1.4f, 2500000), (1.5f, 10000000)
            }
        },
        {
            PlayerAttributeType.MoveSpeed, new List<(float value, long price)>()
            {
                (1.0f, 10), (1.05f, 50), (1.1f, 250), (1.15f, 1000), (1.2f, 5000), 
                (1.25f, 25000), (1.3f, 100000), (1.35f, 500000), (1.4f, 2500000), (1.5f, 10000000)
            }
        },
        {
            PlayerAttributeType.ExpRate, new List<(float value, long price)>()
            {
                (1.00f, 10), (1.10f, 50), (1.20f, 250), (1.30f, 1000), (1.40f, 5000), 
                (1.50f, 25000), (1.60f, 100000), (1.70f, 500000), (1.80f, 2500000), (2.00f, 10000000)
            }
        },
        {
            PlayerAttributeType.GoldRate, new List<(float value, long price)>()
            {
                (1.00f, 10), (1.10f, 50), (1.20f, 250), (1.30f, 1000), (1.40f, 5000), 
                (1.50f, 25000), (1.60f, 100000), (1.70f, 500000), (1.80f, 2500000), (2.00f, 10000000)
            }
        },
        {
            PlayerAttributeType.RefreshTime, new List<(float value, long price)>()
            {
                (5, 1), (10, 10), (15, 50), (20, 250), (25, 1000), (30, 5000),
                (35, 25000), (40, 100000), (45, 500000), (50, 2500000), (60, 10000000),
                (70, 50000000), (80, 250000000), (90, 1000000000), (100, 2000000000)
            }
        },
        {
            PlayerAttributeType.HealPerFives, new List<(float value, long price)>()
            {
                (0, 5), (1, 25), (2, 125), (3, 625), (4, 3125), (5, 15625),
                (6, 78125), (7, 390625), (8, 1953125), (9, 9765625), (10, 48828125)
            }
        },
        {
            PlayerAttributeType.BulletSpeed, new List<(float value, long price)>()
            {
                (1.0f, 1), (1.1f, 10), (1.2f, 50), (1.3f, 250), (1.4f, 1000), (1.5f, 5000),
                (1.6f, 25000), (1.7f, 100000), (1.8f, 500000), (1.9f, 2500000), (2.0f, 10000000),
                (2.1f, 25000000), (2.2f, 100000000), (2.3f, 500000000), (2.4f, 1000000000), (2.5f, 2000000000)
            }
        },
        {
            PlayerAttributeType.BulletSize, new List<(float value, long price)>()
            {
                (1.00f, 1), (1.05f, 10), (1.10f, 50), (1.15f, 250), (1.20f, 1000), (1.25f, 5000),
                (1.30f, 25000), (1.35f, 100000), (1.40f, 500000), (1.45f, 2500000), (1.50f, 10000000),
            }
        },
        {
            PlayerAttributeType.BulletLife, new List<(float value, long price)>()
            {
                (1.0f, 1), (1.1f, 10), (1.2f, 50), (1.3f, 250), (1.4f, 1000), (1.5f, 5000),
                (1.6f, 25000), (1.7f, 100000), (1.8f, 500000), (1.9f, 2500000), (2.0f, 10000000),
            }
        },
        {
            PlayerAttributeType.BulletCount, new List<(float value, long price)>()
            {
                (1, 100), (2, 5000), (3, 100000), (4, 5000000), (5, 100000000),
            }
        },
        {
            PlayerAttributeType.BulletPierce, new List<(float value, long price)>()
            {
                (0, 100), (1, 5000), (2, 100000), (3, 5000000), (4, 100000000),
            }
        },
        {
            PlayerAttributeType.MoreHPFurits, new List<(float value, long price)>()
            {
                (5f, 10), (6f, 50), (7f, 100), (8f, 500), (9f, 1000), (10f, 5000),
                (11f, 10000), (12f, 50000), (13f, 100000), (14f, 500000), (15f, 1000000),
                (16f, 2500000), (17f, 5000000), (18f, 10000000), (19f, 25000000), (20f, 50000000),
                (21f, 100000000), (22f, 250000000), (23f, 500000000), (24f, 1000000000), (25f, 2000000000)
            }
        },
        {
            PlayerAttributeType.MoreATKFurits, new List<(float value, long price)>()
            {
                (1f, 50), (2f, 500), (3f, 5000), (4f, 50000), (5f, 500000), (6f, 5000000),
                (7f, 50000000), (8f, 500000000), (9f, 1000000000), (10f, 2000000000)
            }
        },
        {
            PlayerAttributeType.MoreDEFFurits, new List<(float value, long price)>()
            {
                (1f, 100), (2f, 10000), (3f, 1000000), (4f, 10000000), (5f, 2000000000)
               
            }
        },
        {
            PlayerAttributeType.MoreRefreshFurits, new List<(float value, long price)>()
            {
                (3f, 1), (4f, 10), (5f, 50), (6f, 250), (7f, 1000), (8f, 5000),
                (9f, 25000), (10f, 100000), (11f, 500000), (12f, 2500000), (13f, 10000000),
                (14f, 25000000), (15f, 100000000), (16f, 500000000), (17f, 1000000000), (18f, 2000000000)
            }
        }
    };
}