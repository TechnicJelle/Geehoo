// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class Enemy : EasyDraw
{
	//TODO: Make a base class for both enemies and player
	public Vec2 pos => new(x, y);
	public float radius { get; }

	public int health { get; private set; }

	private const int SIZE = 32;

	private const float SPEED = 0.03f;
	private const int SHOOT_DELAY = 500;

	private int _millisAtLastShot;
	private readonly float _offset;

	public Enemy(float y, float offset) : base(SIZE, SIZE, false)
	{
		radius = SIZE / 2f;
		health = 1;

		SetOrigin(SIZE / 2f, SIZE / 2f);

		this.y = y;
		_offset = offset;

		NoStroke();

		Fill(128);
		//       MIDDLE BOTTOM ,    TOP LEFT,  MIDDLE TOP,            RIGHT TOP
		Quad(SIZE / 2f, SIZE, 0, 0, SIZE/2f, SIZE * 0.2f, SIZE, 0);
	}

	public void Recalc()
	{
		y += SPEED * Time.deltaTime;
		if (y < -SIZE)
			return;
		// if (y > game.height + 100)
		//  Destroy(); //not yet

		x = Mathf.Map(Mathf.Sin(y*0.01f + _offset), -1, 1, 0, game.width);

		if (Time.time - _millisAtLastShot > SHOOT_DELAY)
		{
			_millisAtLastShot = Time.time;
			game.AddChild(new Projectile(x, y, 0, 0.4f, 4, true));
		}
	}

	public void TakeDamage(int damage = 1)
	{
		health -= damage;
		if (health <= 0)
		{
			MyGame myGame = (MyGame) game;
			myGame.enemies.Remove(this);
			Destroy();
		}
	}
}
