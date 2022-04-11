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

	public LineSegment[] hull { get; private set; }

	public Enemy(float y, float offset, int size = SIZE, int health = 1) : base(offset, y, size, health)
	{
		ApplyForce(new Vec2(0, SPEED));
		_offset = offset;
		Fill(128);

		HitBoxRefresh(new Vec2(radius, radius), true);
	}

	private void HitBoxRefresh(Vec2 pos, bool first = false)
	{
		Vec2 middleBottom = pos + new Vec2(0, radius);
		Vec2 leftTop = pos + new Vec2(-radius, -radius);
		Vec2 middleTop = pos + new Vec2(0, -radius * 0.2f);
		Vec2 rightTop = pos + new Vec2(radius, -radius);

		LineSegment left = new(leftTop, middleBottom);
		LineSegment right = new(rightTop, middleBottom);
		LineSegment leftBack = new(leftTop, middleTop);
		LineSegment rightBack = new(rightTop, middleTop);

		hull = new []{left, right, leftBack, rightBack};

		if(first)
			Quad(middleBottom, leftTop, middleTop, rightTop);
	}

	public override void Recalc()
	{
		//positioning
		base.Recalc();
		Pos.x = Mathf.Map(Mathf.Sin(Pos.y * 0.01f + _offset), -1, 1, radius, game.width - radius);
		HitBoxRefresh(Pos);
		if (Pos.y < -SIZE)
			return;

		//remove if off screen
		if (y > game.height + radius)
			Die(); //not yet

		//shooting
		if (Time.time - _millisAtLastShot > SHOOT_DELAY)
		{
			_millisAtLastShot = Time.time;
			game.AddChild(new Projectile(x, y, 0, SHOT_SPEED, 4, true));
		}

		//drawing hitbox
		foreach (LineSegment lineSegment in hull)
		{
			lineSegment.Draw();
		}
	}

	protected override void Die()
	{
		MyGame myGame = (MyGame) game;
		myGame.enemies.Remove(this);
		LateDestroy();
	}
}
