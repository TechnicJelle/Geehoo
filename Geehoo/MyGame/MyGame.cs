using System;
using System.Collections.Generic;
using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class MyGame : Game
{
	public Player player { get; }
	public List<Enemy> enemies { get; }

	private readonly EasyDraw _hud;

	private const bool VSYNC = false;

	private MyGame() : base(500, 1000, false, VSYNC)
	{
		if (!VSYNC)
			targetFps = 60;
		player = new Player(250, 500);
		AddChild(player);

		enemies = new List<Enemy>();
		float f = 0;
		for (float h = 0; h > -height; h -= height / 5f)
		{
			for (float i = 0; i < width; i += width / 3f)
			{
				enemies.Add(new Enemy(h, f));
				Console.WriteLine(f);
				f += width / 4f;
			}
		}

		foreach (Enemy enemy in enemies)
		{
			AddChild(enemy);
		}

		_hud = new EasyDraw(width, height, false);
		AddChild(_hud);


		Console.WriteLine("MyGame initialized");
	}

	// For every game object, Update is called every frame, by the engine:
	private void Update()
	{
		foreach (Enemy enemy in enemies)
		{
			enemy.Recalc();
		}
		player.Recalc();

		// Gizmos.DrawLine(0, 0, player.GetCorePosition().x, player.GetCorePosition().y);

		// Console.WriteLine(currentFps);
		// Console.WriteLine(game.GetDiagnostics());

		_hud.ClearTransparent();
		_hud.TextAlign(CenterMode.Min, CenterMode.Min);
		_hud.Text("Health: " + player.health, 0, 0);
	}

	private static void Main()
	{
		new MyGame().Start();
	}
}
