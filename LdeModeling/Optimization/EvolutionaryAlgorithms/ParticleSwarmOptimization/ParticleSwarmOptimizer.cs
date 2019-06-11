using Randomizer.Randomizing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.EvolutionaryAlgorithms.ParticleSwarmOptimization
{
    class ParticleSwarmOptimizer : EvolutionaryAlgorithm<ParticleSwarmOptimizerParameters>
    {
        private double[][] _velocities;
        private double[][] _bestFoundPosition;
        private double[] _bestFoundValue;
        private double[] _bestEverFoundPosition;
        private double _bestEverFoundValue;

        protected override void Initialize()
        {
            // Call a base initialization method
            base.Initialize();

            // Initialize velocities and memories
            _velocities = new double[Parameters.Size][];
            _bestFoundPosition = new double[Parameters.Size][];
            _bestFoundValue = new double[Parameters.Size];

            for (int i = 0; i < Parameters.Size; i++)
            {
                _velocities[i] = new double[Problem.Dimension];
                _bestFoundPosition[i] = new double[Problem.Dimension];
            }

            // Initialize the best group found solution
            _bestEverFoundPosition = new double[Problem.Dimension];
        }

        protected override void Generate()
        {
            // Unpdate iteration
            Iteration = 0;

            // Generate the new population
            // With normal distribution
            if (Parameters.GenerationType == PopulationGenerationType.Normal)
            {
                for (int i = 0; i < Parameters.Size; i++)
                {
                    Population[i] = RandomEngine.Instance.GenerateNormallyDistributedVector(
                    dimension: Problem.Dimension,
                    mean: Parameters.GenerationMean,
                    sd: Parameters.GenerationSd);
                }
            }

            // With uniform distribution
            if (Parameters.GenerationType == PopulationGenerationType.Uniform)
            {
                for (int i = 0; i < Parameters.Size; i++)
                {
                    Population[i] = RandomEngine.Instance.GenerateUniformlyDistributedVector(
                    dimension: Problem.Dimension,
                    from: Parameters.GenerationFrom,
                    to: Parameters.GenerationTo);
                }
            }

            // Generate velocities
            for (int i = 0; i < Parameters.Size; i++)
            {
                _velocities[i] = RandomEngine.Instance.GenerateUniformlyDistributedVector(
                dimension: Problem.Dimension, 
                from: Parameters.GenerationVelocitiesFrom,
                to: Parameters.GenerationVelocitiesTo);
            }

            // Now calculate the fitness
            CalculateFitness();

            // Initialize history with data
            for (int i = 0; i < Parameters.Size; i++)
            {
                TryUpdateSwarmHistory(Population[i], Fitness[i], i);
            }
        }

        protected override void NextIteration()
        {
            // Update iteration
            Iteration++;

            for (int i = 0; i < Parameters.Size; i++)
            {
                var currentVelocity = _velocities[i];
                var currentParticle = Population[i];
                var bestLocallyFound = _bestFoundPosition[i];

                for (int j = 0; j < Problem.Dimension; j++)
                {
                    // Generate random values
                    double randValueLoc = RandomEngine.Instance.GenerateUniformlyDistributed();
                    double randValueGlob = RandomEngine.Instance.GenerateUniformlyDistributed();

                    // Update velocity
                    currentVelocity[j] = Parameters.W * currentVelocity[j]
                        + randValueLoc * Parameters.Phi1 * (bestLocallyFound[j] - currentParticle[j])
                        + randValueGlob * Parameters.Phi2 * (_bestEverFoundPosition[j] - currentParticle[j]);

                    // Update particle position
                    currentParticle[j] += currentVelocity[j];
                }

                // Calculate fitness
                CalculateFitness();

                // Initialize history with data
                for (int k = 0; k < Parameters.Size; k++)
                {
                    TryUpdateSwarmHistory(Population[k], Fitness[k], k);
                }
            }
        }

        private void TryUpdateSwarmHistory(double[] alternative, double value, int currentParticleIndex)
        {
            if (value > Fitness[currentParticleIndex])
            {
                Population[currentParticleIndex] = alternative;
                Fitness[currentParticleIndex] = value;

                if (value > _bestEverFoundValue)
                {
                    BestValue = value;
                    BestSolution = alternative;
                    _bestEverFoundValue = value;
                    _bestEverFoundPosition = alternative;
                }
            }
        }
    }
}
