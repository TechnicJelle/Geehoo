// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class Enemy : Entity
{
	private const int SIZE = 32;

	protected const float SPEED = 0.4f;
	private const int SHOOT_DELAY = 500;
	private const float SHOT_SPEED = 1f;

	private int _millisAtLastShot;

	public LineSegment[] hull { get; private set; }

	protected Enemy(float y, float x, int size = SIZE, int health = 1) : base(x, y, size, health)
	{
		Fill(128);

		HitBoxRefresh(new Vec2(radius, radius), true);
	}

	protected void HitBoxRefresh(Vec2 pos, bool first = false, float rot = 0, bool draw = false)
	{
		Vec2 middleBottom = pos + new Vec2(0, radius).Rotate(Angle.FromDegrees(rot));
		Vec2 leftTop = pos + new Vec2(-radius, -radius).Rotate(Angle.FromDegrees(rot));
		Vec2 middleTop = pos + new Vec2(0, -radius * 0.2f).Rotate(Angle.FromDegrees(rot));
		Vec2 rightTop = pos + new Vec2(radius, -radius).Rotate(Angle.FromDegrees(rot));

		LineSegment left = new(leftTop, middleBottom);
		LineSegment right = new(rightTop, middleBottom);
		LineSegment leftBack = new(leftTop, middleTop);
		LineSegment rightBack = new(rightTop, middleTop);

		hull = new []{left, right, leftBack, rightBack};

		if (draw)
		{
			foreach (LineSegment lineSegment in hull)
			{
				lineSegment.Draw();
			}
		}

		if(first)
			Quad(middleBottom, leftTop, middleTop, rightTop);
	}

	public override void Recalc()
	{
		//positioning
		base.Recalc();
		HitBoxRefresh(Pos);
		if (Pos.y < -SIZE)
			return;

		//remove if off screen
		if (y > game.height + radius)
			Die();

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
		LateDestroy();
	}
}
