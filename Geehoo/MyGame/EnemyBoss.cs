// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using System;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class EnemyBoss : Enemy
{
	private const int HEALTH = 300;

	public EnemyBoss(float y, float x) : base(y, x, 64, HEALTH)
	{
		DragFactor = 0.5f;
	}

	//TODO: Add fancier shooting pattern

	public override void Recalc()
	{
		MyGame myGame = (MyGame) game;
		Vec2 dir = myGame.player.GetCorePosition() - Pos;
		rotation = Vec2.Rotate(dir, -Angle.HALF_PI).GetAngle().GetDegrees();
		ApplyForce(dir.Limit(SPEED));

		base.Recalc();
		HitBoxRefresh(Pos, rot: rotation, draw:true);
		Console.WriteLine("Boss health: " + health);
	}
}
