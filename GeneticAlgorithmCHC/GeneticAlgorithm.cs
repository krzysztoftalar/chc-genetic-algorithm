using System;
using System.Linq;

namespace GeneticAlgorithmCHC
{
    public class GeneticAlgorithm
    {
        private readonly int _chromosomeLength;
        private readonly int _populationSize;
        private int _threshold;

        public GeneticAlgorithm(int populationSize, int chromosomeLength)
        {
            _populationSize = populationSize;
            _chromosomeLength = chromosomeLength;
            _threshold = _chromosomeLength / 4;
        }

        public Population InitPopulation()
        {
            return new Population(_populationSize, _chromosomeLength);
        }

        public void EvalPopulation(Population population)
        {
            double populationFitness = 0;

            foreach (var individual in population.GetIndividuals())
            {
                populationFitness += CalcFitness(individual);
            }

            population.SetPopulationFitness(populationFitness);
        }

        public double CalcFitness(Individual individual)
        {
            int correctGenes = 0;

            for (int i = 0; i < _chromosomeLength; i++)
            {
                if (individual.GetGene(i) == individual.GetTarget(i))
                {
                    correctGenes++;
                }
            }

            double fitness = (double)correctGenes / _chromosomeLength;

            individual.SetFitness(fitness);

            return fitness;
        }

        public static bool IsTerminationConditionMet(Population population)
        {
            return population.GetIndividuals().Any(individual => individual.GetFitness() == 1);
        }

        public Individual SelectParent(Population population)
        {
            var individuals = population.GetIndividuals();

            var index = new Random().Next(0, _populationSize - 1);

            return individuals[index];
        }

        public Population CrossoverPopulation(Population population)
        {
            var childrenPopulation = new Population();

            var newPopulation = new Population();
            newPopulation.SetIndividual(0, population.GetFittest(0));

            for (int i = 0; i < _populationSize / 2; i++)
            {
                var parent1 = SelectParent(population);
                var parent2 = SelectParent(population);

                if (HammingDistance(parent1, parent2) > _threshold)
                {
                    var (child1, child2) = CrossoverHUX(parent1, parent2);

                    newPopulation.AddIndividual(child1);
                    newPopulation.AddIndividual(child2);

                    population.AddIndividual(child1);
                    population.AddIndividual(child2);

                    childrenPopulation.AddIndividual(child1);
                    childrenPopulation.AddIndividual(child2);
                }
            }

            EvalPopulation(childrenPopulation);

            if (childrenPopulation.GetPopulationLength() == _populationSize)
            {
                _threshold--;
            }
            else
            {
                population = newPopulation;
                EvalPopulation(population);
            }

            if (_threshold == 0)
            {
                population = InitPopulationFromBest(population);

                _threshold = _chromosomeLength / 4;
            }

            population = population.Sort(population);
            population.Resize(_populationSize);

            return population;
        }

        public Population InitPopulationFromBest(Population population)
        {
            var individuals = population.GetIndividuals();
            var individual = population.GetFittest(0);

            var newPopulation = new Population();
            newPopulation.SetIndividual(0, individual);

            for (int i = 1; i < _populationSize; i++)
            {
                newPopulation.SetIndividual(i, individual);

                for (int j = 0; j < _chromosomeLength; j++)
                {
                    if (0.35 > new Random().NextDouble())
                    {
                        individuals[i].Flip(j);
                    }
                }

                newPopulation.SetIndividual(i, individuals[i]);
            }

            return newPopulation;
        }

        public int HammingDistance(Individual parent1, Individual parent2)
        {
            int hd = 0;

            for (int i = 0; i < _chromosomeLength; i++)
            {
                if (parent1.GetGene(i) != parent2.GetGene(i))
                {
                    hd++;
                }
            }

            return hd;
        }

        public (Individual child1, Individual child2) CrossoverHUX(Individual parent1, Individual parent2)
        {
            int del = HammingDistance(parent1, parent2) / 2;

            var child1 = new Individual(_chromosomeLength);
            var child2 = new Individual(_chromosomeLength);

            for (int i = 0; i < _chromosomeLength; i++)
            {
                var child1Gene = parent1.GetGene(i);
                var child2Gene = parent2.GetGene(i);

                bool c1 = child1Gene == 1;
                bool c2 = child2Gene == 1;

                if (del > 0 && child1Gene != child2Gene && new Random().NextDouble() < 0.5)
                {
                    c1 = !c1;
                    c2 = !c2;
                    del--;
                }

                child1.SetGene(i, Convert.ToInt32(c1));
                child2.SetGene(i, Convert.ToInt32(c2));
            }

            return (child1, child2);
        }
    }
}