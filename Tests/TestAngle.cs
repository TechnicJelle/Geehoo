// Author: TechnicJelle
// Copyright (c) TechnicJelle. All rights reserved.
// You're allowed to learn from this, but please do not simply copy.

using System;
using GXPEngine;
using GXPEngine.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests;

public class TestAngle
{
	/// <summary>
	/// When comparing values, the values can be off by this much in either direction before it gets flagged as actually two different numbers
	/// </summary>
	private const float TOLERANCE = 0.0001f;

	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void TestConstants()
	{
		Assert.AreEqual(Angle.FromRadians(0), Angle.ZERO);
		Assert.AreEqual(Angle.FromRadians(Mathf.PI), Angle.PI);
		Assert.AreEqual(Angle.FromRadians(Mathf.HALF_PI), Angle.HALF_PI);
		Assert.AreEqual(Angle.FromRadians(Mathf.THIRD_PI), Angle.THIRD_PI);
		Assert.AreEqual(Angle.FromRadians(Mathf.QUARTER_PI), Angle.QUARTER_PI);
		Assert.AreEqual(Angle.FromRadians(Mathf.TWO_PI), Angle.TWO_PI);

		Assert.AreEqual(Angle.FromDegrees(0), Angle.ZERO);
		Assert.AreEqual(Angle.FromDegrees(180), Angle.PI);
		Assert.AreEqual(Angle.FromDegrees(90), Angle.HALF_PI);
		Assert.AreEqual(Angle.FromDegrees(60), Angle.THIRD_PI);
		Assert.AreEqual(Angle.FromDegrees(45), Angle.QUARTER_PI);
		Assert.AreEqual(Angle.FromDegrees(360), Angle.TWO_PI);
	}

	[Test]
	public void TestConstructors()
	{
		Assert.AreEqual(new Angle(), Angle.ZERO);
	}

	[Test]
	public void TestRandom()
	{
		for (int i = 0; i < 1e2; i++)
		{
			Angle randomAngle = Angle.Random();
			Console.WriteLine(randomAngle.ToString());
			Assert.IsTrue(randomAngle.GetRadians() >= 0);
			Assert.IsTrue(randomAngle.GetRadians() < Mathf.TWO_PI);
		}
	}

