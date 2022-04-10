// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using GXPEngine;
using GXPEngine.Core;

namespace Geehoo.MyGame;

public class LineSegment
{
	public Vec2 start { get; }
	public Vec2 end { get; }

	public LineSegment(Vec2 start, Vec2 end)
	{
		this.start = start;
		this.end = end;
	}

	public void Draw()
	{
		Gizmos.DrawLine(start.x, start.y, end.x, end.y);
	}

	public Vec2 GetDir()
	{
		return end - start;
	}

	public static (Vec2? intersectionPoint, float? toi) Intersect(LineSegment a, LineSegment b)
	{
		Vec2 av = a.GetDir();
		Vec2 bv = b.GetDir();
		float d = Vec2.Cross(av, bv);
		if (d == 0) return (null, null);

		Vec2 dir = a.start - b.start;
		float ua = Vec2.Cross(bv, dir) / d;
		float ub = Vec2.Cross(av, dir) / d;

		if (ua is < 0 or > 1 || ub is < 0 or > 1) return (null, null);

		return (a.start + av * ua, ua);
	}

	public static (Vec2? intersectionPoint, Vec2? reflectionVector) Reflect(LineSegment dynamic, LineSegment @static)
	{
		(dynamic intersectionPoint, dynamic toi) = Intersect(dynamic, @static);
		if (intersectionPoint == null || toi == null) return (null, null);
		Angle diff = Angle.Difference(dynamic.GetDir().GetAngle(), @static.GetDir().GetAngle());
		Vec2 reflected = Vec2.FromAngle(@static.GetDir().GetAngle() + diff) * dynamic.GetDir().Length() * (1f - toi);
		return (intersectionPoint, reflected);
	}
}
