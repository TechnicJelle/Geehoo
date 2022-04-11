// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using System;
using GXPEngine.Core;
using NUnit.Framework;

namespace Tests;

public class TestVec2
{
	/// <summary>
	/// When comparing values, the values can be off by this much in either direction before it gets flagged as actually two different numbers
	/// </summary>
	private const float TOLERANCE = 0.00001f;

	[SetUp]
	public void Setup()
	{
	}

	/// <summary>
	/// Verifies that two Vec2 are equal considering a delta.
	/// </summary>
	/// <param name="expected">The expected value</param>
	/// <param name="actual">The actual value</param>
	/// <param name="delta">The maximum acceptable difference between the the expected and the actual</param>
	private static void Vec2AreEqual(Vec2 expected, Vec2 actual, float delta = TOLERANCE)
	{
		Assert.AreEqual(expected.x, actual.x, delta);
		Assert.AreEqual(expected.y, actual.y, delta);
	}

	[Test]
	public void TestConstructors()
	{
		Vec2AreEqual(new Vec2(0.0f, 0.0f), new Vec2());
		Vec2AreEqual(new Vec2(0.0, 0.0), new Vec2());
		Vec2AreEqual(new Vec2(0, 0), new Vec2());

		Vec2AreEqual(new Vec2(0.0f), new Vec2());
		Vec2AreEqual(new Vec2(0.0), new Vec2());
		Vec2AreEqual(new Vec2(0), new Vec2());
	}

	[Test]
	public void TestFromAngle()
	{
		Vec2AreEqual(new Vec2(1, 0), Vec2.FromAngle(Angle.ZERO));
		Vec2AreEqual(new Vec2(0, 1), Vec2.FromAngle(Angle.HALF_PI));
		Vec2AreEqual(new Vec2(-1, 0), Vec2.FromAngle(Angle.PI));
		Vec2AreEqual(new Vec2(0, -1), Vec2.FromAngle(Angle.PI + Angle.HALF_PI));

		Vec2AreEqual(new Vec2(0.70710677f, 0.70710677f), Vec2.FromAngle(Angle.QUARTER_PI));
		Vec2AreEqual(new Vec2(0.70710677f, -0.70710677f), Vec2.FromAngle(-Angle.QUARTER_PI));
	}

	[Test]
	public void TestRandom()
	{
		for (int i = 0; i < 100; i++)
		{
			Vec2 randomUnitVector = Vec2.Random();
			Console.WriteLine($"v: {randomUnitVector}, length: {randomUnitVector.Length()}");
		}

		// Assert.Inconclusive();
		Assert.Ignore();
	}

	[Test]
	// ReSharper disable once InconsistentNaming
	public void TestSetXY()
	{
		Vec2AreEqual(new Vec2(1,0), new Vec2().SetXY(1, 0));
		Vec2AreEqual(new Vec2(10,0), new Vec2().SetXY(10, 0));
		Vec2AreEqual(new Vec2(-10,0), new Vec2().SetXY(-10, 0));

		Vec2AreEqual(new Vec2(0,1), new Vec2().SetXY(0,1));
		Vec2AreEqual(new Vec2(0,10), new Vec2().SetXY(0,10));
		Vec2AreEqual(new Vec2(0,-10), new Vec2().SetXY(0,-10));
	}

	[Test]
	public void TestLength()
	{
		for (int i = 0; i < 1e4; i++) //repeat it a bunch of times to see if the speed is worse than LengthSq
		{
			Assert.AreEqual(0, new Vec2(0, 0).Length(), TOLERANCE);
			Assert.AreEqual(1, new Vec2(1, 0).Length(), TOLERANCE);
			Assert.AreEqual(1, new Vec2(-1, 0).Length(), TOLERANCE);
			Assert.AreEqual(1, new Vec2(0, 1).Length(), TOLERANCE);
			Assert.AreEqual(1, new Vec2(0, -1).Length(), TOLERANCE);

			Assert.AreEqual(5, new Vec2(-3, -4).Length(), TOLERANCE);
			Assert.AreEqual(4, new Vec2(0, 4).Length(), TOLERANCE);
		}
	}

