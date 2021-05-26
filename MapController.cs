using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
	public class MapController
	{
        public int MapHeight;
        public int MapWidth;
        public int cellSize = 40;

		public readonly MapCell[,] Dungeon;
		public readonly Vector InitialPositionP1;
		public readonly Vector InitialPositionP2;
		public readonly Vector Exit;
		public readonly Vector Button;
		public readonly Vector Door;
		public int DoorIndex;


		public Image spriteSheet;
		public List<MapEntity> mapObjects;

		public void Init(MapController map)
		{
			spriteSheet = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Sprites\\Forest.png"));
			map.mapObjects = new List<MapEntity>();
		}


		private MapController(MapCell[,] dungeon, Vector initialPositionP1, Vector initialPositionP2, Vector exit, Vector button, Vector door, int mapWidth, int mapHeight)
		{
			Dungeon = dungeon;
			InitialPositionP1 = initialPositionP1;
			InitialPositionP2 = initialPositionP2;
			Exit = exit;
			Button = button;
			MapWidth = mapWidth;
			MapHeight = mapHeight;
			Door = door;
		}

		public static MapController FromText(string text)
		{
			var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			return FromLines(lines);
		}

		public void InitMap(MapController map)
		{
			for (int i = 0; i < map.MapHeight; i++)
			{
				for (int j = 0; j < map.MapWidth; j++)
				{
					switch (map.Dungeon[i, j])
					{
						case MapCell.Wall:
							{
								map.mapObjects.Add(new MapEntity(new PointF(j * map.cellSize, i * map.cellSize), new Size(40, 40)));
								break;
							}
						case MapCell.Door:
							{
								map.mapObjects.Add(new MapEntity(new PointF(j * map.cellSize, i * map.cellSize), new Size(40, 40)));
								DoorIndex = mapObjects.Count;
								break;
							}
					}
				}
			}
		}

		public static void DrawMap(Graphics g, MapController map)
		{
			for (int i = 0; i < map.MapHeight; i++)
			{
				for (int j = 0; j < map.MapWidth; j++)
				{
					switch (map.Dungeon[i,j])
					{
						case MapCell.Wall:
							{
								g.DrawImage(map.spriteSheet, new Rectangle(new Point(j * map.cellSize, i * map.cellSize), new Size(map.cellSize, map.cellSize)), 0, 200, 20, 20, GraphicsUnit.Pixel);  
								break;
							}
						case MapCell.Button:
							{
								g.DrawImage(map.spriteSheet, new Rectangle(new Point(j * map.cellSize, i * map.cellSize), new Size(20, 12)), 581, 114, 19, 11, GraphicsUnit.Pixel);
								break;
							}
						case MapCell.Exit:
							{
								g.DrawImage(map.spriteSheet, new Rectangle(new Point((int)map.Exit.X,
								(int)map.Exit.Y),
								new Size(map.cellSize, map.cellSize)), 0, 100, 20, 20, GraphicsUnit.Pixel);
								break;
							}
					}
				}
			}
		} 

		public static MapController FromLines(string[] lines)
		{
			var dungeon = new MapCell[lines[0].Length, lines.Length];
			var initialPositionP1 = Vector.Zero;
			var initialPositionP2 = Vector.Zero;
			var exit = Vector.Zero;
			var door = Vector.Zero;
			var button = Vector.Zero;
			var cellSize = 40;
			for (var y = 0; y < lines[0].Length; y++)
			{
				for (var x = 0; x < lines.Length; x++)
				{
					switch (lines[y][x])
					{
						case '#':
							dungeon[y, x] = MapCell.Wall;
							break;
						case 'P':
							dungeon[y, x] = MapCell.Player1;
							initialPositionP1 = new Vector(x * cellSize, y * cellSize);
							break;
						case 'H':
							dungeon[y, x] = MapCell.Player2;
							initialPositionP2 = new Vector(x * cellSize, y * cellSize);
							break;
						case 'B':
							dungeon[y, x] = MapCell.Button;
							button = new Vector(x * cellSize, y * cellSize);
							break;
						case 'E':
							dungeon[y, x] = MapCell.Exit;
							exit = new Vector(x * cellSize, y * cellSize);
							break;
						case 'D':
							dungeon[y, x] = MapCell.Door;
							door = new Vector(x * cellSize, y * cellSize);
							break;
						default:
							dungeon[y, x] = MapCell.Empty;
							break;
					}
				}
			}
			return new MapController(dungeon, initialPositionP1, initialPositionP2, exit, button, door, lines.Length, lines[0].Length);
		}
		public static int GetWidth(MapController map)
		{
			return map.cellSize * (map.MapWidth) + 15;
		}
		public static int GetHeight(MapController map)
		{
			return map.cellSize * (map.MapHeight + 1) + 10;
		}
	}
}
