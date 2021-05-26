using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Game
{
	public class GameForm : Form
	{
		private readonly Image firstPlayerImg;
		private readonly Image secondPlayerImg;
		private Image dwarf;
		private readonly Image spriteSheet;
		private readonly Timer timer;
		private Level currentLevel;
        private readonly Image image;
		private bool right;
		private bool left;
		private bool up;
		private Block block;
		private bool down;
		private readonly Size spaceSize = new Size(900, 900);
		private Level next;
		private int iterationIndex;

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			DoubleBuffered = true;
			WindowState = FormWindowState.Maximized;

			Init();
		}

		private void Init() 
		{
			dwarf = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Sprites\\Dwarf.png"));
		}

		public GameForm(IEnumerable<Level> levels)
		{
			DoubleBuffered = true;
            firstPlayerImg = Image.FromFile("D:/Проекты С#/Game/Game/Sprites/link.png");
			secondPlayerImg = Image.FromFile("D:/Проекты С#/Game/Game/Sprites/rocket.png");
			image = new Bitmap(spaceSize.Width, spaceSize.Height, PixelFormat.Format24bppRgb);
			timer = new Timer { Interval = 10 };
			timer.Tick += TimerTick;
			timer.Tick += SecondCounter;
			timer.Start();
			var top = 10;
			foreach (var level in levels)
			{
				if (currentLevel == null) 
				{
					currentLevel = level;
					level.Map.Init(currentLevel.Map);
					spriteSheet = currentLevel.Map.spriteSheet;
					level.Map.InitMap(level.Map);
				}
				var link = new LinkLabel
				{
					Text = level.Name,
					Left = 1000,
					Top = top,
					BackColor = Color.Transparent
				};
				block = currentLevel.block_;
				if (level.Id < levels.Count()) next = levels.SkipWhile(i => i.Id != level.Id).Skip(1).FirstOrDefault();
				link.LinkClicked += (sender, args) => ChangeLevel(level);
				link.Parent = this;
				Controls.Add(link);
				top += link.PreferredHeight + 10;
			}
			KeyPreview = true;
		}

		private void ChangeLevel(Level newSpace)
		{
			currentLevel = newSpace;
			newSpace.Map.Init(newSpace.Map);
			currentLevel.Map.InitMap(newSpace.Map);
			currentLevel.Reset();
			currentLevel.LevelBot.ClearBothQueue();
			timer.Start();
			iterationIndex = 0;
		}

		private void SecondCounter(object sender, EventArgs e) 
		{
			if (iterationIndex % 100 == 0) currentLevel.TimerTick();
			if (currentLevel.TimerValue <= 0)
			{
				currentLevel.LevelBot.UseBuffer(currentLevel.IsFirst);
				currentLevel.Reset();
			}
		}

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
			NextIteration(e.KeyChar);
        }

        protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			HandleKey(e.KeyCode, true);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			HandleKey(e.KeyCode, false);
		}

		private void HandleKey(Keys e, bool down)
		{
			if (e == Keys.A) left = down;
			if (e == Keys.D) right = down;
			if (e == Keys.W) up = down;
			if (e == Keys.S) this.down = down;
		}

		private void MovePlayer()
		{
			//var rotate = left ? Turn.Left : (right ? Turn.Right : Turn.None);
			//currentLevel.Move(spaceSize, rotate);
			if (up) currentLevel.Move(spaceSize, currentLevel.IsFirst, Turn.None, 0, 70);
			if (down) currentLevel.Move(spaceSize, currentLevel.IsFirst, Turn.None, 0, -70);
			if (left) currentLevel.Move(spaceSize, currentLevel.IsFirst, Turn.None, -70, 0);
			if (right) currentLevel.Move(spaceSize, currentLevel.IsFirst, Turn.None, 70, 0);
			if (!up && !down && !left && !right) currentLevel.Move(spaceSize, currentLevel.IsFirst, Turn.None, 0, 0);
		}

		private void NextIteration(object e)
		{
			if ((char)e == (char)113)
			{
				currentLevel.IterationCount++;
				currentLevel.Reset();
				if (currentLevel.IsFirst)
				{
					currentLevel.SwichIsFirst();
					currentLevel.LevelBot.ClearSecondQueie();
				}
				else
				{
					currentLevel.SwichIsFirst();
					currentLevel.LevelBot.ClearFirstQueue();
				}
			}
		}

		private void TimerTick(object sender, EventArgs e)
		{
			if (currentLevel == null) return;
			MovePlayer();
			currentLevel.MoveBot(currentLevel.IsFirst ? currentLevel.Player1: currentLevel.Player2);
			if (currentLevel.IsCompleted)
			{
				Text = "Level is on button - " + currentLevel.IsOnButton;
				timer.Stop();
				ChangeLevel(next);
			}
			else
			{
				Text = "IsFirst - " + currentLevel.IsFirst + currentLevel.TimerValue + " " + currentLevel.IterationCount;
				iterationIndex++;
			}
			if (currentLevel.IsOnButton) 
			{
				currentLevel.OpenDoor(currentLevel);
			}
			Invalidate();
			Update();
		}

        protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.Bisque, ClientRectangle);

			var g = e.Graphics; 

			currentLevel.Player1.image = firstPlayerImg;
			currentLevel.Player2.image = secondPlayerImg;

			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			//g.FillRectangle(Brushes.Beige, ClientRectangle);
			MapController.DrawMap(e.Graphics, currentLevel.Map);

			DrawTo(g, currentLevel.Player1, currentLevel.Map);
			DrawTo(g, currentLevel.Player2, currentLevel.Map);
			DrawActiveObjects(e.Graphics);

			//g.DrawImage(spriteSheet, new Rectangle(new Point((int)currentLevel.Exit.X, (int)currentLevel.Exit.Y), new Size(currentLevel.Map.cellSize, currentLevel.Map.cellSize)), 100, 0, 20, 20, GraphicsUnit.Pixel);
			//e.Graphics.DrawImage(image, (ClientRectangle.Width - image.Width) / 2, (ClientRectangle.Height - image.Height) / 2);

			//e.Graphics.FillRectangle(Brushes.Blue, new Rectangle(600, 500, 20, 100));

		}

		private void DrawActiveObjects(Graphics g) 
		{
			//g.DrawImage(spriteSheet, new Rectangle(new Point((int)currentLevel.Exit.X, 
			//	(int)currentLevel.Exit.Y), 
			//	new Size(currentLevel.Map.cellSize, currentLevel.Map.cellSize)), 0, 100, 20, 20, GraphicsUnit.Pixel);

			 g.DrawImage(spriteSheet, new Rectangle(new Point((int)currentLevel.Door.X, 
				(int)currentLevel.Door.Y), 
				new Size(currentLevel.Map.cellSize, currentLevel.Map.cellSize)), 5, 230, 20, 20, GraphicsUnit.Pixel);
		}

		private void DrawTo(Graphics g, Player player, MapController map)
		{

			if (currentLevel == null) return;

			//g.DrawImage(exit, new Point((int)currentLevel.Exit.X, (int)currentLevel.Exit.Y));

			var matrix = g.Transform;
			
			if (timer.Enabled)
			{
				g.Transform = matrix;

				//g.TranslateTransform((float)player.Location.X, (float)player.Location.Y);
				//g.DrawImage(dwarf, new Point(-firstPlayerImg.Width / 2, -firstPlayerImg.Height / 2));
				g.DrawImage(dwarf, new Rectangle((int)player.Location.X, (int)player.Location.Y, player.Size, player.Size), 32, 32, 31, 31, GraphicsUnit.Pixel);
			}	
		}
	}
}