	[Test]
	public void TestLengthSq()
	{
		for (int i = 0; i < 1e4; i++) //repeat it a bunch of times to see if the speed is better than Length
		{
			Assert.AreEqual(0, new Vec2(0, 0).LengthSq(), TOLERANCE);
			Assert.AreEqual(1, new Vec2(1, 0).LengthSq(), TOLERANCE);
			Assert.AreEqual(1, new Vec2(-1, 0).LengthSq(), TOLERANCE);
			Assert.AreEqual(1, new Vec2(0, 1).LengthSq(), TOLERANCE);
			Assert.AreEqual(1, new Vec2(0, -1).LengthSq(), TOLERANCE);

			Assert.AreEqual(25, new Vec2(-3, -4).LengthSq(), TOLERANCE);
			Assert.AreEqual(16, new Vec2(0, 4).LengthSq(), TOLERANCE);
		}
	}

	[Test]
	public void TestNormalized()
	{
		void TestNormalizedSingular(float startX, float startY, float normX, float normY)
		{
			Vec2 vec2 = new(startX, startY);
			Vec2AreEqual(vec2.Normalized(), new Vec2(normX, normY));
			Vec2AreEqual(vec2, new Vec2(startX, startY)); //To make sure the vector wasn't modified
		}

		TestNormalizedSingular(0, 0, 0, 0);
		TestNormalizedSingular(1, 0, 1, 0);
		TestNormalizedSingular(0, 1, 0, 1);
		TestNormalizedSingular(-1, 0, -1, 0);
		TestNormalizedSingular(0, -1, 0, -1);

		TestNormalizedSingular(0, 10, 0, 1);
		TestNormalizedSingular(0, -10, 0, -1);
		TestNormalizedSingular(10, 0, 1, 0);
		TestNormalizedSingular(-10, 0, -1, 0);

		TestNormalizedSingular(1, 1, 0.70710677f, 0.70710677f);
		TestNormalizedSingular(-1, 1, -0.70710677f, 0.70710677f);
	}

	[Test]
	public void TestNormalize()
	{
		Vec2AreEqual(new Vec2(-3, 4).Normalize(), new Vec2(-0.6, 0.8));
		Vec2AreEqual(new Vec2(0, 4f).Normalize(), new Vec2(0, 1));
		Vec2AreEqual(new Vec2(0, 0).Normalize(), new Vec2(0, 0));
		Vec2AreEqual(new Vec2(0.5, 0).Normalize(), new Vec2(1, 0));
	}

	[Test]
	public void TestSetLength()
	{
		Vec2AreEqual(new Vec2(1, 0), new Vec2(10, 0).SetLength(1));
		Vec2AreEqual(new Vec2(-1, 0), new Vec2(-10, 0).SetLength(1));
		Vec2AreEqual(new Vec2(2, 0), new Vec2(10, 0).SetLength(2));
		Vec2AreEqual(new Vec2(0.1, 0), new Vec2(10, 0).SetLength(0.1f));
	}

	[Test]
	public void TestLimit()
	{
		Vec2AreEqual(new Vec2(0.70710677f, 0.70710677f), new Vec2(10, 10).Limit(1));
		Vec2AreEqual(new Vec2(-0.70710677f, 0.70710677f), new Vec2(-10, 10).Limit(1));
		Vec2AreEqual(new Vec2(0, 1), new Vec2(0, 10).Limit(1));
		Vec2AreEqual(new Vec2(0, 2), new Vec2(0, 2).Limit(10));
	}

