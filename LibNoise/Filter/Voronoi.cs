using System;

namespace LibNoise.Filter
{
    /// <summary>
    ///     Noise module that outputs Voronoi cells.
    ///     In mathematics, a <i>Voronoi cell</i> is a region containing all the
    ///     points that are closer to a specific <i>seed point</i> than to any
    ///     other seed point.  These cells mesh with one another, producing
    ///     polygon-like formations.
    ///     By default, this noise module randomly places a seed point within
    ///     each unit cube.  By modifying the <i>frequency</i> of the seed points,
    ///     an application can change the distance between seed points.  The
    ///     higher the frequency, the closer together this noise module places
    ///     the seed points, which reduces the size of the cells.
    ///     This noise module assigns each Voronoi cell with a random constant
    ///     value from a coherent-noise function.  The <i>displacement value</i>
    ///     controls the range of random values to assign to each cell.  The
    ///     range of random values is +/- the displacement value.
    ///     The frequency determines the size of the Voronoi cells and the
    ///     distance between these cells.
    ///     To modify the random positions of the seed points, call the SetSeed()
    ///     method.
    ///     This noise module can optionally add the distance from the nearest
    ///     seed to the output value.  To enable this feature, call the
    ///     EnableDistance() method.  This causes the points in the Voronoi cells
    ///     to increase in value the further away that point is from the nearest
    ///     seed point.
    ///     Voronoi cells are often used to generate cracked-mud terrain
    ///     formations or crystal-like textures
    /// </summary>
    public class Voronoi : FilterModule, IModule3D
	{
		/// <summary>
	    ///     Default persistence value for the Voronoi noise module.
	    /// </summary>
	    public const float DefaultDisplacement = 1.0f;

		
	    public float GetValue(float x, float y, float z)
		{
			//TODO This method could be more efficient by caching the seed values.
			x *= Frequency;
			y *= Frequency;
			z *= Frequency;

			var xInt = x > 0.0f ? (int) x : (int) x - 1;
			var yInt = y > 0.0f ? (int) y : (int) y - 1;
			var zInt = z > 0.0f ? (int) z : (int) z - 1;

			var minDist = 2147483647.0f;
			var xCandidate = 0.0f;
			var yCandidate = 0.0f;
			var zCandidate = 0.0f;

			// Inside each unit cube, there is a seed point at a random position.  Go
			// through each of the nearby cubes until we find a cube with a seed point
			// that is closest to the specified position.
			for (var zCur = zInt - 2; zCur <= zInt + 2; zCur++)
			for (var yCur = yInt - 2; yCur <= yInt + 2; yCur++)
			for (var xCur = xInt - 2; xCur <= xInt + 2; xCur++)
			{
				// Calculate the position and distance to the seed point inside of
				// this unit cube.
				var xPos = xCur + Primitive3D.GetValue(xCur, yCur, zCur);
				var yPos = yCur + Primitive3D.GetValue(xCur, yCur, zCur);
				var zPos = zCur + Primitive3D.GetValue(xCur, yCur, zCur);

				var xDist = xPos - x;
				var yDist = yPos - y;
				var zDist = zPos - z;
				var dist = xDist * xDist + yDist * yDist + zDist * zDist;

				if (dist < minDist)
				{
					// This seed point is closer to any others found so far, so record
					// this seed point.
					minDist = dist;
					xCandidate = xPos;
					yCandidate = yPos;
					zCandidate = zPos;
				}
			}

			float value;

			if (Distance)
			{
				// Determine the distance to the nearest seed point.
				var xDist = xCandidate - x;
				var yDist = yCandidate - y;
				var zDist = zCandidate - z;
				value = (float) Math.Sqrt(xDist * xDist + yDist * yDist + zDist * zDist) * Libnoise.Sqrt3 - 1.0f;
			}
			else
			{
				value = 0.0f;
			}

			// Return the calculated distance with the displacement value applied.
			return value + Displacement * Primitive3D.GetValue(
				       (int) Math.Floor(xCandidate),
				       (int) Math.Floor(yCandidate),
				       (int) Math.Floor(zCandidate));
		}

		/// <summary>
	    ///     This noise module assigns each Voronoi cell with a random constant
	    ///     value from a coherent-noise function.  The
	    ///     <i>
	    ///         displacement
	    ///         value
	    ///     </i>
	    ///     controls the range of random values to assign to each
	    ///     cell.  The range of random values is +/- the displacement value.
	    /// </summary>
	    public float Displacement { get; set; } = DefaultDisplacement;

	    /// <summary>
	    ///     Applying the distance from the nearest seed point to the output
	    ///     value causes the points in the Voronoi cells to increase in value
	    ///     the further away that point is from the nearest seed point.
	    /// </summary>
	    public bool Distance { get; set; }
	}
}