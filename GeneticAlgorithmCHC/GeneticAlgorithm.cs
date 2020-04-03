using System;
using System.Collections.Generic;
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
                    correctGenes += 1;
                }
            }

            double fitness = (double) correctGenes / _chromosomeLength;

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
                    var offSpring = CrossoverHUX(parent1, parent2);

                    newPopulation.AddIndividual(offSpring.off1);
                    newPopulation.AddIndividual(offSpring.off2);

                    population.AddIndividual(offSpring.off1);
                    population.AddIndividual(offSpring.off2);

                    childrenPopulation.AddIndividual(offSpring.off1);
                    childrenPopulation.AddIndividual(offSpring.off2);
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

        public (Individual off1, Individual off2) CrossoverHUX(Individual parent1, Individual parent2)
        {
            int del = HammingDistance(parent1, parent2) / 2;

            var child1 = new Individual(_chromosomeLength);
            var child2 = new Individual(_chromosomeLength);

            int c1, c2;

            for (int i = 0; i < _chromosomeLength; i++)
            {
                c1 = parent1.GetGene(i);
                c2 = parent2.GetGene(i);

                bool a = c1 == 1;
                bool b = c2 == 1;

                if (del > 0 && c1 != c2 && new Random().NextDouble() < 0.5)
                {
                    a = !a;
                    b = !b;
                    del--;
                }

                child1.SetGene(i, Convert.ToInt32(a));
                child2.SetGene(i, Convert.ToInt32(b));
            }

            return (child1, child2);
        }
    }
}