	[Test]
	public void TestSetAngle()
	{
		Vec2AreEqual(new Vec2(1, 0), new Vec2(1, 0).SetAngle(Angle.ZERO));

		Vec2AreEqual(new Vec2(0, 1), new Vec2(1, 0).SetAngle(Angle.HALF_PI));
		Vec2AreEqual(new Vec2(0, 10), new Vec2(0, -10).SetAngle(Angle.HALF_PI));
		Vec2AreEqual(new Vec2(-10, 0), new Vec2(0, -10).SetAngle(Angle.PI));

		Vec2AreEqual(new Vec2(0, -10), new Vec2(0, -10).SetAngle(-Angle.HALF_PI));
		Vec2AreEqual(new Vec2(0, 10), new Vec2(0, -10).SetAngle(Angle.HALF_PI + Angle.TWO_PI));
	}

	[Test]
	public void TestGetAngle()
	{
		Assert.AreEqual(Angle.ZERO, new Vec2(0, 0).GetAngle());
		Assert.AreEqual(Angle.ZERO, new Vec2(1, 0).GetAngle());

		Assert.AreEqual(Angle.HALF_PI, new Vec2(0, 1).GetAngle());
		Assert.AreEqual(Angle.PI, new Vec2(-1, 0).GetAngle());

		Assert.AreEqual((Angle.PI + Angle.QUARTER_PI).GetRadians(), new Vec2(-1, -1).GetAngle().GetRadians());
		const float delta = 0.000001f;
		Assert.AreEqual((Angle.TWO_PI - Angle.HALF_PI - Angle.QUARTER_PI).GetRadians(), new Vec2(-1, -1).GetAngle().GetRadians(), TOLERANCE);

		//Makes sure the results are in the valid ranges
		for (float i = -1.0f; i < 1.0f; i += 0.1f)
		{
			for (float j = -1.0f; j < 1.0f; j += 0.1f)
			{
				Assert.GreaterOrEqual(new Vec2(i, j).GetAngle().GetRadians(), Angle.ZERO.GetTotalDegrees());
				Assert.Less(new Vec2(i, j).GetAngle().GetRadians(), Angle.TWO_PI.GetTotalDegrees());
			}
		}
	}

	[Test]
	public void TestRotate()
	{
		Vec2AreEqual(new Vec2(0, 0), new Vec2(0, 0).Rotate(Angle.ZERO));
		Vec2AreEqual(new Vec2(-1, 0), new Vec2(1, 0).Rotate(Angle.PI));
		Vec2AreEqual(new Vec2(0, 1), new Vec2(1, 0).Rotate(Angle.HALF_PI));
		Vec2AreEqual(new Vec2(0, -1), new Vec2(1, 0).Rotate(-Angle.HALF_PI));
		Vec2AreEqual(new Vec2(1, 0), new Vec2(1, 0).Rotate(Angle.TWO_PI));
		Vec2AreEqual(new Vec2(1, 0), new Vec2(1, 0).Rotate(2*Angle.TWO_PI));
		Vec2AreEqual(new Vec2(1, 0), new Vec2(1, 0).Rotate(-2*Angle.TWO_PI));
		Vec2AreEqual(new Vec2(7.07, 7.07), new Vec2(10, 0).Rotate(Angle.QUARTER_PI), 0.1f);
		Vec2AreEqual(new Vec2(7.07, -7.07), new Vec2(10, 0).Rotate(-Angle.QUARTER_PI), 0.1f);
	}

	[Test]
	public void TestRotateAround()
	{
		Vec2AreEqual(new Vec2(-3, 3), new Vec2(4,6).RotateAround(new Vec2(2, 1), Angle.HALF_PI));
	}

	[Test]
	public void TestGetAbs()
	{
		Vec2AreEqual(new Vec2(1,0), new Vec2(1, 0).GetAbs());
		Vec2AreEqual(new Vec2(1,0), new Vec2(-1, 0).GetAbs());
		Vec2AreEqual(new Vec2(0,1), new Vec2(0, 1).GetAbs());
		Vec2AreEqual(new Vec2(0,1), new Vec2(0, -1).GetAbs());

		Vec2AreEqual(new Vec2(0.1,0), new Vec2(0.1f, 0f).GetAbs());
		Vec2AreEqual(new Vec2(0.1,0), new Vec2(-0.1f, 0f).GetAbs());
		Vec2AreEqual(new Vec2(0,0.1), new Vec2(0f, 0.1f).GetAbs());
		Vec2AreEqual(new Vec2(0,0.1), new Vec2(0f, -0.1f).GetAbs());
	}

