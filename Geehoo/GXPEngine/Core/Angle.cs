// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using System;

// ReSharper disable MemberCanBePrivate.Global

namespace GXPEngine.Core;

public readonly struct Angle
{
	/// <summary>
	/// When comparing values, the values can be off by this much in either direction before it gets flagged as actually two different numbers
	/// </summary>
	private const float TOLERANCE = 0.0000001f;

	public const float PI = (float) Math.PI;
	public const float HALF_PI = (float) (Math.PI / 2.0);
	public const float THIRD_PI = (float) (Math.PI / 3.0);
	public const float QUARTER_PI = (float) (Math.PI / 4.0);
	public const float TWO_PI = (float) (2.0 * Math.PI);

	private readonly float _totalRadians;

	private Angle(float totalRadians)
	{
		_totalRadians = totalRadians;
	}

	public static Angle FromRadians(float radians)
	{
		return new Angle(radians);
	}

	public static Angle FromDegrees(float degrees)
	{
		return new Angle(Deg2Rad(degrees));
	}

	/// <returns>An angle in radians between 0 and TWO_PI</returns>
	public float GetRadians()
	{
		return Mathf.Wrap(GetTotalRadians(), TWO_PI);
	}

	/// <returns>An angle in radians between -PI and PI</returns>
	public float GetRadiansWithNegative()
	{
		return Mathf.Wrap(GetTotalRadians() + PI, TWO_PI) - PI;
	}

	/// <returns>The actual angle in radians. Can be less than 0 and more than TWO_PI!</returns>
	public float GetTotalRadians()
	{
		return _totalRadians;
	}

	/// <returns>float degrees (0..360)</returns>
	public float GetDegrees()
	{
		return Rad2Deg(GetRadians());
	}

	/// <returns>float degrees (-180..180)</returns>
	public float GetDegreesWithNegative()
	{
		return Rad2Deg(GetRadiansWithNegative());
	}

	/// <returns>The actual angle in degrees. Can be less than 0 and more than TWO_PI!</returns>
	public float GetTotalDegrees()
	{
		return Rad2Deg(GetTotalRadians());
	}

	private const float RAD_TO_DEG = 180.0f / PI;
	private const float DEG_TO_RAD = PI / 180.0f;

	/// <summary>
	/// Converts the given radians to degrees
	/// </summary>
	public static float Rad2Deg(float radians)
	{
		return radians * RAD_TO_DEG;
	}

	/// <summary>
	/// Converts the given degrees to radians
	/// </summary>
	public static float Deg2Rad(float degrees)
	{
		return degrees * DEG_TO_RAD;
	}

	/// <summary>
	/// Calculates the difference between two angles
	/// </summary>
	public static Angle Difference(Angle angle1, Angle angle2)
	{
		//TODO: Improve this
		float diff = (angle2.GetDegrees() - angle1.GetDegrees() + 180f) % 360f - 180f;
		return FromDegrees(diff < -180f ? diff + 360f : diff);
	}

	public static Angle operator +(Angle left, Angle right)
	{
		return new Angle(left.GetTotalRadians() + right.GetTotalRadians());
	}

	public static Angle operator -(Angle angle)
	{
		return new Angle(-angle.GetTotalRadians());
	}

	public static Angle operator -(Angle left, Angle right)
	{
		return new Angle(left.GetTotalRadians() + right.GetTotalRadians());
	}

	public static Angle operator *(Angle angle, float f)
	{
		return new Angle(angle.GetTotalRadians() * f);
	}

	public static Angle operator *(float f, Angle angle)
	{
		return new Angle(f * angle.GetTotalRadians());
	}

	public static Angle operator *(Angle left, Angle right)
	{
		return new Angle(left.GetTotalRadians() * right.GetTotalRadians());
	}

	public static Angle operator /(Angle angle, float f)
	{
		return new Angle(angle.GetTotalRadians() / f);
	}

	public static Angle operator /(float f, Angle angle)
	{
		return new Angle(f / angle.GetTotalRadians());
	}

	public static Angle operator /(Angle left, Angle right)
	{
		return new Angle(left.GetTotalRadians() / right.GetTotalRadians());
	}

	public static bool operator ==(Angle left, Angle right)
	{
		return Math.Abs(left.GetTotalRadians() - right.GetTotalRadians()) < TOLERANCE;
	}

	public static bool operator !=(Angle left, Angle right)
	{
		return Math.Abs(left.GetTotalRadians() - right.GetTotalRadians()) > TOLERANCE;
	}

	public override bool Equals(object obj)
	{
		if (obj is not Angle angle)
			return false;
		return Math.Abs(GetTotalRadians() - angle.GetTotalRadians()) < TOLERANCE;
	}

	public override int GetHashCode()
	{
		return GetTotalRadians().GetHashCode();
	}

	public override string ToString()
	{
		return $"{GetTotalRadians()} radians";
	}
}
