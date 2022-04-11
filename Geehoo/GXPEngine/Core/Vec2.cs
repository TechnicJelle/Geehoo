// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
//  Except for the functions in this file that are either inspired by, or taken from Processing: https://github.com/processing/processing4/blob/master/core/src/processing/core/PVector.java
// You're allowed to learn from this, but please do not simply copy.

using System;
using System.Diagnostics.CodeAnalysis;

namespace GXPEngine.Core;

public struct Vec2
{
	//TODO: Polish up XML documentation

	// ReSharper disable InconsistentNaming
	public float x;
	public float y;
	// ReSharper restore InconsistentNaming

	/// <summary>
	/// When comparing values, the values can be off by this much in either direction before it gets flagged as actually two different numbers
	/// </summary>
	private const float TOLERANCE = 0.0000001f;

	/// <summary>
	/// Constructs a new vector (defaults to (0, 0) )
	/// </summary>
	/// <param name="pX">new x position</param>
	/// <param name="pY">new y position</param>
	public Vec2(float pX = 0.0f, float pY = 0.0f)
	{
		x = pX;
		y = pY;
	}

	public Vec2(double pX = 0.0, double pY = 0.0) : this((float) pX, (float) pY)
	{
	}

	public Vec2(int pX = 0, int pY = 0) : this((float) pX, (float) pY)
	{
	}

	/// <summary>
	/// Constructs a new vector with the same number for both of the elements
	/// </summary>
	public Vec2(float f) : this(f, f)
	{
	}

	/// <summary>
	/// Constructs a new vector with the same number for both of the elements
	/// </summary>
	public Vec2(double d) : this(d, d)
	{
	}

	/// <summary>
	/// Constructs a new vector with the same number for both of the elements
	/// </summary>
	public Vec2(int i) : this(i, i)
	{
	}

	/// <summary>
	/// Returns a new vector pointing in the given direction
	/// </summary>
	public static Vec2 FromAngle(Angle angle)
	{
		return new Vec2(Math.Cos(angle),  Math.Sin(angle));
	}

	/// <summary>
	/// Returns a new unit vector pointing in a random direction
	/// </summary>
	public static Vec2 Random()
	{
		return FromAngle(Angle.Random());
	}

	/// <summary>
	/// Set the vector to a different point
	/// </summary>
	/// <param name="x">new x position</param>
	/// <param name="y">new y position</param>
	[SuppressMessage("ReSharper", "ParameterHidesMember")]
	// ReSharper disable once InconsistentNaming
	public Vec2 SetXY(float x = 0.0f, float y = 0.0f)
	{
		this.x = x;
		this.y = y;
		return this;
	}

	/// <summary>
	/// Calculates the magnitude of the vector (using sqrt)
	/// </summary>
	/// <returns>The magnitude of the vector</returns>
	public float Length()
	{
		return (float) Math.Sqrt(x * x + y * y);
	}

	/// <summary>
	/// Calculates the square magnitude of the vector (so there is no slow sqrt() being called)
	/// </summary>
	/// <returns>The square magnitude of the vector</returns>
	public float LengthSq()
	{
		return x * x + y * y;
	}

	/// <summary>
	/// Calculates a normalized version of the vector
	/// </summary>
	/// <returns>A normalized copy of the vector</returns>
	public Vec2 Normalized()
	{
		float length = Length();
		return length == 0 ? new Vec2() : new Vec2(x / length, y / length);
	}

	/// <summary>
	/// Modifies the vector to be normalized
	/// </summary>
	/// <returns>The normalized vector</returns>
	public Vec2 Normalize()
	{
		return this = Normalized();
	}

	/// <summary>
	/// Sets the length of this vector
	/// </summary>
	/// <param name="length">The desired length for this vector</param>
	/// <returns>The modified vector</returns>
	public Vec2 SetLength(float length)
	{
		Normalize();
		return this *= length;
	}

	/// <summary>
	/// Limit the length of this vector
	/// </summary>
	/// <param name="max">The maximum length the vector may be</param>
	/// <returns>The modified vector</returns>
	public Vec2 Limit(float max)
	{
		return LengthSq() < max * max ? this : SetLength(max);
	}

	/// <summary>
	/// Set vector angle to the given direction in radians (length doesn't change)
	/// </summary>
	/// <returns>The modified vector</returns>
	public Vec2 SetAngle(Angle angle)
	{
		float m = Length();
		x = (float) (m * Math.Cos(angle));
		y = (float) (m * Math.Sin(angle));
		return this;
	}

	/// <summary>
	/// Gets the vector's angle
	/// </summary>
	public Angle GetAngle()
	{
		return Angle.FromRadians((float) Math.Atan2(y, x));
	}