	[Test]
	public void TestNormal()
	{
		void TestNormalSingular(float x, float y)
		{
			Vec2AreEqual(new Vec2(x, y).Rotate(Angle.HALF_PI), new Vec2(x, y).GetNormal());
		}

		TestNormalSingular(2, 5);
		TestNormalSingular(0, 0);
		TestNormalSingular(-2, 0.5f);
	}

	[Test]
	public void TestDist()
	{
		Assert.AreEqual(1, new Vec2(0, 0).Dist(new Vec2(1, 0)));
		Assert.AreEqual(1, new Vec2(0, 0).Dist(new Vec2(-1, 0)));
		Assert.AreEqual(1, new Vec2(0, 0).Dist(new Vec2(0, 1)));
		Assert.AreEqual(1, new Vec2(0, 0).Dist(new Vec2(0, -1)));

		Assert.AreEqual(1, Vec2.Dist(new Vec2(0, 0), new Vec2(1, 0)));
		Assert.AreEqual(1, Vec2.Dist(new Vec2(0, 0), new Vec2(-1, 0)));
		Assert.AreEqual(1, Vec2.Dist(new Vec2(0, 0), new Vec2(0, 1)));
		Assert.AreEqual(1, Vec2.Dist(new Vec2(0, 0), new Vec2(0, -1)));

		Assert.AreEqual(6.708204f, Vec2.Dist(new Vec2(2, 4), new Vec2(5, 10)), TOLERANCE);
		Assert.AreEqual(3.026549f, Vec2.Dist(new Vec2(-2, 0.4), new Vec2(1, 0)), TOLERANCE);
		Assert.AreEqual(44.701813f, Vec2.Dist(new Vec2(-0.11, 56), new Vec2(-8, 100)), TOLERANCE);
		Assert.AreEqual(0.0f, Vec2.Dist(new Vec2(3, 3),new Vec2(3, 3)), TOLERANCE);
	}

	[Test]
	public void TestDistSq()
	{
		Assert.AreEqual(1, new Vec2(0, 0).DistSq(new Vec2(1, 0)));
		Assert.AreEqual(1, new Vec2(0, 0).DistSq(new Vec2(-1, 0)));
		Assert.AreEqual(1, new Vec2(0, 0).DistSq(new Vec2(0, 1)));
		Assert.AreEqual(1, new Vec2(0, 0).DistSq(new Vec2(0, -1)));

		Assert.AreEqual(1, Vec2.DistSq(new Vec2(0, 0), new Vec2(1, 0)));
		Assert.AreEqual(1, Vec2.DistSq(new Vec2(0, 0), new Vec2(-1, 0)));
		Assert.AreEqual(1, Vec2.DistSq(new Vec2(0, 0), new Vec2(0, 1)));
		Assert.AreEqual(1, Vec2.DistSq(new Vec2(0, 0), new Vec2(0, -1)));

		Assert.AreEqual(45f, Vec2.DistSq(new Vec2(2, 4), new Vec2(5, 10)), TOLERANCE);
		Assert.AreEqual(9.159999f, Vec2.DistSq(new Vec2(-2, 0.4), new Vec2(1, 0)), TOLERANCE);
		Assert.AreEqual(1998.2520854870f, Vec2.DistSq(new Vec2(-0.11, 56), new Vec2(-8, 100)), TOLERANCE);
		Assert.AreEqual(0.0f, Vec2.DistSq(new Vec2(3, 3),new Vec2(3, 3)), TOLERANCE);
	}

