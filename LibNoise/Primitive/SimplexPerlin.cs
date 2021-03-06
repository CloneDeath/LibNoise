﻿using System;

namespace LibNoise.Primitive
{
    /// <summary>
    ///     Noise module that outputs 3-dimensional Simplex Perlin noise.
    ///     This algorithm has a computational cost of O(n+1) where n is the dimension.
    ///     It's a lost faster than ImprovedPerlin (O(2^n))
    ///     Quality is not used here.
    ///     This noise module outputs values that usually range from
    ///     -1.0 to +1.0, but there are no guarantees that all output values will
    ///     exist within that range.
    /// </summary>
    public class SimplexPerlin : ImprovedPerlin, IModule4D, IModule3D, IModule2D
	{
		/// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public new float GetValue(float x, float y)
		{
			// Noise contributions from the three corners
			float n0 = 0, n1 = 0, n2 = 0;

			// Skew the input space to determine which simplex cell we're in
			var s = (x + y) * F2; // Hairy factor for 2D

			var i = (int)Math.Floor(x + s);
			var j = (int)Math.Floor(y + s);

			var t = (i + j) * G2;

			// The x,y distances from the cell origin
			var x0 = x - (i - t);
			var y0 = y - (j - t);

			// For the 2D case, the simplex shape is an equilateral triangle.
			// Determine which simplex we are in.

			// Offsets for second (middle) corner of simplex in (i,j)
			int i1, j1;

			if (x0 > y0)
			{
// lower triangle, XY order: (0,0)->(1,0)->(1,1)
				i1 = 1;
				j1 = 0;
			}
			else
			{
// upper triangle, YX order: (0,0)->(0,1)->(1,1)
				i1 = 0;
				j1 = 1;
			}

			// A step of (1,0) in (i,j) means a step of (1-c,-c) in (x,y), and
			// a step of (0,1) in (i,j) means a step of (-c,1-c) in (x,y), where
			// c = (3-sqrt(3))/6
			var x1 = x0 - i1 + G2; // Offsets for middle corner in (x,y) unskewed
			var y1 = y0 - j1 + G2;
			var x2 = x0 + G22; // Offsets for last corner in (x,y) unskewed
			var y2 = y0 + G22;

			// Work out the hashed gradient indices of the three simplex corners
			var ii = i & 0xff;
			var jj = j & 0xff;

			// Calculate the contribution from the three corners
			var t0 = 0.5f - x0 * x0 - y0 * y0;

			if (t0 > 0)
			{
				t0 *= t0;
				var gi0 = Random[ii + Random[jj]] % 12;
				n0 = t0 * t0 * Dot(Grad3[gi0], x0, y0); // (x,y) of grad3 used for
				// 2D gradient
			}

			var t1 = 0.5f - x1 * x1 - y1 * y1;

			if (t1 > 0)
			{
				t1 *= t1;
				var gi1 = Random[ii + i1 + Random[jj + j1]] % 12;
				n1 = t1 * t1 * Dot(Grad3[gi1], x1, y1);
			}

			var t2 = 0.5f - x2 * x2 - y2 * y2;
			if (t2 > 0)
			{
				t2 *= t2;
				var gi2 = Random[ii + 1 + Random[jj + 1]] % 12;
				n2 = t2 * t2 * Dot(Grad3[gi2], x2, y2);
			}

			// Add contributions from each corner to get the final noise value.
			// The result is scaled to return values in the interval [-1,1].
			return 70.0f * (n0 + n1 + n2);
		}

		
	    public new float GetValue(float x, float y, float z)
		{
			float n0 = 0, n1 = 0, n2 = 0, n3 = 0;

			// Noise contributions from the four corners
			// Skew the input space to determine which simplex cell we're in
			var s = (x + y + z) * F3;

			// for 3D
			var i = (int)Math.Floor(x + s);
			var j = (int)Math.Floor(y + s);
			var k = (int)Math.Floor(z + s);

			var t = (i + j + k) * G3;

			// The x,y,z distances from the cell origin
			var x0 = x - (i - t);
			var y0 = y - (j - t);
			var z0 = z - (k - t);

			// For the 3D case, the simplex shape is a slightly irregular tetrahedron.
			// Determine which simplex we are in.
			// Offsets for second corner of simplex in (i,j,k)
			int i1, j1, k1;

			// coords
			int i2, j2, k2; // Offsets for third corner of simplex in (i,j,k) coords

			if (x0 >= y0)
			{
				if (y0 >= z0)
				{
// X Y Z order
					i1 = 1;
					j1 = 0;
					k1 = 0;
					i2 = 1;
					j2 = 1;
					k2 = 0;
				}
				else if (x0 >= z0)
				{
// X Z Y order
					i1 = 1;
					j1 = 0;
					k1 = 0;
					i2 = 1;
					j2 = 0;
					k2 = 1;
				}
				else
				{
// Z X Y order
					i1 = 0;
					j1 = 0;
					k1 = 1;
					i2 = 1;
					j2 = 0;
					k2 = 1;
				}
			}
			else
			{
				// x0 < y0
				if (y0 < z0)
				{
// Z Y X order
					i1 = 0;
					j1 = 0;
					k1 = 1;
					i2 = 0;
					j2 = 1;
					k2 = 1;
				}
				else if (x0 < z0)
				{
// Y Z X order
					i1 = 0;
					j1 = 1;
					k1 = 0;
					i2 = 0;
					j2 = 1;
					k2 = 1;
				}
				else
				{
// Y X Z order
					i1 = 0;
					j1 = 1;
					k1 = 0;
					i2 = 1;
					j2 = 1;
					k2 = 0;
				}
			}

			// A step of (1,0,0) in (i,j,k) means a step of (1-c,-c,-c) in (x,y,z),
			// a step of (0,1,0) in (i,j,k) means a step of (-c,1-c,-c) in (x,y,z),
			// and
			// a step of (0,0,1) in (i,j,k) means a step of (-c,-c,1-c) in (x,y,z),
			// where c = 1/6.

			// Offsets for second corner in (x,y,z) coords
			var x1 = x0 - i1 + G3;
			var y1 = y0 - j1 + G3;
			var z1 = z0 - k1 + G3;

			// Offsets for third corner in (x,y,z)
			var x2 = x0 - i2 + F3;
			var y2 = y0 - j2 + F3;
			var z2 = z0 - k2 + F3;

			// Offsets for last corner in (x,y,z)
			var x3 = x0 - 0.5f;
			var y3 = y0 - 0.5f;
			var z3 = z0 - 0.5f;

			// Work out the hashed gradient indices of the four simplex corners
			var ii = i & 0xff;
			var jj = j & 0xff;
			var kk = k & 0xff;

			// Calculate the contribution from the four corners
			var t0 = 0.6f - x0 * x0 - y0 * y0 - z0 * z0;
			if (t0 > 0)
			{
				t0 *= t0;
				var gi0 = Random[ii + Random[jj + Random[kk]]] % 12;
				n0 = t0 * t0 * Dot(Grad3[gi0], x0, y0, z0);
			}

			var t1 = 0.6f - x1 * x1 - y1 * y1 - z1 * z1;
			if (t1 > 0)
			{
				t1 *= t1;
				var gi1 = Random[ii + i1 + Random[jj + j1 + Random[kk + k1]]] % 12;
				n1 = t1 * t1 * Dot(Grad3[gi1], x1, y1, z1);
			}

			var t2 = 0.6f - x2 * x2 - y2 * y2 - z2 * z2;
			if (t2 > 0)
			{
				t2 *= t2;
				var gi2 = Random[ii + i2 + Random[jj + j2 + Random[kk + k2]]] % 12;
				n2 = t2 * t2 * Dot(Grad3[gi2], x2, y2, z2);
			}

			var t3 = 0.6f - x3 * x3 - y3 * y3 - z3 * z3;
			if (t3 > 0)
			{
				t3 *= t3;
				var gi3 = Random[ii + 1 + Random[jj + 1 + Random[kk + 1]]] % 12;
				n3 = t3 * t3 * Dot(Grad3[gi3], x3, y3, z3);
			}

			// Add contributions from each corner to get the final noise value.
			// The result is scaled to stay just inside [-1,1]
			return 32.0f * (n0 + n1 + n2 + n3);
		}

