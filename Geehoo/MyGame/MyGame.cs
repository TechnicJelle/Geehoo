using System;
using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class MyGame : Game
{
	private readonly Player _player;

	private MyGame() : base(500, 1000, false, true)
	{
		_player = new Player(new Vec2(250, 500));
		AddChild(_player);
		Console.WriteLine("MyGame initialized");
	}

	// For every game object, Update is called every frame, by the engine:
	private void Update()
	{
		Gizmos.DrawLine(0, 0, _player.GetCorePosition().x, _player.GetCorePosition().y);
		Console.WriteLine(currentFps);
		Console.WriteLine(game.GetDiagnostics());
	}

	private static void Main()
	{
		new MyGame().Start();
	}
}
