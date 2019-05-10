using System;
using System.Collections.Generic;
using TestApp.MathematicalCore.ArrayExtensions;
using TestApp.MathematicalCore.LinearOperator;
using TestApp.MathematicalCore.Normalization;
using TestApp.MathematicalCore.Randomizing;
using TestApp.Optimization.EvolutionaryAlgorithms.RealValueGeneticAlgorithm.ParameterTypes;

namespace TestApp.Optimization.EvolutionaryAlgorithms.RealValueGeneticAlgorithm
{
    class RealGeneticAlgorithm : EvolutionaryAlgorithm<RealGeneticAlgorithmParameters>
    {
        private class ReverseComparer : IComparer<double>
        {
            public int Compare(double x, double y)
            {
                return y.CompareTo(x);
            }
        }

        private ReverseComparer reverseComparer = new ReverseComparer();

        protected override void Initialize()
        {
            // Call a base initialization method
            base.Initialize();

            // Initialize the trial population
            MergedPopulation = new double[Parameters.Size + Parameters.SizeOfTrialPopulation][];
            for (int i = 0; i < Parameters.Size + Parameters.SizeOfTrialPopulation; i++)
            {
                MergedPopulation[i] = new double[Problem.Dimension];
            }
            // Initialize the next fitness
            MergedFitness = new double[Parameters.Size + Parameters.SizeOfTrialPopulation];

            // Initialize the merged population
            TrialPopulation = new double[Parameters.SizeOfTrialPopulation][];
            for (int i = 0; i < Problem.Dimension; i++)
            {
                TrialPopulation[i] = new double[Problem.Dimension];
            }
            // Initialize the merged fitness
            TrialFitness = new double[Parameters.SizeOfTrialPopulation];
        }

        protected override void Generate()
        {
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

            // Now calculate the fitness
            CalculateFitness();
        }

        protected override void NextIteration()
        {
            // For all individs
            for (int i = 0; i < Parameters.SizeOfTrialPopulation; i++)
            {
                // Make a selection
                int[] parentsIndices = PerformSelection();
                // Perform a crossover
                double[] offspring = PerformCrossover(parentsIndices);
                // Make a mutant
                double[] mutant = PerformMutation(offspring);

                // Add it to the trial population
                TrialPopulation[i] = mutant;
                // Calculate its fitness
                TrialFitness[i] = Problem.CalcualteCriterion(mutant);
            }

            // Perform the next population based on the current and previous
            switch(Parameters.NextPopulationType)
            {
                case RvgaNextPopulationType.Offsprings:
                    {
                        // Sort the trial population
                        Array.Sort(TrialFitness, TrialPopulation, reverseComparer);
                        // Make the new population
                        for (int i = 0; i < Parameters.Size; i++)
                        {
                            for (int j = 0; j < Problem.Dimension; j++)
                            {
                                Population[i][j] = TrialPopulation[i][j];
                            }
                            Fitness[i] = TrialFitness[i];
                        }
                        break;
                    }
                case RvgaNextPopulationType.ParentsAndOffsprings:
                    {
                        // Fill in the merged populations
                        // Parents
                        for (int i = 0; i < Parameters.Size; i++)
                        {
                            for (int j = 0; j < Problem.Dimension; j++)
                            {
                                MergedPopulation[i][j] = Population[i][j];
                            }
                            MergedFitness[i] = Fitness[i];
                        }
                        // Offsprings
                        for (int i = 0, j = Parameters.Size; i < Parameters.SizeOfTrialPopulation; i++, j++)
                        {
                            for (int k = 0; k < Problem.Dimension; k++)
                            {
                                MergedPopulation[j][k] = TrialPopulation[i][k];
                            }
                            MergedFitness[j] = TrialFitness[i];
                        }

                        // Sort the trial population
                        Array.Sort(MergedFitness, MergedPopulation, reverseComparer);

                        // Make the new population truncated
                        for (int i = 0; i < Parameters.Size; i++)
                        {
                            for (int j = 0; j < Problem.Dimension; j++)
                            {
                                Population[i][j] = MergedPopulation[i][j];
                            }
                            Fitness[i] = MergedFitness[i];
                        }
                        break;
                    }
            }

            // Update the solution
            TryUpdateSolution(Fitness[0], Population[0]);
        }

        #region Selection

