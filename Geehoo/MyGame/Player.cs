// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class Player : EasyDraw
{
	private const int SIZE = 48;
	private const float CORE_SIZE = 12;
	private readonly Vec2 _relativeCorePosition;


	public Player(Vec2 pos) : base(SIZE, SIZE)
	{
		x = pos.x;
		y = pos.y;

		NoStroke();

		//body
		Fill(64, 64, 64);
		Quad(SIZE /2f, 0, SIZE, SIZE, SIZE /2f, SIZE * 0.8f, 0, SIZE);

		//core
		Fill(64, 64, 255);
		_relativeCorePosition = new Vec2(SIZE/2f, SIZE * 0.6f);
		Ellipse(_relativeCorePosition.x, _relativeCorePosition.y, CORE_SIZE, CORE_SIZE);
	}

	public Vec2 GetCorePosition()
	{
		return new Vec2(x + _relativeCorePosition.x, y + _relativeCorePosition.y);
	}

	private void Update()
	{
		x = Input.mouseX;
		y = Input.mouseY - SIZE;

		if (Input.GetMouseButton(0))
		{
			game.AddChild(new Projectile(x + SIZE/2f, y, 0, -10, 5));
		}
	}
}
