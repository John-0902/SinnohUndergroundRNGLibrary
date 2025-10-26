using System.Drawing;

namespace SinnohUndergroundRNGLibrary
{
    public enum MiningObjectType
    {
        SMALL_PRISM_SPHERE = 1,
        SMALL_PALE_SPHERE = 2,
        SMALL_RED_SPHERE = 3,
        SMALL_BLUE_SPHERE = 4,
        SMALL_GREEN_SPHERE = 5,
        LARGE_PRISM_SPHERE = 6,
        LARGE_PALE_SPHERE = 7,
        LARGE_RED_SPHERE = 8,
        LARGE_BLUE_SPHERE = 9,
        LARGE_GREEN_SPHERE = 10,

        OVAL_STONE = 11,
        ODD_KEYSTONE = 12,
        SUN_STONE = 13,
        STAR_PIECE = 14,
        MOON_STONE = 15,
        HARD_STONE = 16,
        THUNDER_STONE = 17,
        EVERSTONE = 18,
        FIRE_STONE = 19,
        WATER_STONE = 20,
        LEAF_STONE = 21,

        NUGGET_UNUSED = 22,

        HELIX_FOSSIL = 23,
        DOME_FOSSIL = 24,
        CLAW_FOSSIL = 25,
        ROOT_FOSSIL = 26,
        OLD_AMBER = 27,

        RARE_BONE = 28,

        REVIVE = 29,
        MAX_REVIVE = 30,
        RED_SHARD = 31,
        BLUE_SHARD = 32,
        YELLOW_SHARD = 33,
        GREEN_SHARD = 34,
        HEART_SCALE = 35,

        ARMOR_FOSSIL = 36,
        SKULL_FOSSIL = 37,

        LIGHT_CLAY = 38,
        IRON_BALL = 39,
        ICY_ROCK = 40,
        SMOOTH_ROCK = 41,
        HEAT_ROCK = 42,
        DAMP_ROCK = 43,

        FLAME_PLATE = 44,
        SPLASH_PLATE = 45,
        ZAP_PLATE = 46,
        MEADOW_PLATE = 47,
        ICICLE_PLATE = 48,
        FIST_PLATE = 49,
        TOXIC_PLATE = 50,
        EARTH_PLATE = 51,
        SKY_PLATE = 52,
        MIND_PLATE = 53,
        INSECT_PLATE = 54,
        STONE_PLATE = 55,
        SPOOKY_PLATE = 56,
        DRACO_PLATE = 57,
        DREAD_PLATE = 58,
        IRON_PLATE = 59,

        ROCK_1 = 60,
        ROCK_2 = 61,
        ROCK_3 = 62,
        ROCK_4 = 63,
        ROCK_5 = 64,
        ROCK_6 = 65,
        ROCK_7 = 66
    };

    public class MiningObject
    {
        public MiningObjectType Type { get; }
        public int Width { get; }
        public int Height { get; }
        public string[] Shape { get; }
        public RotateFlipType ImageRotation { get; set; }

        public MiningObject(MiningObjectType type, int width, int height, string[] shape = null,
            RotateFlipType imageRotation = RotateFlipType.RotateNoneFlipNone)
        {
            Type = type;
            Width = width;
            Height = height;
            Shape = shape;
            ImageRotation = imageRotation;
        }

        public bool TryPlace(int x, int y, bool[,] wall)
        {
            int endX = x + Width;
            int endY = y + Height;

            if (endX > 13 || endY > 10) return false;

            for (int i = x; i < endX; i++)
            {
                for (int j = y; j < endY; j++)
                {
                    if (IsWithinShape(i - x, j - y))
                    {
                        if (wall[j, i]) return false;
                    }
                }
            }

            for (int i = x; i < endX; i++)
            {
                for (int j = y; j < endY; j++)
                {
                    if (IsWithinShape(i - x, j - y))
                    {
                        wall[j, i] = true;
                    }
                }
            }

            return true;
        }

        public bool IsPlate()
            => 44 <= (int)Type && (int)Type <= 59; // fire plate and iron plate

        private bool IsWithinShape(int x, int y)
            => Shape == null || Shape[y][x] != '*';

        public static readonly int[,] ItemWeights = new int[,] // even TID, odd TID
        {
            {22, 30},
            {30, 22},
            {194, 167},
            {167, 194},
            {150, 150},
            {13, 15},
            {15, 13},
            {96, 83},
            {83, 96},
            {75, 75},
            {0, 0},
            {0, 0},
            {1, 4},
            {2, 2},
            {2, 1},
            {2, 1},
            {4, 4},
            {1, 4},
            {4, 4},
            {1, 4},
            {4, 1},
            {2, 1},
            {2, 1},
            {0, 0},
            {0, 0},
            {0, 0},
            {0, 0},
            {0, 0},
            {0, 0},
            {0, 0},
            {0, 0},
            {0, 0},
            {0, 0},
            {0, 0},
            {0, 0},
            {0, 0},
            {0, 0},
            {0, 0},
            {1, 1},
            {1, 1},
            {8, 8},
            {1, 1},
            {13, 13},
            {13, 13},
            {13, 13},
            {13, 13},
            {33, 33},
            {25, 0},
            {0, 25},
            {1, 1},
            {1, 1},
            {1, 2},
            {2, 1},
            {1, 2},
            {2, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},

            // rocks
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
        };

        public static readonly int[,] ItemWeightsNationalDex = new int[,] // even TID, odd TID
        {
            {20, 27},
            {27, 20},
            {164, 110},
            {124, 150},
            {107, 107},
            {10, 13},
            {13, 10},
            {75, 61},
            {61, 75},
            {53, 53},
            {0, 0},
            {2, 2},
            {3, 15},
            {10, 10},
            {8, 1},
            {7, 2},
            {20, 20},
            {5, 30},
            {20, 20},
            {5, 30},
            {30, 5},
            {15, 3},
            {15, 2},
            {1, 3},
            {1, 3},
            {1, 3},
            {1, 3},
            {13, 1},
            {1, 3},
            {1, 3},
            {1, 3},
            {1, 3},
            {3, 1},
            {3, 1},
            {3, 1},
            {3, 1},
            {2, 2},
            {3, 3},
            {5, 5},
            {5, 5},
            {10, 10},
            {2, 2},
            {17, 17},
            {17, 17},
            {17, 17},
            {17, 17},
            {30, 30},
            {12, 0},
            {0, 12},
            {2, 5},
            {5, 2},
            {5, 11},
            {11, 5},
            {5, 11},
            {11, 5},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},
            {1, 1},

            // rocks
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
            //{1, 1},
        };

    }
}
