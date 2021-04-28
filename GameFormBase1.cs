namespace Game
{
    public class GameFormBase1
    {
        private readonly Size spaceSize = new Size(900, 900);
        private bool right;
        private bool left;
        private bool up;
        private bool down;
        private bool isFirst;

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
    }
}