// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class Entity : EasyDraw
{
	protected float DragFactor = 1f;

	protected Vec2 Pos;
	protected Vec2 vel { get; private set; }
	private Vec2 _acc;

	protected readonly int Size;
	protected float radius { get; }
	public int health { get; private set; }

	protected Entity(float x, float y, int size, int health) : base(size, size, false)
	{
		Pos = new Vec2(x, y);
		vel = new Vec2(0, 0);
		_acc = new Vec2(0, 0);

		Size = size;
		radius = size / 2f;
		this.health = health;

		SetOrigin(radius, radius);

		UpdatePos();

		NoStroke();
	}

	public void ApplyForce(Vec2 force)
	{
		_acc += force;
	}

	public virtual void Recalc()
	{
		UpdatePos();
	}

	private void UpdatePos()
	{
		vel += _acc;
		// vel *= DragFactor * (1f-Time.deltaTime);
		vel *= DragFactor;
		// pos += vel * Time.deltaTime;
		Pos += vel;
		_acc *= 0;

		x = Pos.x;
		y = Pos.y;
	}

	public void TakeDamage(int damage = 1)
	{
		health -= damage;
		if (health <= 0)
		{
			Die();
		}
	}

	protected virtual void Die()
	{
		Destroy();
	}
}