	[Test]
	public void TestDot()
	{
		Assert.AreEqual(50.0f, new Vec2(2, 4).Dot(new Vec2(5, 10)));
		Assert.AreEqual(-2.0f, new Vec2(-2, 0.4).Dot(new Vec2(1, 0)));
		Assert.AreEqual(5600.87988f, new Vec2(-0.11, 56).Dot(new Vec2(-8, 100)));
		Assert.AreEqual(18.0f, new Vec2(3, 3).Dot(new Vec2(3, 3)));
	}

	[Test]
	public void TestCross()
	{
		Assert.AreEqual(0f, new Vec2(2, 4).Cross(new Vec2(5, 10)));
		Assert.AreEqual(-0.4f, new Vec2(-2, 0.4).Cross(new Vec2(1, 0)));
		Assert.AreEqual(437f, new Vec2(-0.11, 56).Cross(new Vec2(-8, 100)));
		Assert.AreEqual(0f, new Vec2(3, 3).Cross(new Vec2(3, 3)));
	}

	[Test]
	public void TestOperatorAdd()
	{
		Vec2AreEqual(new Vec2(3f, 3f), new Vec2(1f, 1f) + new Vec2(2f, 2f));
		Vec2AreEqual(new Vec2(5f, -1f), new Vec2(0f, 1f) + new Vec2(5f, -2f));
		Vec2AreEqual(new Vec2(-1.1f, 49f), new Vec2(-1f, -1f) + new Vec2(-0.1f, 50f));
	}

	[Test]
	public void TestOperatorUnaryNegation()
	{
		Vec2AreEqual(new Vec2(2f, 2f).Rotate(Angle.PI), -new Vec2(2f, 2f));
		Vec2AreEqual(new Vec2(5f, -2f).Rotate(Angle.TWO_PI - Angle.PI), -new Vec2(5f, -2f));
		Vec2AreEqual(new Vec2(-0.1f, 50f).GetNormal().GetNormal(), -new Vec2(-0.1f, 50f));
	}

	[Test]
	public void TestOperatorSub()
	{
		Vec2AreEqual(new Vec2(-1f, -1f), new Vec2(1f, 1f) - new Vec2(2f, 2f));
		Vec2AreEqual(new Vec2(-5f, 3f), new Vec2(0f, 1f) - new Vec2(5f, -2f));
		Vec2AreEqual(new Vec2(-0.9f, -51f), new Vec2(-1f, -1f) - new Vec2(-0.1f, 50f));
	}

	[Test]
	public void TestOperatorMult()
	{
		//Vector <-> float
		void TestOperatorMultSingular(Vec2 expected, float f, Vec2 vec)
		{
			Vec2AreEqual(expected, vec * f); //check both ways
			Vec2AreEqual(expected, f * vec);
		}

		TestOperatorMultSingular(new Vec2(0.8f, 0.8f), 0.8f, new Vec2(1f, 1f));
		TestOperatorMultSingular(new Vec2(0.0f, -3.0f), -3.0f, new Vec2(0f, 1f));
		TestOperatorMultSingular(new Vec2(0.0f, 0.0f), 0.0f, new Vec2(-1f, -1f));

		//Element-wise Multiplication Vector <-> Vector
		Vec2AreEqual(new Vec2(2f, 2f), new Vec2(1f, 1f) * new Vec2(2f, 2f));
		Vec2AreEqual(new Vec2(0f, -2f), new Vec2(0f, 1f) * new Vec2(5f, -2f));
		Vec2AreEqual(new Vec2(0.1f, -50f), new Vec2(-1f, -1f) * new Vec2(-0.1f, 50f));
	}

