using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

            TextUtils.AnimateText("Welcome to the Dungeons of Doom...", 70);
            Thread.Sleep(1000);

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
                message = ($"You met a/an: {room.Monster.Name} Health: {room.Monster.Health} Attack: {room.Monster.Attack}");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Beep();

                BattleMonster(room);            
            }
        }

        private void BattleMonster(Room room)
        {
            do
            {
            int damage = room.Monster.Fight(player);

            if (room.Monster.Health <= 0)
            {
                Console.WriteLine($"{room.Monster.Name} died out of fear!");
                room.Monster.Health = 0;
                //room.Monster = null;
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine($"You took {damage} damage");
                Thread.Sleep(1000);
            }

            if (player.Health > 0 && room.Monster.Health != 0)
            {
                int playerDamage = player.Fight(room.Monster);
                Console.WriteLine($"You gave {playerDamage} damage");
                Thread.Sleep(1000);
            }
            } while (room.Monster.Health != 0) ;

            room.Monster = null;
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
            if (world[player.X, player.Y].Item != null)
            {
                player.Backpack.Add(world[player.X, player.Y].Item);
                world[player.X, player.Y].Item = null;
            }
        }

        private void DisplayStats()
        {
            Console.WriteLine($"Health: {player.Health}");

            Console.WriteLine($"Game info: {message}");
            message = "";

            //if (message != null)
            //{
            //    Console.WriteLine(message);
            //    message = null;
            //}
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
                        if (random.Next(0, 100) < 10)
                            world[x, y].Monster = new Ogre("Ogre", "O", 30, 3);

                        if (random.Next(0, 100) < 10)
                            world[x, y].Monster = new Orc("Orc", "0", 12, 3);

                        if (random.Next(0, 100) < 10)
                            world[x, y].Item = new Weapon("Sword", "S", 2, 3);
                    }
                }
            }
        }

        private void CreatePlayer()
        {
            player = new Player("Player", "P", 30, 5, 0, 0);
        }
    }
}
