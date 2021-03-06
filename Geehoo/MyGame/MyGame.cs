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
		for (float h = 0; h > -height/2f; h -= height/2f / 5f)
		{
			for (float i = 0; i < width; i += width / 3f)
			{
				enemies.Add(new EnemyZigzag(h, f));
				Console.WriteLine(f);
				f += width / 4f;
			}
		}

		enemies.Add(new EnemyBoss(-height/2f, 250));

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
		Vec2 windForce = new(0, Mathf.Map(player.GetCorePosition().y, 0, height*0.7f, 0.11f, 0));
		player.ApplyForce(windForce);
		for (int i = enemies.Count - 1; i >= 0; i--)
		{
			Enemy enemy = enemies[i];
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
