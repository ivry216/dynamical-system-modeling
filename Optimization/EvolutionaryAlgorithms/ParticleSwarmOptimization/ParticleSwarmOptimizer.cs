using MathCore.Extensions.Arrays;
using Randomizer.Randomizing;

namespace Optimization.EvolutionaryAlgorithms.ParticleSwarmOptimization
{
    public class ParticleSwarmOptimizer : EvolutionaryAlgorithm<ParticleSwarmOptimizerParameters>
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

        protected override void GenerateInitial()
        {
            // Generate the new population
            // With normal distribution
            if (Parameters.GenerationParameters.GenerationType == PopulationGenerationType.Normal)
            {
                for (int i = 0; i < Parameters.Size; i++)
                {
                    Population[i] = RandomEngine.Instance.GenerateNormallyDistributedVector(
                    dimension: Problem.Dimension,
                    mean: Parameters.GenerationParameters.GenerationMean,
                    sd: Parameters.GenerationParameters.GenerationSd);
                }
            }

            // With uniform distribution
            if (Parameters.GenerationParameters.GenerationType == PopulationGenerationType.Uniform)
            {
                for (int i = 0; i < Parameters.Size; i++)
                {
                    Population[i] = RandomEngine.Instance.GenerateUniformlyDistributedVector(
                    dimension: Problem.Dimension,
                    from: Parameters.GenerationParameters.GenerationFrom,
                    to: Parameters.GenerationParameters.GenerationTo);
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
                TryUpdateSolution(Fitness[i], Population[i]);
            }

            //
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
            }

            // Calculate fitness
            CalculateFitness();

            // Initialize history with data
            for (int k = 0; k < Parameters.Size; k++)
            {
                TryUpdateSwarmHistory(Population[k], Fitness[k], k);
                TryUpdateSolution(Fitness[k], Population[k]);
            }
        }

        private void TryUpdateSwarmHistory(double[] alternative, double value, int currentParticleIndex)
        {
            if (value > _bestFoundValue[currentParticleIndex])
            {
                _bestFoundPosition[currentParticleIndex].FillWithVector(alternative);
                _bestFoundValue[currentParticleIndex] = value;

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