	[Test]
	public void TestGetRadians()
	{
		Assert.AreEqual(Mathf.HALF_PI * 2, Angle.FromRadians(Mathf.HALF_PI * -10).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 3, Angle.FromRadians(Mathf.HALF_PI * -9).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 0, Angle.FromRadians(Mathf.HALF_PI * -8).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 1, Angle.FromRadians(Mathf.HALF_PI * -7).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 2, Angle.FromRadians(Mathf.HALF_PI * -6).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 3, Angle.FromRadians(Mathf.HALF_PI * -5).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 0, Angle.FromRadians(Mathf.HALF_PI * -4).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 1, Angle.FromRadians(Mathf.HALF_PI * -3).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 2, Angle.FromRadians(Mathf.HALF_PI * -2).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 3, Angle.FromRadians(Mathf.HALF_PI * -1).GetRadians(), TOLERANCE);

		Assert.AreEqual(Mathf.HALF_PI * 0, Angle.FromRadians(Mathf.HALF_PI * 0).GetRadians(), TOLERANCE);

		Assert.AreEqual(Mathf.HALF_PI * 1, Angle.FromRadians(Mathf.HALF_PI * 1).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 2, Angle.FromRadians(Mathf.HALF_PI * 2).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 3, Angle.FromRadians(Mathf.HALF_PI * 3).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 0, Angle.FromRadians(Mathf.HALF_PI * 4).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 1, Angle.FromRadians(Mathf.HALF_PI * 5).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 2, Angle.FromRadians(Mathf.HALF_PI * 6).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 3, Angle.FromRadians(Mathf.HALF_PI * 7).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 0, Angle.FromRadians(Mathf.HALF_PI * 8).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 1, Angle.FromRadians(Mathf.HALF_PI * 9).GetRadians(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 2, Angle.FromRadians(Mathf.HALF_PI * 10).GetRadians(), TOLERANCE);
	}

	[Test]
	public void TestGetRadiansWithNegative()
	{
		Assert.AreEqual(Mathf.HALF_PI * -2, Angle.FromRadians(Mathf.HALF_PI * -10).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * -1, Angle.FromRadians(Mathf.HALF_PI * -9).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 0, Angle.FromRadians(Mathf.HALF_PI * -8).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 1, Angle.FromRadians(Mathf.HALF_PI * -7).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * -2, Angle.FromRadians(Mathf.HALF_PI * -6).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * -1, Angle.FromRadians(Mathf.HALF_PI * -5).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 0, Angle.FromRadians(Mathf.HALF_PI * -4).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 1, Angle.FromRadians(Mathf.HALF_PI * -3).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * -2, Angle.FromRadians(Mathf.HALF_PI * -2).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * -1, Angle.FromRadians(Mathf.HALF_PI * -1).GetRadiansWithNegative(), TOLERANCE);

		Assert.AreEqual(Mathf.HALF_PI * 0, Angle.FromRadians(Mathf.HALF_PI * 0).GetRadiansWithNegative(), TOLERANCE);

		Assert.AreEqual(Mathf.HALF_PI * 1, Angle.FromRadians(Mathf.HALF_PI * 1).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * -2, Angle.FromRadians(Mathf.HALF_PI * 2).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * -1, Angle.FromRadians(Mathf.HALF_PI * 3).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 0, Angle.FromRadians(Mathf.HALF_PI * 4).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 1, Angle.FromRadians(Mathf.HALF_PI * 5).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * -2, Angle.FromRadians(Mathf.HALF_PI * 6).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * -1, Angle.FromRadians(Mathf.HALF_PI * 7).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 0, Angle.FromRadians(Mathf.HALF_PI * 8).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 1, Angle.FromRadians(Mathf.HALF_PI * 9).GetRadiansWithNegative(), TOLERANCE);
		Assert.AreEqual(Mathf.HALF_PI * 2, Angle.FromRadians(Mathf.HALF_PI * 10).GetRadiansWithNegative(), TOLERANCE);
	}

	[Test]
	public void TestGetDegrees()
	{
		Assert.AreEqual(270, Angle.FromDegrees(45 * -10).GetDegrees(), TOLERANCE);
		Assert.AreEqual(315, Angle.FromDegrees(45 * -9).GetDegrees(), TOLERANCE);
		Assert.AreEqual(0, Angle.FromDegrees(45 * -8).GetDegrees(), TOLERANCE);
		Assert.AreEqual(45, Angle.FromDegrees(45 * -7).GetDegrees(), TOLERANCE);
		Assert.AreEqual(90, Angle.FromDegrees(45 * -6).GetDegrees(), TOLERANCE);
		Assert.AreEqual(135, Angle.FromDegrees(45 * -5).GetDegrees(), TOLERANCE);
		Assert.AreEqual(180, Angle.FromDegrees(45 * -4).GetDegrees(), TOLERANCE);
		Assert.AreEqual(225, Angle.FromDegrees(45 * -3).GetDegrees(), TOLERANCE);
		Assert.AreEqual(270, Angle.FromDegrees(45 * -2).GetDegrees(), TOLERANCE);
		Assert.AreEqual(315, Angle.FromDegrees(45 * -1).GetDegrees(), TOLERANCE);

		Assert.AreEqual(0, Angle.FromDegrees(45 * 0).GetDegrees(), TOLERANCE);

		Assert.AreEqual(45, Angle.FromDegrees(45 * 1).GetDegrees(), TOLERANCE);
		Assert.AreEqual(90, Angle.FromDegrees(45 * 2).GetDegrees(), TOLERANCE);
		Assert.AreEqual(135, Angle.FromDegrees(45 * 3).GetDegrees(), TOLERANCE);
		Assert.AreEqual(180, Angle.FromDegrees(45 * 4).GetDegrees(), TOLERANCE);
		Assert.AreEqual(225, Angle.FromDegrees(45 * 5).GetDegrees(), TOLERANCE);
		Assert.AreEqual(270, Angle.FromDegrees(45 * 6).GetDegrees(), TOLERANCE);
		Assert.AreEqual(315, Angle.FromDegrees(45 * 7).GetDegrees(), TOLERANCE);
		Assert.AreEqual(0, Angle.FromDegrees(45 * 8).GetDegrees(), TOLERANCE);
		Assert.AreEqual(45, Angle.FromDegrees(45 * 9).GetDegrees(), TOLERANCE);
		Assert.AreEqual(90, Angle.FromDegrees(45 * 10).GetDegrees(), TOLERANCE);
	}

	[Test]
	public void TestGetDegreesWithNegative()
	{
		Assert.AreEqual(-90, Angle.FromDegrees(45 * -10).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(-45, Angle.FromDegrees(45 * -9).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(0, Angle.FromDegrees(45 * -8).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(45, Angle.FromDegrees(45 * -7).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(90, Angle.FromDegrees(45 * -6).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(135, Angle.FromDegrees(45 * -5).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(-180, Angle.FromDegrees(45 * -4).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(-135, Angle.FromDegrees(45 * -3).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(-90, Angle.FromDegrees(45 * -2).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(-45, Angle.FromDegrees(45 * -1).GetDegreesWithNegative(), TOLERANCE);

		Assert.AreEqual(0, Angle.FromDegrees(45 * 0).GetDegreesWithNegative(), TOLERANCE);

		Assert.AreEqual(45, Angle.FromDegrees(45 * 1).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(90, Angle.FromDegrees(45 * 2).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(135, Angle.FromDegrees(45 * 3).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(-180, Angle.FromDegrees(45 * 4).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(-135, Angle.FromDegrees(45 * 5).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(-90, Angle.FromDegrees(45 * 6).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(-45, Angle.FromDegrees(45 * 7).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(0, Angle.FromDegrees(45 * 8).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(45, Angle.FromDegrees(45 * 9).GetDegreesWithNegative(), TOLERANCE);
		Assert.AreEqual(90, Angle.FromDegrees(45 * 10).GetDegreesWithNegative(), TOLERANCE);
	}

	[Test]
	public void TestGetTotalDegrees()
	{
		Assert.AreEqual(-450, Angle.FromDegrees(45 * -10).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-405, Angle.FromDegrees(45 * -9).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-360, Angle.FromDegrees(45 * -8).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-315, Angle.FromDegrees(45 * -7).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-270, Angle.FromDegrees(45 * -6).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-225, Angle.FromDegrees(45 * -5).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-180, Angle.FromDegrees(45 * -4).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-135, Angle.FromDegrees(45 * -3).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-90, Angle.FromDegrees(45 * -2).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-45, Angle.FromDegrees(45 * -1).GetTotalDegrees(), TOLERANCE);

		Assert.AreEqual(0, Angle.FromDegrees(45 * 0).GetTotalDegrees(), TOLERANCE);

		Assert.AreEqual(45, Angle.FromDegrees(45 * 1).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(90, Angle.FromDegrees(45 * 2).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(135, Angle.FromDegrees(45 * 3).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(180, Angle.FromDegrees(45 * 4).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(225, Angle.FromDegrees(45 * 5).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(270, Angle.FromDegrees(45 * 6).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(315, Angle.FromDegrees(45 * 7).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(360, Angle.FromDegrees(45 * 8).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(405, Angle.FromDegrees(45 * 9).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(450, Angle.FromDegrees(45 * 10).GetTotalDegrees(), TOLERANCE);
	}

	[Test]
	public void TestDifference()
	{
		Assert.AreEqual(0, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(-360)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(90, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(-270)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(179, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(-181)).GetTotalDegrees(), TOLERANCE);
		// Assert.AreEqual(-180, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(-180)).GetTotalDegrees(), TOLERANCE); //Doesn't work right and that makes sense
		Assert.AreEqual(-179, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(-179)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-90, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(-90)).GetTotalDegrees(), TOLERANCE);

		Assert.AreEqual(0, Angle.Difference(Angle.FromDegrees(0), Angle.FromRadians(0)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(90, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(90)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(179, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(179)).GetTotalDegrees(), TOLERANCE);
		// Assert.AreEqual(-180, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(180)).GetTotalDegrees(), TOLERANCE); //Doesn't work right and that makes sense
		Assert.AreEqual(-179, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(181)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-90, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(270)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(0, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(360)).GetTotalDegrees(), TOLERANCE);

		Assert.AreEqual(90, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(450)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(179, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(539)).GetTotalDegrees(), TOLERANCE);
		// Assert.AreEqual(180, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(540)).GetTotalDegrees(), TOLERANCE); //Doesn't work right and that makes sense
		Assert.AreEqual(-179, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(541)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-90, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(630)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(0, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(720)).GetTotalDegrees(), TOLERANCE);

		//Older tests, but still worth keeping, I guess. More tests can never hurt.
		Assert.AreEqual(0, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(360)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(0, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(720)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(0, Angle.Difference(Angle.FromDegrees(-360), Angle.FromDegrees(720)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(179, Angle.Difference(Angle.FromDegrees(-179), Angle.FromDegrees(0)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(-179, Angle.Difference(Angle.FromDegrees(179), Angle.FromDegrees(0)).GetTotalDegrees(), TOLERANCE);
		Assert.AreEqual(45, Angle.Difference(Angle.FromDegrees(0), Angle.FromDegrees(45)).GetTotalDegrees(), TOLERANCE);
	}

	[Test]
	public void TestOperatorAdd()
	{
		void TestSingular(float a, float b, float expected, bool expectToFail = false)
		{
			Angle add = Angle.FromDegrees(a) + Angle.FromDegrees(b);
			if (add == Angle.FromDegrees(expected))
			{
				if (expectToFail)
					Assert.Fail($"{a} + {b} == {add.GetDegrees()} == {expected}, but should have failed.");
			}
			else
			{
				if (!expectToFail)
					Assert.Fail($"{a} + {b} == {add.GetDegrees()} != {expected}");
			}
		}

		TestSingular(0, 0, 0);
		TestSingular(45, 45, 90);
		TestSingular(90, 90, 180);
		TestSingular(180, 180, 360);
		TestSingular(360, 360, 720);
		TestSingular(1, 2, 4, true);
		TestSingular(179, 360, 179);
	}

	[Test]
	public void TestOperatorSub()
	{
		void TestSingular(float a, float b, float expected, bool expectToFail = false)
		{
			Angle sub = Angle.FromDegrees(a) - Angle.FromDegrees(b);
			if (sub == Angle.FromDegrees(expected))
			{
				if(expectToFail)
					Assert.Fail($"{a} - {b} == {sub.GetDegrees()} == {expected}, but should have failed.");
			}
			else
			{
				if(!expectToFail)
					Assert.Fail($"{a} - {b} == {sub.GetDegrees()} != {expected}");
			}
		}

		TestSingular(0, 0, 0);
		TestSingular(45, 45, 0);
		TestSingular(90, 90, 0);
		TestSingular(180, 180, 0);
		TestSingular(360, 360, 0);
		TestSingular(1, 2, -4, true);
		TestSingular(179, 360, 179);
		TestSingular(0, 180, -180);
		TestSingular(0, 360, 0);
		TestSingular(0, 720, 0);
		TestSingular(0, 180, -180);
	}

	[Test]
	public void TestOperatorUnaryNegation()
	{
		Assert.AreEqual(Angle.FromDegrees(0).GetDegrees(), (-Angle.FromDegrees(0)).GetDegrees(), TOLERANCE);
		Assert.AreEqual(Angle.FromDegrees(-180).GetDegrees(), (-Angle.FromDegrees(180)).GetDegrees(), TOLERANCE);
		Assert.AreEqual(Angle.FromDegrees(-45).GetDegrees(), (-Angle.FromDegrees(45)).GetDegrees(), TOLERANCE);
	}

	[Test]
	public void TestOperatorMult()
	{
		void TestSingular(Angle a, float b, Angle expected, bool expectToFail = false)
		{
			Angle mult = a * b;
			if (b * a == a * b && mult == expected)
			{
				if (expectToFail)
					Assert.Fail($"{a} * {b} == {mult.GetDegrees()} == {expected}, but should have failed.");
			}
			else
			{
				if (!expectToFail)
					Assert.Fail($"{a} * {b} == {mult.GetDegrees()} != {expected}");
			}
		}

		TestSingular(Angle.FromDegrees(0),0, Angle.FromDegrees(0));
		TestSingular(Angle.FromDegrees(45), 2, Angle.FromDegrees(90));
		TestSingular(Angle.FromDegrees(90), 2, Angle.FromDegrees(180));
		TestSingular(Angle.FromDegrees(180), 4, Angle.FromDegrees(0));
		TestSingular(Angle.FromDegrees(10), 2, Angle.FromDegrees(-5), true);
	}

	[Test]
	public void TestOperatorDiv()
	{
		void TestSingular(Angle a, float b, Angle expected, bool expectToFail = false)
		{
			Angle div = a / b;
			if (div == expected)
			{
				if (expectToFail)
					Assert.Fail($"{a} / {b} == {div} == {expected}, but should have failed.");
			}
			else
			{
				if (!expectToFail)
					Assert.Fail($"{a} / {b} == {div} != {expected}");
			}
		}

		TestSingular(Angle.FromDegrees(0),0, Angle.FromDegrees(0), true);
		TestSingular(Angle.FromDegrees(45), 2, Angle.FromDegrees(22.5f));
		TestSingular(Angle.FromDegrees(90), 2, Angle.FromDegrees(45));
		TestSingular(Angle.FromDegrees(180), 4, Angle.FromDegrees(45));
		TestSingular(Angle.FromDegrees(10), 2, Angle.FromDegrees(-5), true);
		TestSingular(Angle.FromDegrees(720), 2, Angle.FromDegrees(360));
		TestSingular(Angle.FromDegrees(720), 3, Angle.FromDegrees(240));
	}

	[Test]
	public void TestOperatorNotEquals()
	{
		Assert.IsTrue(Angle.FromDegrees(0) == Angle.ZERO);
		Assert.IsTrue(Angle.FromDegrees(0) != Angle.PI);
		Assert.IsFalse(Angle.FromDegrees(0) != Angle.TWO_PI);
		Assert.IsFalse(Angle.FromDegrees(720) != Angle.TWO_PI);
		Assert.IsFalse(Angle.FromDegrees(-360) != Angle.TWO_PI);
	}

	[Test]
	public void TestOperatorLessThan()
	{
		Assert.IsTrue(Angle.FromDegrees(0) < Angle.PI);
		Assert.IsTrue(Angle.FromDegrees(45) < Angle.FromDegrees(90));
		Assert.IsFalse(Angle.FromDegrees(90) < Angle.FromDegrees(45));
		Assert.IsTrue(Angle.FromDegrees(370) < Angle.FromDegrees(45));
		Assert.IsFalse(Angle.FromDegrees(90) < Angle.HALF_PI);
	}

	[Test]
	public void TestOperatorLessThanOrEqual()
	{
		Assert.IsTrue(Angle.FromDegrees(0) <= Angle.PI);
		Assert.IsTrue(Angle.FromDegrees(45) <= Angle.FromDegrees(90));
		Assert.IsFalse(Angle.FromDegrees(90) <= Angle.FromDegrees(45));
		Assert.IsTrue(Angle.FromDegrees(370) <= Angle.FromDegrees(45));
		Assert.IsTrue(Angle.FromDegrees(90) <= Angle.HALF_PI);
	}

	[Test]
	public void TestOperatorGreaterThan()
	{
		Assert.IsTrue(Angle.PI > Angle.FromDegrees(0));
		Assert.IsTrue(Angle.FromDegrees(90) > Angle.FromDegrees(45));
		Assert.IsFalse(Angle.FromDegrees(45) > Angle.FromDegrees(90));
		Console.WriteLine(Angle.FromDegrees(45) + "," + Angle.FromDegrees(370));
		Assert.IsTrue(Angle.FromDegrees(45) > Angle.FromDegrees(370));
		Assert.IsFalse(Angle.HALF_PI > Angle.FromDegrees(90));
	}

	[Test]
	public void TestOperatorGreaterThanOrEqual()
	{
		Assert.IsTrue(Angle.PI >= Angle.FromDegrees(0));
		Assert.IsTrue(Angle.FromDegrees(90) >= Angle.FromDegrees(45));
		Assert.IsFalse(Angle.FromDegrees(45) >= Angle.FromDegrees(90));
		Assert.IsTrue(Angle.FromDegrees(45) >= Angle.FromDegrees(370));
		Assert.IsTrue(Angle.HALF_PI >= Angle.FromDegrees(90));
	}

	[Test]
	public void TestEquals()
	{
		Assert.IsTrue(Angle.ZERO.Equals(Angle.FromDegrees(0)));
		Assert.IsTrue(Angle.ZERO.Equals(Angle.FromRadians(0)));
		Assert.IsFalse(Angle.PI.Equals(Angle.TWO_PI));
		// ReSharper disable SuspiciousTypeConversion.Global
		Assert.IsFalse(Angle.PI.Equals(new float()));
		// ReSharper restore SuspiciousTypeConversion.Global
	}

	[Test]
	public void TestGetHashCode()
	{
		Assert.AreEqual(Angle.ZERO.GetHashCode(), Angle.FromDegrees(0).GetHashCode());
		Assert.AreEqual(Angle.ZERO.GetHashCode(), Angle.FromRadians(0).GetHashCode());
		Assert.AreEqual(Angle.ZERO.GetHashCode(), Angle.FromRadians(Mathf.TWO_PI).GetHashCode());
		Assert.AreEqual(Angle.ZERO.GetHashCode(), Angle.TWO_PI.GetHashCode());
		Assert.AreEqual(Angle.FromDegrees(90).GetHashCode(), Angle.FromRadians(Mathf.HALF_PI).GetHashCode());
	}
}
