using System.Drawing;
using System.Reflection;
using System.Resources;

namespace SinnohUndergroundRNGLibrary.Util
{
    public class Visualizer
    {
        // if you happen to run into any problems in different projects, this line may contain the culprite
        private static readonly ResourceManager resources = new ResourceManager("SinnohUndergroundRNGLibrary.Properties.Resources", Assembly.GetExecutingAssembly());

        public static Bitmap DrawCover(int[,] cover, int tileSize = 32)
        {
            var bitmap = new Bitmap(tileSize * 13, tileSize * 10);
            Graphics canvas = Graphics.FromImage(bitmap);

            var tiles = new Bitmap[]
            {
                Properties.Resources.sand,
                Properties.Resources.dirt,
                Properties.Resources.rock
            };

            for (int y = 0; y < cover.GetLength(0); y++)
            {
                for (int x = 0; x < cover.GetLength(1); x++)
                {
                    // 0 (or 2) -> sand; 4 -> dirt -> 6 rock -> so / 3 gives 0, 1, 2 for a nice index
                    canvas.DrawImage(tiles[cover[y, x] / 3], x * tileSize, y * tileSize, tileSize, tileSize);
                }
            }

            return bitmap;
        }

        public static Bitmap DrawWall(MiningWallResult wall, int tileSize = 32)
        {
            var bitmap = new Bitmap(tileSize * 13, tileSize * 10);
            Graphics canvas = Graphics.FromImage(bitmap);

            var backgroundTile = resources.GetObject("background") as Bitmap;

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 13; x++)
                {
                    canvas.DrawImage(backgroundTile, x * tileSize, y * tileSize, tileSize, tileSize);
                }
            }

            foreach ((int x, int y, var rock) in wall.Rocks)
            {
                for (int tileY = 0; tileY < rock.Shape.Length; tileY++)
                {
                    for (int tileX = 0; tileX < rock.Shape[tileY].Length; tileX++)
                    {
                        char id = rock.Shape[tileY][tileX];
                        if (id == '*') continue;

                        var tile = resources.GetObject($"rock_{id}") as Bitmap;
                        canvas.DrawImage(tile, (x + tileX) * tileSize, (y + tileY) * tileSize, tileSize, tileSize);
                    }
                }
            }

            foreach ((int x, int y, var treasure) in wall.Treasures)
            {
                int rotation = (int)treasure.ImageRotation;
                var sprite = resources.GetObject($"{treasure.Type}{(rotation > 0 ? $"_{rotation}" : "")}") as Bitmap;

                canvas.DrawImage(sprite, x * tileSize, y * tileSize, treasure.Width * tileSize, treasure.Height * tileSize);
            }

            return bitmap;
        }
    }
}
