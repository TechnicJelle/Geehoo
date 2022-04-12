// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class EnemyZigzag : Enemy
{
	private readonly float _x;

	public EnemyZigzag(float y, float x, int size = 32, int health = 1) : base(y, x, size, health)
	{
		ApplyForce(new Vec2(0, SPEED));
		_x = x;
	}

	public override void Recalc()
	{
		//TODO: Improve this
		// if (Pos.y > radius)
		// {
		// 	Vec2 force = new(Math.Sin(Pos.y * 0.1f + _x) * 0.01f, 0);
		// 	ApplyForce(force);
		// }

		base.Recalc();
		Pos.x = Mathf.Map(Mathf.Sin(Pos.y * 0.01f + _x), -1, 1, radius, game.width - radius);
	}
}
