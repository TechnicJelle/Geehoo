// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class Player : EasyDraw
{
	//TODO: Make a base class for both enemies and player
	public Vec2 pos => new(x, y);
	public float radius { get; }
	public float coreRadius { get; }

	public int health { get; private set; }

	private const int SIZE = 48;
	private const float CORE_SIZE = 12;
	private readonly Vec2 _relativeCorePosition;

	private const float SPEED = 0.3f;
	private const int SHOOT_DELAY = 100;
	private const float PROJECTILE_SPEED = 0.5f;

	private int _millisAtLastShot;

	public Player(float x, float y) : base(SIZE, SIZE, false)
	{
		radius = SIZE / 2f;
		coreRadius = CORE_SIZE / 2f;
		health = 3;

		SetOrigin(radius, radius);

		this.x = x;
		this.y = y;

		NoStroke();

		//body
		Fill(64);
		//       MIDDLE TOP ,      BOTTOM RIGHT,  MIDDLE BOTTOM,               LEFT BOTTOM
		Quad(radius, 0, SIZE, SIZE, radius, SIZE * 0.8f, 0, SIZE);

		//core
		Fill(64, 64, 255);
		_relativeCorePosition = new Vec2(0, radius*0.2f);
		Ellipse(radius + _relativeCorePosition.x, radius + _relativeCorePosition.y, CORE_SIZE, CORE_SIZE);
	}

	public Vec2 GetCorePosition()
	{
		Vec2 corePosition = _relativeCorePosition;
		corePosition.Rotate(Angle.FromDegrees(rotation));
		return new Vec2(x + corePosition.x, y + corePosition.y);
	}

	public void Recalc()
	{
		Vec2 newPos = new();
		if (Input.GetKey(Key.LEFT) || Input.GetKey(Key.A)) newPos.x -= 1;
		if (Input.GetKey(Key.RIGHT) || Input.GetKey(Key.D)) newPos.x += 1;
		if (Input.GetKey(Key.UP) || Input.GetKey(Key.W)) newPos.y -= 1;
		if (Input.GetKey(Key.DOWN) || Input.GetKey(Key.S)) newPos.y += 1;
		newPos.Normalize();
		newPos *= SPEED * Time.deltaTime;

		x += newPos.x;
		y += newPos.y;

		Vec2 mousePos = new(Input.mouseX, Input.mouseY);
		Vec2 pos = new(x, y);
		Vec2 direction = mousePos - pos;

		rotation = direction.GetNormal().GetAngle().GetTotalDegrees();

		if (Input.GetMouseButton(0) && Time.time - _millisAtLastShot > SHOOT_DELAY)
		{
			_millisAtLastShot = Time.time;
			Vec2 dir = direction;
			dir.SetLength(PROJECTILE_SPEED);

			Vec2 spawnPos = direction;
			spawnPos.SetLength(radius);
			spawnPos += GetCorePosition();
			game.AddChild(new Projectile(spawnPos, dir, 5, false));
		}
	}

	public void TakeDamage(int damage = 1)
	{
		health -= damage;
		if (health <= 0)
		{
			Destroy();
		}
	}
}
