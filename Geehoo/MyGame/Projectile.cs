// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class Projectile : EasyDraw
{
	private Vec2 _pos;
	private Vec2 _vel;
	private Vec2 _acc;

	private int _radius;
	public Projectile(float x, float y, float xVel, float yVel, int radius) : base(radius*2, radius*2)
	{
		_pos = new Vec2(x, y);
		_vel = new Vec2(xVel, yVel);
		_acc = new Vec2();
		_radius = radius;

		SetOrigin(_radius, _radius);

		UpdatePos();

		NoStroke();
		Fill(64, 64, 255);
		Ellipse(_radius, _radius, _radius*2, _radius*2);
	}

	private void Update()
	{
		UpdatePos();

		if (y < -100)
		{
			game.RemoveChild(this);
		}
	}

	private void UpdatePos()
	{
		_vel += _acc;
		_pos += _vel;
		_acc *= 0;

		x = _pos.x;
		y = _pos.y;
	}
}
