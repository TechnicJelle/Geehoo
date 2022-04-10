// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class Player : Entity
{
	private const int SIZE = 48;
	private const float CORE_SIZE = 10;

	private const float MOVE_FORCE = 0.1f;
	private const float DRAG = 0.9f;

	private const int SHOOT_DELAY = 100;
	private const float PROJECTILE_SPEED = 3f;

	private int _millisAtLastShot;

	public float coreRadius { get; }
	private readonly Vec2 _relativeCorePosition;

	public Player(float x, float y) : base(x, y, SIZE, 3)
	{
		DragFactor = DRAG;
		coreRadius = CORE_SIZE / 2f;

		//body
		Fill(64);
		//       MIDDLE TOP ,      BOTTOM RIGHT,  MIDDLE BOTTOM,               LEFT BOTTOM
		Quad(radius, 0, Size, Size, radius, Size * 0.8f, 0, Size);

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

	public override void Recalc()
	{
		//movement
		Vec2 input = new();
		if (Input.GetKey(Key.LEFT) || Input.GetKey(Key.A)) input.x -= 1;
		if (Input.GetKey(Key.RIGHT) || Input.GetKey(Key.D)) input.x += 1;
		if (Input.GetKey(Key.UP) || Input.GetKey(Key.W)) input.y -= 1;
		if (Input.GetKey(Key.DOWN) || Input.GetKey(Key.S)) input.y += 1;
		input.Normalize();
		input *= MOVE_FORCE;
		ApplyForce(input);

		//rotation
		Vec2 mousePos = new(Input.mouseX, Input.mouseY);
		Vec2 direction = mousePos - Pos;

		rotation = direction.GetNormal().GetAngle().GetTotalDegrees();

		//shooting
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
		base.Recalc();
	}
}
