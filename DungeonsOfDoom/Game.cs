using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils;

namespace DungeonsOfDoom
{
    class Game
    {
        Player player;
        Room[,] world;
        Random random = new Random();
        string message = "";

        internal void Play()
        {
            CreatePlayer();
            CreateWorld();

            TextUtils.AnimateText(@"
     ______            _        _______  _______  _______  _        _______ 
    (  __  \ |\     /|( (    /|(  ____ \(  ____ \(  ___  )( (    /|(  ____ \
    | (  \  )| )   ( ||  \  ( || (    \/| (    \/| (   ) ||  \  ( || (    \/
    | |   ) || |   | ||   \ | || |      | (__    | |   | ||   \ | || (_____ 
    | |   | || |   | || (\ \) || | ____ |  __)   | |   | || (\ \) |(_____  )
    | |   ) || |   | || | \   || | \_  )| (      | |   | || | \   |      ) |
    | (__/  )| (___) || )  \  || (___) || (____/\| (___) || )  \  |/\____) |
    (______/ (_______)|/    )_)(_______)(_______/(_______)|/    )_)\_______)
                                                                            
             _______  _______    ______   _______  _______  __   __ 
            (  ___  )(  ____ \  (  __  \ (  ___  )(  ___  )(  \ /  )
            | (   ) || (    \/  | (  \  )| (   ) || (   ) || () () |
            | |   | || (__      | |   ) || |   | || |   | || || || |
            | |   | ||  __)     | |   | || |   | || |   | || |(_)| |
            | |   | || (        | |   ) || |   | || |   | || |   | |
            | (___) || )        | (__/  )| (___) || (___) || )   ( |
            (_______)|/         (______/ (_______)(_______)|/     \|", 1); Thread.Sleep(1000);

            do
            {
                Console.Clear();
                DisplayStats();
                DisplayBackpack();
                DisplayWorld();
                AskForMovement();
                CheckForItem();
                CheckForMonster();
            } while (player.Health > 0);

            GameOver();
        }

        private void CheckForMonster()
        {
            Room room = world[player.X, player.Y];

            if (room.Monster != null)
            {
                Console.Beep();
                TextUtils.AnimateText($"Monster spotted: {room.Monster.Name} Health: {room.Monster.Health} Attack: {room.Monster.Attack}", 20);
                Console.WriteLine();

                Battle(room);
            }
        }

        private void Battle(Room room)
        {
            while (player.Health > 0 && room.Monster.Health > 0)
            {
                int firstToFight = RandomUtils.Randomizer(1, 3);

                switch (firstToFight)
                {
                    case 1:
                        int damage = room.Monster.Fight(player);
                        if (room.Monster.Health > 0)
                        {
                            Console.WriteLine($"{player.Name} took {damage} damage");
                            Console.WriteLine($"{room.Monster.Name}: {room.Monster.Health} {player.Name}: {player.Health}");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            Console.WriteLine($"{room.Monster.Name} died out of fear!");
                            Thread.Sleep(1000);
                        }
                        break;

                    case 2:
                        int playerDamage = player.Fight(room.Monster);
                        Console.WriteLine($"{room.Monster.Name} took {playerDamage} damage");
                        Console.WriteLine($"{room.Monster.Name}: {room.Monster.Health} {player.Name}: {player.Health}");
                        Thread.Sleep(1000);
                        break;
                }
            }

            if (room.Monster.Health <= 0)
            {
                player.Backpack.Add(room.Monster);
                room.Monster = null;
                Monster.numberOfMonster--;
            }
        }

        private void DisplayBackpack()
        {
            Console.Write("Backpack: ");

            foreach (var item in player.Backpack)
            {
                Console.Write($"{item.Name} ");
            }
            Console.WriteLine();
        }

        private void CheckForItem()
        {
            Room room = world[player.X, player.Y];

            if (room.Item != null)
            {
                string itemMessage = room.Item.PickUpItem(player, room);
                TextUtils.AnimateText(itemMessage, 20);
                Thread.Sleep(1000);
            }
        }

        private void DisplayStats()
        {
            Console.WriteLine($"Player health: {player.Health} Player attack: {player.Attack}");
            Console.WriteLine($"Monsters: {Monster.numberOfMonster}");
            Console.WriteLine($"Game info: {message}");
            message = "";
        }

        private void AskForMovement()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            int newX = player.X;
            int newY = player.Y;
            bool isValidMove = true;

            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow: newX++; break;
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.UpArrow: newY--; break;
                case ConsoleKey.DownArrow: newY++; break;
                default: isValidMove = false; break;
            }

            if (isValidMove &&
                newX >= 0 && newX < world.GetLength(0) &&
                newY >= 0 && newY < world.GetLength(1))
            {
                player.X = newX;
                player.Y = newY;

                Console.ResetColor();
            }
        }

        private void DisplayWorld()
        {
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    // Lokal variabel room pekar ut world/rum
                    Room room = world[x, y];

                    if (player.X == x && player.Y == y)
                    {
                        Console.Write(player.Icon);
                    }
                    else if (room.Monster != null)
                    {
                        Console.Write(room.Monster.Icon);
                    }
                    else if (room.Item != null)
                    {
                        Console.Write(room.Item.Icon);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

        private void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game over...");
            Console.ReadKey();
            Play();
        }

        private void CreateWorld()
        {
            world = new Room[20, 10];

            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    world[x, y] = new Room();

                    // Genererar ej monster + items om det är samma som spelarens koordinat
                    if (player.X != x || player.Y != y)
                    {
                        // 10% av fallen lägger vi till ett monster i rummet
                        if (RandomUtils.Percentage(10))
                        {
                            if (RandomUtils.Randomizer(1, 3) == 1)
                            {
                                world[x, y].Monster = new Ogre("Ogre", "O", 30, 10);
                            }
                            else
                            {
                                world[x, y].Monster = new Orc("Orc", "0", 12, 10);
                            }
                        }

                        if (RandomUtils.Percentage(10))
                        {
                            if (RandomUtils.Randomizer(1, 3) == 1)
                            {
                                world[x, y].Item = new Weapon("Sword", "S", 2, 3);
                            }
                            else
                            {
                                world[x, y].Item = new Food("Mushroom", "M", 0, 5);
                            }
                        }
                    }
                }
            }
        }

        private void CreatePlayer()
        {
            player = new Player("Player", "P", 50, 10, 0, 0);
        }
    }
}
