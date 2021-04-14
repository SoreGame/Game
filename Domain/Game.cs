using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Domain
{
    public enum GameStage 
    {
        NotStarted = 0,
        FirstPlayerMove = 1,
        SecondPlayerMove = 2,
        Finished = 3
    }

    class Game
    {
        GameStage Stage;
        public Player FirstPlayer { get; set; }
        public Player SecondPlayer { get; set; }
        public bool IsFirstPlayerCurrent { get; set; }

        public Player CurrentPlayer => IsFirstPlayerCurrent ? FirstPlayer : SecondPlayer;
        //Таймер
        //Противники
        //Обьекты
    }

    class Field 
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Hero> Hero { get; set; }
    }

    class Hero 
    {
        public int Health { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Point Posittion { get; set; }
    }

    class Player
    {
        public string Name{get; set;}
        public Field Field { get; set; }
    }

    class Object
    {
        public Point Posittion { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