        private int[] PerformSelection()
        {
            switch (Parameters.SelectionType)
            {
                case RvgaSelectionType.Proportional:
                    return PerfrormProportionalSelection();
                case RvgaSelectionType.Rank:
                    return PerformRankSelection();
                case RvgaSelectionType.Tournament:
                    return PerformTournamentSelection();
            }

            return null;
        }

        private int[] PerfrormProportionalSelection()
        {
            // Convert the fitness to cumulative probabilities
            double[] probabilities = Normalizer.Instance.ConvertToCumulative(Fitness);

            // Generate the indexes of parents
            int[] parentsIndices = RandomEngine.Instance.GenerateIndexByProbabilitiesVector(
                dimension: Parameters.NumberOfParents,
                indexesNumber: Parameters.Size,
                probabilities: probabilities);

            return parentsIndices;
        }

        private int[] PerformRankSelection()
        {
            // Get sorted indices and values
            (int[] Indices, double[] SortedFitness) sorted = Fitness.GetSortedIndicesAndValues();

            // Get the array of ranks
            int[] ranks = new int[Parameters.Size];
            ranks.AssignWIntegerSequence(start: Parameters.Size, step: -1);
            // Normalize ranks
            double[] probabilities = Normalizer.Instance.ConvertToCumulative(ranks);

            // Generate the numbers by probabilities
            int[] generatedNumbers = RandomEngine.Instance.GenerateIndexByProbabilitiesVector(
                dimension: Parameters.NumberOfParents,
                indexesNumber: Parameters.Size,
                probabilities: probabilities);

            // Generated numbers to indices
            int[] indices = new int[Parameters.NumberOfParents];
            for (int i = 0; i < Parameters.NumberOfParents; i++)
            {
                indices[i] = sorted.Indices[generatedNumbers[i]];
            }

            return indices;
        }

        private int[] PerformTournamentSelection()
        {
            // Initialize numbers to indices
            int[] indices = new int[Parameters.NumberOfParents];
            // Find indices one by one
            for (int i = 0; i < Parameters.NumberOfParents; i++)
            {
                // Reinitialize trial variables
                double trialFitness = double.MinValue;
                int indexChampion = -1;
                // Make a tournament
                for (int j = 0; j < Parameters.TournamentSize; j++)
                {
                    // Generate index
                    int randomIndex = RandomEngine.Instance.GenerateIntsUniformlyDistributed(0, Parameters.Size);
                    if (trialFitness < Fitness[randomIndex])
                    {
                        trialFitness = Fitness[randomIndex];
                        indexChampion = randomIndex;
                    }
                }
                indices[i] = indexChampion;
            }

            return indices;
        }

        #endregion Selection

        #region Crossover

        private double[] PerformCrossover(int[] parentsIndices)
        {
            switch (Parameters.CrossoverType)
            {
                case RvgaCrossoverType.Intermediate:
                    return PerformIntermediateCrossover(parentsIndices);
                case RvgaCrossoverType.WeightedIntermediate:
                    return PerformWeighedCrossover(parentsIndices);
                case RvgaCrossoverType.Uniform:
                    return PerformUniformCrossover(parentsIndices);
            }

            return null;
        }

        private double[] PerformIntermediateCrossover(int[] parentsIndices)
        {
            // Get the parents vectors
            double[][] parents = GetParents(parentsIndices);

            // Get an offspring
            double[] offspring = VectorProcessor.Instance.CalcualteSum(parents);
            offspring = VectorProcessor.Instance.ScaleByValue(vector: offspring, value: 1.0 / Parameters.NumberOfParents);

            return offspring;
        }

        private double[] PerformWeighedCrossover(int[] parentsIndices)
        {
            // Get the parents vectors
            double[][] parents = GetParents(parentsIndices);
            // Get their fitnesses
            double[] parentsFitness = GetParentsFitness(parentsIndices);

            // Get an offspring
            double[] offspring = VectorProcessor.Instance.ClaculateWeightedSum(parents, parentsFitness);

            return offspring;
        }

        private double[] PerformUniformCrossover(int[] parentsIndices)
        {
            // Get the parents vectors
            double[][] parents = GetParents(parentsIndices);
            // Initialize offspring
            double[] offspring = new double[Problem.Dimension];
            // For all the dims get the value
            for (int i = 0; i < Problem.Dimension; i++)
            {
                int randomIndex = RandomEngine.Instance.GenerateIntsUniformlyDistributed(from: 0, to: Parameters.NumberOfParents);
                offspring[i] = parents[randomIndex][i];
            }

            return offspring;
        }

