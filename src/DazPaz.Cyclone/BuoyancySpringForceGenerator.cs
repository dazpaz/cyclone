﻿namespace DazPaz.Cyclone
{
	public class BuoyancySpringForceGenerator : IParticleForceGenerator
	{
		#region Properties

		private const double DefaultLiquidDensity = 1000.0f;

		private double MaximumDepth { get; set; }
		private double Volume { get; set; }
		private double LiquidHeight { get; set; }
		private double LiquidDensity { get; set; }

		#endregion

		#region Constructors

		public BuoyancySpringForceGenerator(double maximumDepth, double volume, double liquidHeight,
			double liquidDensity = DefaultLiquidDensity)
		{
			MaximumDepth = maximumDepth;
			Volume = volume;
			LiquidHeight = liquidHeight;
			LiquidDensity = liquidDensity;
		}

		#endregion

		#region IParticleForceGenerator Members

		public void UpdateForce(IParticle particle, double duration)
		{
			 var particleHeight = particle.Position.Y;

			// Check if we are out of the water - if so, return as there is no buoyancy force to add
			if (particleHeight >= LiquidHeight + MaximumDepth) return;

			// Fully submerged buoyancy force
			var force = new Vector3 {Y = LiquidDensity * Volume};

			// If partially submerged, adjust the buoyancy force based on how deep the object is
			if (particleHeight > LiquidHeight - MaximumDepth)
			{
				var amountSubmerged = (LiquidHeight - particleHeight) / (MaximumDepth);
				force.Y *= amountSubmerged;
			}
		
			particle.AddForce(force);
		}

		#endregion
	}
}
