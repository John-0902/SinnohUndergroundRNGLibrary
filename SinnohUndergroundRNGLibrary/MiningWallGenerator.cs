using PokemonPRNG.LCG64;
using SinnohUndergroundRNGLibrary.Util;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SinnohUndergroundRNGLibrary
{
    public class MiningWallResult
    {
        public ulong HeadSeed { get; }
        public ulong TailSeed { get; }
        public readonly List<(int x, int y, MiningObject treasure)> Treasures;
        public readonly List<(int x, int y, MiningObject rock)> Rocks;
        public readonly int[,] Cover;

        public MiningWallResult(ulong headSeed, ulong tailSeed, List<(int, int, MiningObject)> treasures, List<(int, int, MiningObject)> rocks, int[,] cover)
        {
            HeadSeed = headSeed;
            TailSeed = tailSeed;
            Treasures = treasures;
            Rocks = rocks;
            Cover = cover;
        }
    }

    public class MiningWallGenerator
    {
        private static readonly MiningObject[] Treasures = new MiningObject[]
        {
            new MiningObject(MiningObjectType.SMALL_PRISM_SPHERE, 2, 2),
            new MiningObject(MiningObjectType.SMALL_PALE_SPHERE, 2, 2),
            new MiningObject(MiningObjectType.SMALL_RED_SPHERE, 2, 2),
            new MiningObject(MiningObjectType.SMALL_BLUE_SPHERE, 2, 2),
            new MiningObject(MiningObjectType.SMALL_GREEN_SPHERE, 2, 2),
            new MiningObject(MiningObjectType.LARGE_PRISM_SPHERE, 3, 3),
            new MiningObject(MiningObjectType.LARGE_PALE_SPHERE, 3, 3),
            new MiningObject(MiningObjectType.LARGE_RED_SPHERE, 3, 3),
            new MiningObject(MiningObjectType.LARGE_BLUE_SPHERE, 3, 3),
            new MiningObject(MiningObjectType.LARGE_GREEN_SPHERE, 3, 3),
            new MiningObject(MiningObjectType.OVAL_STONE, 3, 3),
            new MiningObject(MiningObjectType.ODD_KEYSTONE, 4, 4),
            new MiningObject(MiningObjectType.SUN_STONE, 3, 3,
                new string[]
                {
                    "*x*",
                    "xxx",
                    "xxx",
                }),
            new MiningObject(MiningObjectType.STAR_PIECE, 3, 3,
                new string[]
                {
                    "*x*",
                    "xxx",
                    "*x*",
                }),
            new MiningObject(MiningObjectType.MOON_STONE, 4, 2,
                new string[]
                {
                    "*xxx",
                    "xxx*",
                }),
            new MiningObject(MiningObjectType.MOON_STONE, 2, 4,
                new string[]
                {
                    "x*",
                    "xx",
                    "xx",
                    "*x",
                }, RotateFlipType.Rotate90FlipNone),
            new MiningObject(MiningObjectType.HARD_STONE, 2, 2),
            new MiningObject(MiningObjectType.THUNDER_STONE, 3, 3,
                new string[]
                {
                    "*xx",
                    "xxx",
                    "xx*",
                }),
            new MiningObject(MiningObjectType.EVERSTONE, 4, 2),
            new MiningObject(MiningObjectType.FIRE_STONE, 3, 3),
            new MiningObject(MiningObjectType.WATER_STONE, 3, 3,
                new string[]
                {
                    "xxx",
                    "xxx",
                    "xx*",
                }),
            new MiningObject(MiningObjectType.LEAF_STONE, 3, 4,
                new string[]
                {
                    "*x*",
                    "xxx",
                    "xxx",
                    "*x*",
                }),
            new MiningObject(MiningObjectType.LEAF_STONE, 4, 3,
                new string[]
                {
                    "*xx*",
                    "xxxx",
                    "*xx*",
                }, RotateFlipType.Rotate90FlipNone),
            new MiningObject(MiningObjectType.HELIX_FOSSIL, 4, 4,
                new string[]
                {
                    "*xxx",
                    "xxxx",
                    "xxxx",
                    "xxx*",
                }),
            new MiningObject(MiningObjectType.HELIX_FOSSIL, 4, 4,
                new string[]
                {
                    "xxx*",
                    "xxxx",
                    "xxxx",
                    "*xxx",
                }, RotateFlipType.Rotate90FlipNone),
            new MiningObject(MiningObjectType.HELIX_FOSSIL, 4, 4,
                new string[]
                {
                    "*xxx",
                    "xxxx",
                    "xxxx",
                    "xxx*",
                }),
            new MiningObject(MiningObjectType.HELIX_FOSSIL, 4, 4,
                new string[]
                {
                    "xxx*",
                    "xxxx",
                    "xxxx",
                    "*xxx",
                }, RotateFlipType.Rotate90FlipNone),
            new MiningObject(MiningObjectType.DOME_FOSSIL, 5, 4,
                new string[]
                {
                    "xxxxx",
                    "xxxxx",
                    "xxxxx",
                    "*xxx*",
                }),
            new MiningObject(MiningObjectType.CLAW_FOSSIL, 4, 5,
                new string[]
                {
                    "**xx",
                    "*xxx",
                    "*xxx",
                    "xxx*",
                    "xx**",
                }),
            new MiningObject(MiningObjectType.CLAW_FOSSIL, 5, 4,
                new string[]
                {
                    "xx***",
                    "xxxx*",
                    "*xxxx",
                    "**xxx",
                }, RotateFlipType.Rotate90FlipNone),
            new MiningObject(MiningObjectType.CLAW_FOSSIL, 4, 5,
                new string[]
                {
                    "**xx",
                    "*xxx",
                    "xxx*",
                    "xxx*",
                    "xx**",
                }, RotateFlipType.Rotate180FlipNone),
            new MiningObject(MiningObjectType.CLAW_FOSSIL, 5, 4,
                new string[]
                {
                    "xxx**",
                    "xxxx*",
                    "*xxxx",
                    "***xx",
                }, RotateFlipType.Rotate270FlipNone),
            new MiningObject(MiningObjectType.ROOT_FOSSIL, 5, 5,
                new string[]
                {
                    "xxxx*",
                    "xxxxx",
                    "xx*xx",
                    "***xx",
                    "**xx*",
                }),
            new MiningObject(MiningObjectType.ROOT_FOSSIL, 5, 5,
                new string[]
                {
                    "**xxx",
                    "**xxx",
                    "x**xx",
                    "xxxxx",
                    "*xxx*",
                }, RotateFlipType.Rotate90FlipNone),
            new MiningObject(MiningObjectType.ROOT_FOSSIL, 5, 5,
                new string[]
                {
                    "*xx**",
                    "xx***",
                    "xx*xx",
                    "xxxxx",
                    "*xxxx",
                }, RotateFlipType.Rotate180FlipNone),
            new MiningObject(MiningObjectType.ROOT_FOSSIL, 5, 5,
                new string[]
                {
                    "*xxx*",
                    "xxxxx",
                    "xx**x",
                    "xxx**",
                    "xxx**",
                }, RotateFlipType.Rotate270FlipNone),
            new MiningObject(MiningObjectType.OLD_AMBER, 4, 4,
                new string[]
                {
                    "*xxx",
                    "xxxx",
                    "xxxx",
                    "xxx*",
                }),
            new MiningObject(MiningObjectType.OLD_AMBER, 4, 4,
                new string[]
                {
                    "xxx*",
                    "xxxx",
                    "xxxx",
                    "*xxx",
                }, RotateFlipType.Rotate90FlipNone),
            new MiningObject(MiningObjectType.RARE_BONE, 3, 6,
                new string[]
                {
                    "xxx",
                    "*x*",
                    "*x*",
                    "*x*",
                    "*x*",
                    "xxx",
                }),
            new MiningObject(MiningObjectType.RARE_BONE, 6, 3,
                new string[]
                {
                    "x****x",
                    "xxxxxx",
                    "x****x",
                }, RotateFlipType.Rotate90FlipNone),
            new MiningObject(MiningObjectType.REVIVE, 3, 3,
                new string[]
                {
                    "*x*",
                    "xxx",
                    "*x*",
                }),
            new MiningObject(MiningObjectType.MAX_REVIVE, 3, 3),
            new MiningObject(MiningObjectType.RED_SHARD, 3, 3,
                new string[]
                {
                    "xxx",
                    "xx*",
                    "xxx",
                }),
            new MiningObject(MiningObjectType.BLUE_SHARD, 3, 3,
                new string[]
                {
                    "xxx",
                    "xxx",
                    "xx*",
                }),
            new MiningObject(MiningObjectType.YELLOW_SHARD, 4, 3,
                new string[]
                {
                    "x*x*",
                    "xxx*",
                    "xxxx",
                }),
            new MiningObject(MiningObjectType.GREEN_SHARD, 4, 3,
                new string[]
                {
                    "xxxx",
                    "xxxx",
                    "xx*x",
                }),
            new MiningObject(MiningObjectType.HEART_SCALE, 2, 2,
                new string[]
                {
                    "x*",
                    "xx",
                }),
            new MiningObject(MiningObjectType.ARMOR_FOSSIL, 5, 4,
                new string[]
                {
                    "*xxx*",
                    "*xxx*",
                    "xxxxx",
                    "*xxx*",
                }),
            new MiningObject(MiningObjectType.SKULL_FOSSIL, 4, 4,
                new string[]
                {
                    "xxxx",
                    "xxxx",
                    "xxxx",
                    "*xx*",
                }),
            new MiningObject(MiningObjectType.LIGHT_CLAY, 4, 4,
                new string[]
                {
                    "x*x*",
                    "xxxx",
                    "xxxx",
                    "*x*x",
                }),
            new MiningObject(MiningObjectType.IRON_BALL, 3, 3),
            new MiningObject(MiningObjectType.ICY_ROCK, 4, 4,
                new string[]
                {
                    "*xx*",
                    "xxxx",
                    "xxxx",
                    "x**x",
                }),
            new MiningObject(MiningObjectType.SMOOTH_ROCK, 4, 4,
                new string[]
                {
                    "**x*",
                    "xxx*",
                    "*xxx",
                    "*x**",
                }),
            new MiningObject(MiningObjectType.HEAT_ROCK, 4, 3,
                new string[]
                {
                    "x*x*",
                    "xxxx",
                    "xxxx",
                }),
            new MiningObject(MiningObjectType.DAMP_ROCK, 3, 3,
                new string[]
                {
                    "xxx",
                    "xxx",
                    "x*x",
                }),
            new MiningObject(MiningObjectType.FLAME_PLATE, 4, 3),
            new MiningObject(MiningObjectType.SPLASH_PLATE, 4, 3),
            new MiningObject(MiningObjectType.ZAP_PLATE, 4, 3),
            new MiningObject(MiningObjectType.MEADOW_PLATE, 4, 3),
            new MiningObject(MiningObjectType.ICICLE_PLATE, 4, 3),
            new MiningObject(MiningObjectType.FIST_PLATE, 4, 3),
            new MiningObject(MiningObjectType.TOXIC_PLATE, 4, 3),
            new MiningObject(MiningObjectType.EARTH_PLATE, 4, 3),
            new MiningObject(MiningObjectType.SKY_PLATE, 4, 3),
            new MiningObject(MiningObjectType.MIND_PLATE, 4, 3),
            new MiningObject(MiningObjectType.INSECT_PLATE, 4, 3),
            new MiningObject(MiningObjectType.STONE_PLATE, 4, 3),
            new MiningObject(MiningObjectType.SPOOKY_PLATE, 4, 3),
            new MiningObject(MiningObjectType.DRACO_PLATE, 4, 3),
            new MiningObject(MiningObjectType.DREAD_PLATE, 4, 3),
            new MiningObject(MiningObjectType.IRON_PLATE, 4, 3),
        };

        private static readonly MiningObject[] Rocks = new MiningObject[]
        {
            // please excuse the letters, it's so I can easily pick the right tile sprite for the visualization
            new MiningObject(MiningObjectType.ROCK_1, 4, 1, new string[]
                {
                    "CVVD"
                }),
            new MiningObject(MiningObjectType.ROCK_1, 1, 4, new string[]
                {
                    "A",
                    "W",
                    "W",
                    "B",
                }),
            new MiningObject(MiningObjectType.ROCK_2, 2, 2, new string[]
                {
                    "NO",
                    "PQ",
                }),
            new MiningObject(MiningObjectType.ROCK_3, 3, 2, new string[]
                {
                    "CKD",
                    "*B*",
                }),
            new MiningObject(MiningObjectType.ROCK_3, 2, 3, new string[]
                {
                    "*A",
                    "CJ",
                    "*B",
                }),
            new MiningObject(MiningObjectType.ROCK_3, 3, 2, new string[]
                {
                    "*A*",
                    "CLD",
                }),
            new MiningObject(MiningObjectType.ROCK_3, 2, 3, new string[]
                {
                    "A*",
                    "ID",
                    "B*",
                }),
            new MiningObject(MiningObjectType.ROCK_4, 3, 2, new string[]
                {
                    "CF*",
                    "*ED",
                }),
            new MiningObject(MiningObjectType.ROCK_4, 2, 3, new string[]
                {
                    "*A",
                    "HG",
                    "B*",
                }),
            new MiningObject(MiningObjectType.ROCK_5, 3, 2, new string[]
                {
                    "*HD",
                    "CG*",
                }),
            new MiningObject(MiningObjectType.ROCK_5, 2, 3, new string[]
                {
                    "A*",
                    "EF",
                    "*B",
                }),
            new MiningObject(MiningObjectType.ROCK_6, 3, 3, new string[]
                {
                    "NRO",
                    "TMU",
                    "PSQ",
                }),
            new MiningObject(MiningObjectType.ROCK_7, 2, 4, new string[]
                {
                    "NO",
                    "TU",
                    "TU",
                    "PQ"
                }),
            new MiningObject(MiningObjectType.ROCK_7, 4, 2, new string[]
                {
                    "NRRO",
                    "PSSQ"
                })
        };

        private static readonly int[,] DirtTemplate = new int[8, 8]
        {
            { 0, 0, 4, 4, 4, 4, 0, 0 },
            { 0, 4, 4, 4, 4, 4, 4, 0 },
            { 4, 4, 4, 4, 4, 4, 0, 0 },
            { 4, 4, 4, 4, 4, 4, 0, 0 },
            { 4, 4, 4, 4, 4, 4, 0, 0 },
            { 4, 4, 4, 4, 4, 4, 0, 0 },
            { 0, 4, 4, 4, 4, 4, 4, 0 },
            { 0, 0, 4, 4, 4, 4, 0, 0 }
        };

        private static readonly int[,] RockTemplate = new int[5, 5]
        {
            { 0, 6, 6, 6, 0 },
            { 6, 6, 6, 6, 6 },
            { 6, 6, 6, 6, 6 },
            { 6, 6, 6, 6, 6 },
            { 0, 6, 6, 6, 0 }
        };

        private readonly int[,] weights;
        private readonly int totalWeight;
        private readonly HashSet<MiningObjectType> alreadyMinedPlates = new HashSet<MiningObjectType>()
        {
            MiningObjectType.FLAME_PLATE ,
            MiningObjectType.SPLASH_PLATE,
            MiningObjectType.ZAP_PLATE  ,
            MiningObjectType.MEADOW_PLATE,
            MiningObjectType.ICICLE_PLATE,
            MiningObjectType.FIST_PLATE ,
            MiningObjectType.TOXIC_PLATE ,
            MiningObjectType.EARTH_PLATE ,
            MiningObjectType.SKY_PLATE  ,
            MiningObjectType.MIND_PLATE ,
            MiningObjectType.INSECT_PLATE,
            MiningObjectType.STONE_PLATE ,
            MiningObjectType.SPOOKY_PLATE,
            MiningObjectType.DRACO_PLATE ,
            MiningObjectType.DREAD_PLATE ,
            MiningObjectType.IRON_PLATE
        };

        public readonly int WallWidth = 13;
        public readonly int WallHeight = 10;
        public readonly ushort TID;
        public readonly bool NationalDex;

        private MiningWallGenerator(ushort tid, bool nationalDex, HashSet<MiningObjectType> alreadyMinedPlates = null)
        {
            TID = tid;
            NationalDex = nationalDex;
            weights = nationalDex ? MiningObject.ItemWeightsNationalDex : MiningObject.ItemWeights;
            totalWeight = GetTreasureWeightSum(tid, weights);
            if (alreadyMinedPlates != null)
                this.alreadyMinedPlates = alreadyMinedPlates;
        }

        public static MiningWallGenerator CreateInstance(ushort tid, bool nationalDex, HashSet<MiningObjectType> alreadyMinedPlates = null)
            => new MiningWallGenerator(tid, nationalDex, alreadyMinedPlates);

        public MiningWallResult Generate(ulong seed, bool firstEverMinigame = false)
        {
            ulong headSeed = seed;

            var treasures = new List<(int x, int y, MiningObject item)>();
            var rocks = new List<(int x, int y, MiningObject rock)>();
            var wall = new bool[WallHeight, WallWidth];

            int itemCount = seed.GetRand(3) + 2;
            if (firstEverMinigame) itemCount = 3;

            int objectsPlaced = 0;
            while (objectsPlaced < itemCount)
            {
                var treasure = PickTreasure(ref seed);

                if (treasure.IsPlate())
                {
                    if (alreadyMinedPlates.Contains(treasure.Type)) continue; // each plate can only ever be mined once
                    if (treasures.Any(_ => _.item.Type == treasure.Type)) continue; // a wall can't contain multiple of the same plate
                }

                int x = seed.GetRand(WallWidth);
                int y = seed.GetRand(WallHeight);

                if (treasure.TryPlace(x, y, wall))
                {
                    objectsPlaced++;
                    treasures.Add((x, y, treasure));
                }
            }

            if (!firstEverMinigame)
            {
                // the < 8 can't go in the head of the loop since the loop will still use 3 * (100 - i) rand calls after the object limit is reached
                for (int i = 0; i < 100; i++)
                {
                    var rock = Rocks[seed.GetRand(14)];
                    int x = seed.GetRand(WallWidth);
                    int y = seed.GetRand(WallHeight);

                    if (objectsPlaced < 8 && rock.TryPlace(x, y, wall))
                    {
                        objectsPlaced++;
                        rocks.Add((x, y, rock));
                    }
                }
            }

            var cover = GenerateCoverLayer(ref seed);

            return new MiningWallResult(headSeed, seed, treasures, rocks, cover);
        }

        private int[,] GenerateCoverLayer(ref ulong seed)
        {
            // if the cover is supposed to have the correct durability values for the tile, set each element to 2; for drawing it's irrelevant so I'm leaving them at 0
            var cover = new int[WallHeight, WallWidth];
            byte dirtShapeSize = 8;
            byte rockShapeSize = 5;

            for (int i = 0; i < 10; i++)
            {
                int startX = seed.GetRand(WallWidth + dirtShapeSize) - dirtShapeSize;
                int startY = seed.GetRand(WallHeight + dirtShapeSize) - rockShapeSize;

                for (int y = startY; y < startY + dirtShapeSize; y++)
                {
                    if (y >= WallHeight || y < 0) continue;

                    for (int x = startX; x < startX + dirtShapeSize; x++)
                    {
                        if (x >= WallWidth || x < 0) continue;
                        if (DirtTemplate[y - startY, x - startX] == 0) continue;

                        cover[y, x] = DirtTemplate[y - startY, x - startX];
                    }
                }
            }

            for (int i = 0; i < 15; i++)
            {
                int startX = seed.GetRand(WallWidth + rockShapeSize) - rockShapeSize;
                int startY = seed.GetRand(WallHeight + rockShapeSize) - rockShapeSize;

                for (int y = startY; y < startY + rockShapeSize; y++)
                {
                    if (y >= WallHeight || y < 0) continue;

                    for (int x = startX; x < startX + rockShapeSize; x++)
                    {
                        if (x >= WallWidth || x < 0) continue;
                        if (DirtTemplate[y - startY, x - startX] == 0) continue;

                        if (cover[y, x] < 4) // sand tile
                            goto IDontLikeSand;
                    }
                }

                for (int x = startY; x < startY + rockShapeSize; x++)
                {
                    if (x >= WallHeight || x < 0) continue;

                    for (int y = startX; y < startX + rockShapeSize; y++)
                    {
                        if (y >= WallWidth || y < 0) continue;
                        if (RockTemplate[x - startY, y - startX] == 0) continue;

                        cover[x, y] = RockTemplate[x - startY, y - startX];
                    }
                }

                IDontLikeSand:; // this is a nice alternative to using 2 breaks and a continue
            }

            return cover;
        }

        private MiningObject PickTreasure(ref ulong seed)
        {
            int counter = seed.GetRand(totalWeight);

            for (int i = 0; i < Treasures.Length; i++)
            {
                counter -= weights[i, TID & 1];

                if (counter < 0) return Treasures[i];
            }

            return Treasures[0];
        }      

        private int GetTreasureWeightSum(ushort tid, int[,] weights)
            => Enumerable.Range(0, weights.GetLength(0))
                .Select(_ => weights[_, tid & 1]).Sum();
        

    }
}
