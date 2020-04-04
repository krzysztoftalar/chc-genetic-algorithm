using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmCHC
{
    public class Population
    {
        private List<Individual> _population;
        private double _populationFitness = -1;

        public Population()
        {
            _population = new List<Individual>();
        }

        public Population(int populationSize, int chromosomeLength)
        {
            _population = new List<Individual>();

            for (int i = 0; i < populationSize; i++)
            {
                var individual = new Individual(chromosomeLength);

                _population.Add(individual);
            }
        }

        public List<Individual> GetIndividuals()
        {
            return _population;
        }

        public Individual GetFittest(int index)
        {
            _population = _population
                .OrderByDescending(x => x.GetFitness())
                .ToList();

            return _population[index];
        }

        public Population Sort(Population population)
        {
           return new Population()
           {
               _populationFitness = population._populationFitness,
               _population = population._population
                   .OrderByDescending(x => x.GetFitness())
                   .ToList()
           };
        }

        public int GetPopulationLength()
        {
            return _population.Count;
        }

        public void SetPopulationFitness(double fitness)
        {
            _populationFitness = fitness;
        }

        public void SetIndividual(int index, Individual individual)
        {
            _population.Insert(index, individual);
        }

        public void AddIndividual(Individual individual)
        {
            _population.Add(individual);
        }

        public void Resize(int size)
        {
            int count = _population.Count;

            if (size < count)
            {
                _population.RemoveRange(size, count - size);
            }
            else if (size > count)
            {
                if (size > _population.Capacity)
                    _population.Capacity = size;

                _population.AddRange(Enumerable.Repeat(
                    new Individual(Individual.Target.Length), size - count));
            }
        }
    }
}