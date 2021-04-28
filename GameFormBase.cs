namespace Game
{
    public class GameFormBase
    {
        private readonly Image firstPlayerImg;
        private readonly Image secondPlayerImg;
        private readonly Timer timer;
        private readonly Size spaceSize = new Size(900, 900);
        private readonly Image image;
        private Level currentLevel;
        private bool right;
        private bool left;
        private bool up;
        private bool down;
        private bool isFirst;
        private int iterationIndex;

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;
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

        private void ChangeLevel(Level newSpace)
        {
            currentLevel = newSpace;
            currentLevel.Reset();
            timer.Start();
            iterationIndex = 0;
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
    }
}