		/// <summary>
	    ///     Generates an output value given the coordinates of the specified input value.
	    /// </summary>
	    /// <param name="x">The input coordinate on the x-axis.</param>
	    /// <param name="y">The input coordinate on the y-axis.</param>
	    /// <param name="z">The input coordinate on the z-axis.</param>
	    /// <param name="w">The input coordinate on the w-axis.</param>
	    /// <returns>The resulting output value.</returns>
	    public float GetValue(float x, float y, float z, float w)
		{
			// The skewing and unskewing factors are hairy again for the 4D case
			// Noise contributions
			float n0 = 0, n1 = 0, n2 = 0, n3 = 0, n4 = 0;

			// from the five corners
			// Skew the (x,y,z,w) space to determine which cell of 24 simplices
			var s = (x + y + z + w) * F4; // Factor for 4D skewing

			var i = (int)Math.Floor(x + s);
			var j = (int)Math.Floor(y + s);
			var k = (int)Math.Floor(z + s);
			var l = (int)Math.Floor(w + s);

			var t = (i + j + k + l) * G4; // Factor for 4D unskewing

			// The x,y,z,w distances from the cell origin
			var x0 = x - (i - t);
			var y0 = y - (j - t);
			var z0 = z - (k - t);
			var w0 = w - (l - t);

			// For the 4D case, the simplex is a 4D shape I won't even try to
			// describe.
			// To find out which of the 24 possible simplices we're in, we need to
			// determine the magnitude ordering of x0, y0, z0 and w0.
			// The method below is a good way of finding the ordering of x,y,z,w and
			// then find the correct traversal order for the simplex were in.
			// First, six pair-wise comparisons are performed between each possible
			// pair of the four coordinates, and the results are used to add up
			// binary bits for an integer index.
			var c = 0;

			if (x0 > y0)
				c = 0x20;

			if (x0 > z0)
				c |= 0x10;

			if (y0 > z0)
				c |= 0x08;

			if (x0 > w0)
				c |= 0x04;

			if (y0 > w0)
				c |= 0x02;

			if (z0 > w0)
				c |= 0x01;

			int i1, j1, k1, l1; // The integer offsets for the second simplex corner
			int i2, j2, k2, l2; // The integer offsets for the third simplex corner
			int i3, j3, k3, l3; // The integer offsets for the fourth simplex corner

			// simplex[c] is a 4-vector with the numbers 0, 1, 2 and 3 in some
			// order. Many values of c will never occur, since e.g. x>y>z>w makes
			// x<z, y<w and x<w impossible. Only the 24 indices which have non-zero
			// entries make any sense. We use a thresholding to set the coordinates
			// in turn from the largest magnitude. The number 3 in the "simplex"
			// array is at the position of the largest coordinate.
			var sc = Simplex[c];

			i1 = sc[0] >= 3 ? 1 : 0;
			j1 = sc[1] >= 3 ? 1 : 0;
			k1 = sc[2] >= 3 ? 1 : 0;
			l1 = sc[3] >= 3 ? 1 : 0;

			// The number 2 in the "simplex" array is at the second largest coordinate.
			i2 = sc[0] >= 2 ? 1 : 0;
			j2 = sc[1] >= 2 ? 1 : 0;
			k2 = sc[2] >= 2 ? 1 : 0;
			l2 = sc[3] >= 2 ? 1 : 0;

			// The number 1 in the "simplex" array is at the second smallest coordinate.
			i3 = sc[0] >= 1 ? 1 : 0;
			j3 = sc[1] >= 1 ? 1 : 0;
			k3 = sc[2] >= 1 ? 1 : 0;
			l3 = sc[3] >= 1 ? 1 : 0;

			// The fifth corner has all coordinate offsets = 1, so no need to look that up.
			var x1 = x0 - i1 + G4; // Offsets for second corner in (x,y,z,w)
			var y1 = y0 - j1 + G4;
			var z1 = z0 - k1 + G4;
			var w1 = w0 - l1 + G4;

			var x2 = x0 - i2 + G42; // Offsets for third corner in (x,y,z,w)
			var y2 = y0 - j2 + G42;
			var z2 = z0 - k2 + G42;
			var w2 = w0 - l2 + G42;

			var x3 = x0 - i3 + G43; // Offsets for fourth corner in (x,y,z,w)
			var y3 = y0 - j3 + G43;
			var z3 = z0 - k3 + G43;
			var w3 = w0 - l3 + G43;

			var x4 = x0 + G44; // Offsets for last corner in (x,y,z,w)
			var y4 = y0 + G44;
			var z4 = z0 + G44;
			var w4 = w0 + G44;

			// Work out the hashed gradient indices of the five simplex corners
			var ii = i & 0xff;
			var jj = j & 0xff;
			var kk = k & 0xff;
			var ll = l & 0xff;

			// Calculate the contribution from the five corners
			var t0 = 0.6f - x0 * x0 - y0 * y0 - z0 * z0 - w0 * w0;

			if (t0 > 0)
			{
				t0 *= t0;
				var gi0 = Random[ii + Random[jj + Random[kk + Random[ll]]]] % 32;
				n0 = t0 * t0 * Dot(Grad4[gi0], x0, y0, z0, w0);
			}

			var t1 = 0.6f - x1 * x1 - y1 * y1 - z1 * z1 - w1 * w1;
			if (t1 > 0)
			{
				t1 *= t1;
				var gi1 =
					Random[
						ii + i1 + Random[jj + j1 + Random[kk + k1 + Random[ll + l1]]]] % 32;
				n1 = t1 * t1 * Dot(Grad4[gi1], x1, y1, z1, w1);
			}

			var t2 = 0.6f - x2 * x2 - y2 * y2 - z2 * z2 - w2 * w2;
			if (t2 > 0)
			{
				t2 *= t2;
				var gi2 =
					Random[
						ii + i2 + Random[jj + j2 + Random[kk + k2 + Random[ll + l2]]]] % 32;
				n2 = t2 * t2 * Dot(Grad4[gi2], x2, y2, z2, w2);
			}

			var t3 = 0.6f - x3 * x3 - y3 * y3 - z3 * z3 - w3 * w3;
			if (t3 > 0)
			{
				t3 *= t3;
				var gi3 =
					Random[
						ii + i3 + Random[jj + j3 + Random[kk + k3 + Random[ll + l3]]]] % 32;
				n3 = t3 * t3 * Dot(Grad4[gi3], x3, y3, z3, w3);
			}

			var t4 = 0.6f - x4 * x4 - y4 * y4 - z4 * z4 - w4 * w4;
			if (t4 > 0)
			{
				t4 *= t4;
				var gi4 =
					Random[ii + 1 + Random[jj + 1 + Random[kk + 1 + Random[ll + 1]]]
					] % 32;
				n4 = t4 * t4 * Dot(Grad4[gi4], x4, y4, z4, w4);
			}

			// Sum up and scale the result to cover the range [-1,1]
			return 27.0f * (n0 + n1 + n2 + n3 + n4);
		}