        private double[][] GetParents(int[] parentsIndices)
        {
            // Initialize the parents
            double[][] parents = new double[parentsIndices.Length][];

            for (int i = 0; i < parentsIndices.Length; i++)
            {
                // Initialize each parent
                parents[i] = new double[Problem.Dimension];
                // Fill in
                for (int j = 0; j < Problem.Dimension; j++)
                {
                    parents[i][j] = Population[parentsIndices[i]][j];
                }
            }

            return parents;
        }

        private double[] GetParentsFitness(int[] parentsIndices)
        {
            // Initialize the vector of fitness vals
            double[] fitnesses = new double[parentsIndices.Length];

            // Assign
            for (int i = 0; i < parentsIndices.Length; i++)
            {
                fitnesses[i] = Fitness[i]; 
            }

            return fitnesses;
        }

        #endregion Crossover

        #region Mutation

        private double[] PerformMutation(double[] offspring)
        {
            switch (Parameters.MutationType)
            {
                case RvgaMutationType.Uniform:
                    return PerformUniformMutation(offspring);
                case RvgaMutationType.ProbabilisticUniform:
                    return PerformProbabilisticUniformMutation(offspring);
                case RvgaMutationType.NonUniform:
                    return PerformNonUniformMutation(offspring);
                case RvgaMutationType.Power:
                    return PerformPowerMutation(offspring);
                case RvgaMutationType.Additive:
                    return PerformAdditiveMutation(offspring);
                case RvgaMutationType.ProbabilisticAdditive:
                    return PerformProbabilisticAdditiveMutation(offspring);
            }

            return null;
        }

        private double[] PerformUniformMutation(double[] offspring)
        {
            // Make mutations for the given number of genes to mutate
            for (int i = 0; i < Parameters.MutationNumberOfGenes; i++)
            {
                // Choose index
                int radomIndex = RandomEngine.Instance.GenerateIntsUniformlyDistributed(0, Problem.Dimension);
                // Mutate the gene for that index
                offspring[radomIndex] = RandomEngine.Instance.GenerateUniformlyDistributed(Parameters.MutateFrom[radomIndex], Parameters.MutateTo[radomIndex]);
            }

            return offspring;
        }

        private double[] PerformProbabilisticUniformMutation(double[] offspring)
        {
            // Make mutations for all the genes with the given probability
            for (int i = 0; i < Problem.Dimension; i++)
            {
                // Generate the mutation condition
                bool mutateCondition = RandomEngine.Instance.GenerateUniformlyDistributed() < Parameters.MutationProbability;
                // Provide the mutation for the current gene
                if (mutateCondition)
                {
                    offspring[i] = RandomEngine.Instance.GenerateUniformlyDistributed(Parameters.MutateFrom[i], Parameters.MutateTo[i]);
                }
            }

            return offspring;
        }

        private double[] PerformNonUniformMutation(double[] offspring)
        {
            throw new NotImplementedException();
        }

        private double[] PerformPowerMutation(double[] offspring)
        {
            throw new NotImplementedException();
        }

        private double[] PerformAdditiveMutation(double[] offspring)
        {
            // Make mutations for the given number of genes to mutate
            for (int i = 0; i < Parameters.MutationNumberOfGenes; i++)
            {
                // Choose index
                int radomIndex = RandomEngine.Instance.GenerateIntsUniformlyDistributed(0, Problem.Dimension);
                // Calculate the random valuse
                double randomValue = RandomEngine.Instance.GenerateNormallyDistributed(0, Parameters.MutationAdditiveSD);
                // Mutate the gene for that index
                offspring[radomIndex] += randomValue;
            }

            return offspring;
        }

        private double[] PerformProbabilisticAdditiveMutation(double[] offspring)
        {
            // Make mutations for all the genes with the given probability
            for (int i = 0; i < Problem.Dimension; i++)
            {
                // Generate the mutation condition
                bool mutateCondition = RandomEngine.Instance.GenerateUniformlyDistributed() < Parameters.MutationProbability;
                // Provide the mutation for the current gene
                if (mutateCondition)
                {
                    // Calculate the random valuse
                    double randomValue = RandomEngine.Instance.GenerateNormallyDistributed(0, Parameters.MutationAdditiveSD);
                    // Mutate
                    offspring[i] += randomValue;
                }
            }

            return offspring;
        }


        #endregion Mutation
    }
}