	[Test]
	public void TestOperatorDiv()
	{
		Vec2AreEqual(new Vec2(1f, 1f), new Vec2(2f, 2f) / 2);
		Vec2AreEqual(new Vec2(-2, -2), new Vec2(-10f, -10f) / 5);
		Assert.Throws<AssertionException>(() => { Vec2AreEqual(new Vec2(0, 0), new Vec2(-10f, -10f) / 0); });
		Vec2AreEqual(new Vec2(0, 0), new Vec2(0, 0) / 10);


		Vec2AreEqual(new Vec2(0.0f, -0.5f), new Vec2(0f, 1f) / new Vec2(5f, -2f));
		Vec2AreEqual(new Vec2(10.0f, -0.02f), new Vec2(-1f, -1f) / new Vec2(-0.1f, 50f));

		Vec2AreEqual(new Vec2(2.5, 1.25), 5f / new Vec2(2, 4));
	}

	[Test]
	public void TestOperatorEquals()
	{
		Assert.AreEqual(true, new Vec2(1.0f, 1.0f) == new Vec2(1, 1));
		Assert.AreNotEqual(true, new Vec2(1.0f, 1.0f) == new Vec2(99, 99));
		Assert.AreEqual(true, new Vec2(-1, 1) == new Vec2(-1.0, 1.0));
		Assert.AreEqual(true, new Vec2(-0.1f, 0.1f) == new Vec2(-0.1, 0.1));
	}

	[Test]
	public void TestOperatorNotEquals()
	{
		Assert.AreNotEqual(true, new Vec2(1.0f, 1.0f) != new Vec2(1, 1));
		Assert.AreEqual(true, new Vec2(1.0f, 1.0f) != new Vec2(99, 99));
		Assert.AreNotEqual(true, new Vec2(-1, 1) != new Vec2(-1.0, 1.0));
		Assert.AreNotEqual(true, new Vec2(-0.1f, 0.1f) != new Vec2(-0.1, 0.1));
	}

	[Test]
	public void TestEquals()
	{
		Assert.AreEqual(false, new Vec2().Equals(new object()));
		Assert.AreEqual(false, new Vec2().Equals(null));

		Assert.AreEqual(true, new Vec2(1, 0).Equals(new Vec2(1.0f, 0)));
		Assert.AreEqual(true, new Vec2(10, 0).Equals(new Vec2(10.0f, 0)));
		Assert.AreEqual(true, new Vec2(-1, 0).Equals(new Vec2(-1.0f, 0)));
		Assert.AreEqual(true, new Vec2(-10, 0).Equals(new Vec2(-10.0f, 0)));

		Assert.AreEqual(true, new Vec2(0, 1).Equals(new Vec2(0, 1.0f)));
		Assert.AreEqual(true, new Vec2(0, 10).Equals(new Vec2(0, 10.0f)));
		Assert.AreEqual(true, new Vec2(0, -1).Equals(new Vec2(0, -1.0f)));
		Assert.AreEqual(true, new Vec2(0, -10).Equals(new Vec2(0, -10.0f)));
	}

	[Test]
	public void TestGetHashCode()
	{
		Assert.AreEqual(new Vec2(1, 1).GetHashCode(), new Vec2(1.0f, 1.0f).GetHashCode());
		Assert.AreNotEqual(new Vec2(0, 0).GetHashCode(), new Vec2(0.01f, 0.01f).GetHashCode());
		Assert.AreEqual(new Vec2(1, 1).GetHashCode(), new Vec2(1.0f, 1.0f).GetHashCode());
	}

	[Test]
	public void TestToString()
	{
		Assert.AreEqual("(1,0)", new Vec2(1, 0).ToString());
		Assert.AreEqual("(-1,0)", new Vec2(-1, 0).ToString());
		Assert.AreEqual("(0,1)", new Vec2(0, 1).ToString());
		Assert.AreEqual("(0,-1)", new Vec2(0, -1).ToString());

		Assert.AreEqual("(0.1,0)", new Vec2(0.1f, 0f).ToString());
		Assert.AreEqual("(-0.1,0)", new Vec2(-0.1f, 0f).ToString());
		Assert.AreEqual("(0,0.1)", new Vec2(0f, 0.1f).ToString());
		Assert.AreEqual("(0,-0.1)", new Vec2(0f, -0.1f).ToString());
	}
}