		/// <summary>
	    ///     Computes dot product in 4D.
	    /// </summary>
	    /// <param name="g">4-vector (grid offset).</param>
	    /// <param name="x">X coordinates.</param>
	    /// <param name="y">Y coordinates.</param>
	    /// <param name="z">Z coordinates.</param>
	    /// <param name="t">T coordinates.</param>
	    /// <returns>Dot product.</returns>
	    protected static float Dot(int[] g, float x, float y, float z, float t)
		{
			return g[0] * x + g[1] * y + g[2] * z + g[3] * t;
		}

	    /// <summary>
	    ///     Computes dot product in 3D.
	    /// </summary>
	    /// <param name="g">3-vector (grid offset).</param>
	    /// <param name="x">X coordinates.</param>
	    /// <param name="y">Y coordinates.</param>
	    /// <param name="z">Z coordinates.</param>
	    /// <returns>Dot product.</returns>
	    protected static float Dot(int[] g, float x, float y, float z)
		{
			return g[0] * x + g[1] * y + g[2] * z;
		}

	    /// <summary>
	    ///     Computes dot product in 2D.
	    /// </summary>
	    /// <param name="g">2-vector (grid offset).</param>
	    /// <param name="x">X coordinates.</param>
	    /// <param name="y">Y coordinates.</param>
	    /// <returns>Dot product.</returns>
	    protected static float Dot(int[] g, float x, float y)
		{
			return g[0] * x + g[1] * y;
		}

