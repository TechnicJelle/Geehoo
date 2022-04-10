// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using System;
using System.Linq;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class Projectile : Entity
{
	private readonly bool _enemyShot;

	public Projectile(float x, float y, float xVel, float yVel, int radius, bool enemyShot)
		: this(new Vec2(x, y), new Vec2(xVel, yVel), radius, enemyShot) {}

	public Projectile(Vec2 spawnPos, Vec2 spawnVel, int radius, bool enemyShot)
		: base(spawnPos.x, spawnPos.y, radius*2, 1) {
		_enemyShot = enemyShot;
		ApplyForce(spawnVel);

		Fill(64, 64, 255);
		Ellipse(this.radius, this.radius, this.radius*2, this.radius*2);
	}

	public void Update() {
		Recalc();
	}

	public override void Recalc()
	{
		base.Recalc();

		MyGame myGame = (MyGame) game;

		//Check if projectile has hit the player's core
		if(Pos.DistSq(myGame.player.GetCorePosition()) < Math.Pow(radius + myGame.player.coreRadius, 2))
		{
			myGame.player.TakeDamage();
			TakeDamage();
		}

		//Check if projectile has hit an enemy
		for (int i = myGame.enemies.Count - 1; i >= 0; i--)
		{
			if(_enemyShot) continue; //Enemy projectiles can't hit enemies themselves
			Enemy enemy = myGame.enemies[i];
			LineSegment trajectory = new(Pos, Pos + vel);
			if (enemy.hull.All(hullSegment => LineSegment.Intersect(trajectory, hullSegment).intersectionPoint == null)) continue;
			enemy.TakeDamage();
			TakeDamage();
		}

		//Check if projectile is out of bounds
		if (y < -radius || y > game.height + radius || x < -radius || x > game.width + radius)
		{
			TakeDamage();
		}
	}
}
