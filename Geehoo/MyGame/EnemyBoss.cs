// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using System;

namespace Geehoo.MyGame;

public class EnemyBoss : Enemy
{
	private const int HEALTH = 300;

	public EnemyBoss(float y, float offset) : base(y, offset, 64, HEALTH)
	{
	}

	//TODO: Add fancier shooting pattern

	//TODO: Boss follows player

	public override void Recalc()
	{
		base.Recalc();
		Console.WriteLine("Boss health: " + health);
	}
}