		/// Skewing and unskewing factors for 2D, 3D and 4D, 
		/// some of them pre-multiplied.
		private const float F2 = 0.5f * (Libnoise.Sqrt3 - 1.0f);

		private const float G2 = (3.0f - Libnoise.Sqrt3) / 6.0f;
		private const float G22 = G2 * 2.0f - 1f;

		private const float F3 = 1.0f / 3.0f;
		private const float G3 = 1.0f / 6.0f;

		private const float F4 = (Libnoise.Sqrt5 - 1.0f) / 4.0f;
		private const float G4 = (5.0f - Libnoise.Sqrt5) / 20.0f;
		private const float G42 = G4 * 2.0f;
		private const float G43 = G4 * 3.0f;
		private const float G44 = G4 * 4.0f - 1.0f;

	    /// <summary>
	    ///     Gradient vectors for 3D (pointing to mid points of all edges of a unit
	    ///     cube)
	    /// </summary>
	    private static readonly int[][] Grad3 =
		{
			new[] {1, 1, 0}, new[] {-1, 1, 0}, new[] {1, -1, 0},
			new[] {-1, -1, 0}, new[] {1, 0, 1}, new[] {-1, 0, 1},
			new[] {1, 0, -1}, new[] {-1, 0, -1}, new[] {0, 1, 1},
			new[] {0, -1, 1}, new[] {0, 1, -1}, new[] {0, -1, -1}
		};

