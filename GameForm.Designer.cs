using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Game
{
	public class GameForm : Form
	{
		private readonly Image firstPlayerImg;
		private readonly Image secondPlayerImg;
		private readonly Timer timer;
		private Level currentLevel;
        private readonly Image image;
		private bool right;
		private bool left;
		private bool up;
		private bool down;
		private bool isFirst;
		private readonly Size spaceSize = new Size(900, 900);

		private int iterationIndex;

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			DoubleBuffered = true;
			WindowState = FormWindowState.Maximized;
		}

		public GameForm(IEnumerable<Level> levels)
		{
            firstPlayerImg = Image.FromFile(" D:/Проекты С#/Game/Game/Sprites/link.png");
			secondPlayerImg = Image.FromFile(" D:/Проекты С#/Game/Game/Sprites/rocket.png");
			image = new Bitmap(spaceSize.Width, spaceSize.Height, PixelFormat.Format24bppRgb);
			timer = new Timer { Interval = 10 };
			timer.Tick += TimerTick;
			timer.Start();
			var top = 10;
			foreach (var level in levels)
			{
				if (currentLevel == null) currentLevel = level;
				var link = new LinkLabel
				{
					Text = level.Name,
					Left = 10,
					Top = top,
					BackColor = Color.Transparent
				};
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
			currentLevel.Reset();
			timer.Start();
			iterationIndex = 0;
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
			if (e == Keys.Q) isFirst = true;
			if (e == Keys.E) isFirst = false;
		}
		private void MovePlayer()
		{
			//var rotate = left ? Turn.Left : (right ? Turn.Right : Turn.None);
			//currentLevel.Move(spaceSize, rotate);
			if (up) currentLevel.Move(spaceSize, isFirst, Turn.None, 0, 70);
			if (down) currentLevel.Move(spaceSize, isFirst, Turn.None, 0, -70);
			if (left) currentLevel.Move(spaceSize, isFirst, Turn.None, -70, 0);
			if (right) currentLevel.Move(spaceSize, isFirst, Turn.None, 70, 0);

		}

		private void TimerTick(object sender, EventArgs e)
		{
			if (currentLevel == null) return;
			MovePlayer();
			Text = "IsFirst - " + isFirst;
			Invalidate();
			Update();
		}

        protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.Bisque, ClientRectangle);

			var g = Graphics.FromImage(image);
			var g2 = Graphics.FromImage(image);

			currentLevel.Player1.image = firstPlayerImg;
			currentLevel.Player2.image = secondPlayerImg;

			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillRectangle(Brushes.Beige, ClientRectangle);

			DrawTo(g, currentLevel.Player1);
			DrawTo(g2, currentLevel.Player2);

			e.Graphics.DrawImage(image, (ClientRectangle.Width - image.Width) / 2, (ClientRectangle.Height - image.Height) / 2);
		}

		private void DrawTo(Graphics g, Player player)
		{

			if (currentLevel == null) return;

			var matrix = g.Transform;

			if (timer.Enabled)
			{
				g.Transform = matrix;

				g.TranslateTransform((float)player.Location.X, (float)player.Location.Y);
				g.DrawImage(player.image, new Point(-firstPlayerImg.Width / 2, -firstPlayerImg.Height / 2));

			}
		}
	}
}