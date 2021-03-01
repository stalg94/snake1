using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake
{
	class Program
	{
		static void Main( string[] args )
		{
			Console.SetWindowSize( 80, 25 );

			Walls walls = new Walls( 80, 25 );
			walls.Draw();

			// Отрисовка точек			
			Point p = new Point( 4, 5, '*' );
			Snake snake = new Snake( p, 4, Direction.RIGHT );
			snake.Draw();

			FoodCreator foodCreator = new FoodCreator( 80, 25, '$' );
			Point food = foodCreator.CreateFood();
			FoodCreator foodCreator2 = new FoodCreator(80, 25, 'R');
			Point food2 = foodCreator2.CreateFood();
			food.Draw("Blue");

			Param settings = new Param();

			Sound sound = new Sound(settings.GetResourceFolder());
			sound.Play();
			Sound sound1 = new Sound(settings.GetResourceFolder());														  
			Scores score = new Scores(settings.GetResourceFolder());

			Timer time = new Timer();
			time.Counter();



			while (true)
			{
				if ( walls.IsHit(snake) || snake.IsHitTail() )
				{
					break;
				}
				if(snake.Eat( food ) )
				{
					food = foodCreator.CreateFood();
					food2 = foodCreator2.CreateFood();
					sound1.PlayEat();
					int check = score.currentFood();
					int n;
					if (check > 0 && ((check + 10) % 100) == 0)
					{
						food2.Draw("Grey");
						n = 1;

					}
					else if (check > 0 && check % 100 == 0)
					{
						food.Draw("Blue");
						n = 2;
					}
					else
					{
						food.Draw("Blue");
						n = 1;
					}
					score.UpCurrentPoints(n);
					score.ShowCurrentPoints();
				}
				else
				{
					snake.Move();
				}

				Thread.Sleep( 100 );
				if ( Console.KeyAvailable )
				{
					ConsoleKeyInfo key = Console.ReadKey();
					snake.HandleKey( key.Key );
				}
			}
			sound.Play("gameover");
			time.CounterStatus(false);
			score.WriteGameOver();
			score.userName();
			score.WriteBestResult();
			Console.ReadLine();

		}

	}
}