	/// <summary>
	/// Rotate the vector over the given angle in radians
	/// </summary>
	/// <returns>The modified vector</returns>
	public Vec2 Rotate(Angle angle)
	{
		float temp = x;
		x = (float) (x * Math.Cos(angle) - y * Math.Sin(angle));
		y = (float) (temp * Math.Sin(angle) + y * Math.Cos(angle));
		return this;
	}

	/// <summary>
	/// Rotate the vector around the given point over the given angle in radians
	/// </summary>
	/// <returns></returns>
	public Vec2 RotateAround(Vec2 rotateAround, Angle angle)
	{
		this -= rotateAround;
		Rotate(angle);
		return this += rotateAround;
	}

	/// <summary>
	/// Calculates the absolute components of the vector
	/// </summary>
	/// <returns>A new vector with both components positive</returns>
	public Vec2 GetAbs()
	{
		return new Vec2(Math.Abs(x), Math.Abs(y));
	}

	/// <summary>
	/// Calculates the normal vector on this vector
	/// </summary>
	/// <returns>A new vector that points in the perpendicular direction</returns>
	public Vec2 GetNormal()
	{
		return new Vec2(-y, x);
	}

	/// <summary>
	/// Calculates the distance between this vector and another vector
	/// </summary>
	public float Dist(Vec2 other)
	{
		return Dist(this, other);
	}

	/// <summary>
	/// Calculates the distance between two vectors
	/// </summary>
	public static float Dist(Vec2 v1, Vec2 v2)
	{
		Vec2 d = v1 - v2;
		return d.Length();
	}

	/// <summary>
	/// Calculates the square distance between this vector and another vector (so there is no slow sqrt() being called)
	/// </summary>
	public float DistSq(Vec2 other)
	{
		return DistSq(this, other);
	}

	/// <summary>
	/// Calculates the square distance between two vectors (so there is no slow sqrt() being called)
	/// </summary>
	public static float DistSq(Vec2 v1, Vec2 v2)
	{
		Vec2 d = v1 - v2;
		return d.LengthSq();
	}

	/// <summary>
	/// Calculates the dot product between this vector and another vector
	/// </summary>
	public float Dot(Vec2 v)
	{
		return Dot(this, v);
	}

	/// <summary>
	/// Calculates the dot product between two vectors
	/// </summary>
	public static float Dot(Vec2 v1, Vec2 v2)
	{
		return v1.x * v2.x + v1.y * v2.y;
	}

	/// <summary>
	/// Calculates the cross product between this vector and another vector
	/// </summary>
	public float Cross(Vec2 other)
	{
		return Cross(this, other);
	}

	/// <summary>
	/// Calculates the cross product between two vectors
	/// </summary>
	public static float Cross(Vec2 v1, Vec2 v2)
	{
		return v1.x * v2.y - v1.y * v2.x;
	}

	public static Vec2 operator +(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x + right.x, left.y + right.y);
	}

	public static Vec2 operator -(Vec2 vec)
	{
		return new Vec2(-vec.x, -vec.y);
	}

	public static Vec2 operator -(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x - right.x, left.y - right.y);
	}

	public static Vec2 operator *(Vec2 vec, float f)
	{
		return new Vec2(vec.x * f, vec.y * f);
	}

	public static Vec2 operator *(float f, Vec2 vec)
	{
		return new Vec2(f * vec.x, f * vec.y);
	}

	/// <summary>
	/// Element-wise multiplication
	/// </summary>
	public static Vec2 operator *(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x * right.x, left.y * right.y);
	}

	/// <summary>
	/// Divide a vector by a number (scalar division)
	/// </summary>
	public static Vec2 operator /(Vec2 vec, float f)
	{
		return vec / new Vec2(f);
	}

	/// <summary>
	/// Divide a number by a vector
	/// </summary>
	public static Vec2 operator /(float f, Vec2 vec)
	{
		return new Vec2(f) / vec;
	}

	/// <summary>
	/// Element-wise division
	/// </summary>
	public static Vec2 operator /(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x / right.x, left.y / right.y);
	}

	public static bool operator ==(Vec2 left, Vec2 right)
	{
		return Math.Abs(left.x - right.x) < TOLERANCE && Math.Abs(left.y - right.y) < TOLERANCE;
	}

	public static bool operator !=(Vec2 left, Vec2 right)
	{
		return Math.Abs(left.x - right.x) > TOLERANCE || Math.Abs(left.y - right.y) > TOLERANCE;
	}

	public override bool Equals(object obj)
	{
		if (obj is not Vec2 vec2)
			return false;
		return Math.Abs(x - vec2.x) < TOLERANCE && Math.Abs(y - vec2.y) < TOLERANCE;
	}

	public override int GetHashCode()
	{
		int hash = 17;
		hash = hash * 31 + x.GetHashCode();
		hash = hash * 31 + y.GetHashCode();
		return hash;
	}

	public override string ToString()
	{
		return $"({x},{y})";
	}
}
