// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class Enemy : Entity
{
	private const int SIZE = 32;

	private const float SPEED = 0.4f;
	private const int SHOOT_DELAY = 500;
	private const float SHOT_SPEED = 1f;

	private int _millisAtLastShot;
	private readonly float _offset;

	public Enemy(float y, float offset) : base(offset, y, SIZE, 1)
	{
		ApplyForce(new Vec2(0, SPEED));
		_offset = offset;
		Fill(128);
		//       MIDDLE BOTTOM ,    TOP LEFT,  MIDDLE TOP,            RIGHT TOP
		Quad(SIZE / 2f, SIZE, 0, 0, SIZE/2f, SIZE * 0.2f, SIZE, 0);
	}

	public override void Recalc()
	{
		//positioning
		base.Recalc();
		if (Pos.y < -SIZE)
			return;
		Pos.x = Mathf.Map(Mathf.Sin(Pos.y * 0.01f + _offset), -1, 1, radius, game.width - radius);

		//remove if off screen
		// if (y > game.height + 100)
		//  Die(); //not yet

		//shooting
		if (Time.time - _millisAtLastShot > SHOOT_DELAY)
		{
			_millisAtLastShot = Time.time;
			game.AddChild(new Projectile(x, y, 0, SHOT_SPEED, 4, true));
		}
	}

	protected override void Die()
	{
		MyGame myGame = (MyGame) game;
		myGame.enemies.Remove(this);
		Destroy();
	}
}
