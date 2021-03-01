﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
	class Snake : Figure
	{
		Direction direction;

		public Snake( Point tail, int length, Direction _direction )
		{
			direction = _direction;
			pList = new List<Point>();
			for ( int i = 0; i < length; i++ )
			{
				Point p = new Point( tail );
				p.Move( i, direction );
				pList.Add( p );
			}
		}

		public void Move()
		{
			if (direction != Direction.PAUSE)
			{
				Timer.status = true;
				int xOffset = 80;
				int yOffset = 15;
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.SetCursorPosition(xOffset, yOffset++);
				WriteText("============================", xOffset, yOffset++);
				WriteText("Нажмите пробел для паузы ", xOffset + 2, yOffset++);
				WriteText("============================", xOffset, yOffset++);
				Point tail = pList.First();
				pList.Remove(tail);
				Point head = GetNextPoint();
				pList.Add(head);
				tail.Clear();
				head.Draw("Green");
			}
			else
			{
				Timer.status = false;
				int xOffset = 80;
				int yOffset = 15;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.SetCursorPosition(xOffset, yOffset++);
				WriteText("=== Для продолжения игры ===", xOffset, yOffset++);
				WriteText("= = = = П А У З А = = = =", xOffset + 2, yOffset++);
				WriteText("= нажмите на любую стрелку =", xOffset, yOffset++);
			}
		}

		public Point GetNextPoint()
		{
			Point head = pList.Last();
			Point nextPoint = new Point( head );
			nextPoint.Move( 1, direction );
			return nextPoint;
		}

		public bool IsHitTail()
		{
			var head = pList.Last();
			for(int i = 0; i < pList.Count - 2; i++ )
			{
				if ( head.IsHit( pList[ i ] ) )
					return true;
			}
			return false;
		}

		public void HandleKey(ConsoleKey key)
		{
			if ( key == ConsoleKey.LeftArrow )
				direction = Direction.LEFT;
			else if ( key == ConsoleKey.RightArrow )
				direction = Direction.RIGHT;
			else if ( key == ConsoleKey.DownArrow )
				direction = Direction.DOWN;
			else if ( key == ConsoleKey.UpArrow )
				direction = Direction.UP;
		}

		public bool Eat( Point food )
		{
			Point head = GetNextPoint();
			if ( head.IsHit( food ) )
			{
				food.sym = head.sym;
				pList.Add( food );
				return true;
			}
			else
				return false;
		}
		static void WriteText(String text, int xOffset, int yOffset)
		{
			Console.SetCursorPosition(xOffset, yOffset);
			Console.WriteLine(text);
		}
	}
}