	    /// <summary>
	    ///     Gradient vectors for 4D (pointing to mid points of all edges of a unit 4D
	    ///     hypercube)
	    /// </summary>
	    private static readonly int[][] Grad4 =
		{
			new[] {0, 1, 1, 1}, new[] {0, 1, 1, -1}, new[] {0, 1, -1, 1}, new[] {0, 1, -1, -1},
			new[] {0, -1, 1, 1}, new[] {0, -1, 1, -1}, new[] {0, -1, -1, 1}, new[] {0, -1, -1, -1},
			new[] {1, 0, 1, 1}, new[] {1, 0, 1, -1}, new[] {1, 0, -1, 1}, new[] {1, 0, -1, -1},
			new[] {-1, 0, 1, 1}, new[] {-1, 0, 1, -1}, new[] {-1, 0, -1, 1}, new[] {-1, 0, -1, -1},
			new[] {1, 1, 0, 1}, new[] {1, 1, 0, -1}, new[] {1, -1, 0, 1}, new[] {1, -1, 0, -1},
			new[] {-1, 1, 0, 1}, new[] {-1, 1, 0, -1}, new[] {-1, -1, 0, 1}, new[] {-1, -1, 0, -1},
			new[] {1, 1, 1, 0}, new[] {1, 1, -1, 0}, new[] {1, -1, 1, 0}, new[] {1, -1, -1, 0},
			new[] {-1, 1, 1, 0}, new[] {-1, 1, -1, 0}, new[] {-1, -1, 1, 0}, new[] {-1, -1, -1, 0}
		};

	    /// <summary>
	    ///     A lookup table to traverse the simplex around a given point in 4D.
	    ///     Details can be found where this table is used, in the 4D noise method.
	    /// </summary>
	    private static readonly int[][] Simplex =
		{
			new[] {0, 1, 2, 3}, new[] {0, 1, 3, 2}, new[] {0, 0, 0, 0}, new[] {0, 2, 3, 1},
			new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {1, 2, 3, 0},
			new[] {0, 2, 1, 3}, new[] {0, 0, 0, 0}, new[] {0, 3, 1, 2}, new[] {0, 3, 2, 1},
			new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {1, 3, 2, 0},
			new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0},
			new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0},
			new[] {1, 2, 0, 3}, new[] {0, 0, 0, 0}, new[] {1, 3, 0, 2}, new[] {0, 0, 0, 0},
			new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {2, 3, 0, 1}, new[] {2, 3, 1, 0},
			new[] {1, 0, 2, 3}, new[] {1, 0, 3, 2}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0},
			new[] {0, 0, 0, 0}, new[] {2, 0, 3, 1}, new[] {0, 0, 0, 0}, new[] {2, 1, 3, 0},
			new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0},
			new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0},
			new[] {2, 0, 1, 3}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0},
			new[] {3, 0, 1, 2}, new[] {3, 0, 2, 1}, new[] {0, 0, 0, 0}, new[] {3, 1, 2, 0},
			new[] {2, 1, 0, 3}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0}, new[] {0, 0, 0, 0},
			new[] {3, 1, 0, 2}, new[] {0, 0, 0, 0}, new[] {3, 2, 0, 1}, new[] {3, 2, 1, 0}
		};

		/// <summary>
	    ///     0-args constructor
	    /// </summary>
	    public SimplexPerlin()
			: base(DefaultSeed, DefaultQuality)
		{
		}

	    /// <summary>
	    ///     Create a new ImprovedPerlin with given values
	    /// </summary>
	    /// <param name="seed"></param>
	    /// <param name="quality"></param>
	    public SimplexPerlin(int seed, NoiseQuality quality)
			: base(seed, quality)
		{
		}
	}
}