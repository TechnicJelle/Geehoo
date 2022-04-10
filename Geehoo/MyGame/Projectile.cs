// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using System;
using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class Projectile : EasyDraw
{
	private Vec2 _pos;
	private Vec2 _vel;
	private Vec2 _acc;

	private int _radius;
	private bool _enemyShot;

	public Projectile(float x, float y, float xVel, float yVel, int radius, bool enemyShot)
		: this(new Vec2(x, y), new Vec2(xVel, yVel), radius, enemyShot) {}

	public Projectile(Vec2 spawnPos, Vec2 spawnVel, int radius, bool enemyShot)
		: base(radius * 2, radius * 2, false){
		_pos = spawnPos;
		_vel = spawnVel;
		_acc = new Vec2();
		_radius = radius;
		_enemyShot = enemyShot;

		SetOrigin(_radius, _radius);

		UpdatePos();

		NoStroke();
		Fill(64, 64, 255);
		Ellipse(_radius, _radius, _radius*2, _radius*2);
	}

	private void Update()
	{
		UpdatePos();

		MyGame myGame = (MyGame) game;
		if(_pos.DistSq(myGame.player.pos) < Math.Pow(_radius + myGame.player.coreRadius, 2))
		{
			myGame.player.TakeDamage();
			Destroy();
		}

		for (int i = myGame.enemies.Count - 1; i >= 0; i--)
		{
			Enemy enemy = myGame.enemies[i];
			if(_enemyShot) continue;
			//TODO: Better physics calculations for this one!
			if (_pos.DistSq(enemy.pos) < Math.Pow(_radius + enemy.radius, 2))
			{
				enemy.TakeDamage();
				Destroy();
			}
		}

		if (y < -_radius || y > game.height + _radius || x < -_radius || x > game.width + _radius)
		{
			Destroy();
			Console.WriteLine($"Projectile destroyed: x: {x}, y: {y}" + Utils.Random(0, 10));
		}
	}

	private void UpdatePos()
	{
		_vel += _acc;
		_pos += _vel * Time.deltaTime;
		_acc *= 0;

		x = _pos.x;
		y = _pos.y;
	}
}
