// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

namespace Geehoo.MyGame;

public class EnemyBoss : Enemy
{
	private const int HEALTH = 50;

	public EnemyBoss(float y, float offset) : base(y, offset, 64, HEALTH)
	{
	}

	//TODO: Add fancier shooting pattern
}
