using System;
using System.Linq;

namespace GeneticAlgorithmCHC
{
    public class Individual
    {
        private readonly int[] _chromosome;

        public static readonly int[] Target = {
            1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0,
            1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0
        };

        private double Fitness { get; set; } = -1;

        public Individual(int chromosomeLength)
        {
            _chromosome = new int[chromosomeLength];

            for (int i = 0; i < chromosomeLength; i++)
            {
                SetGene(i, 0.5 < new Random().NextDouble() ? 1 : 0);
            }
        }

        public void Flip(int j)
        {
            bool gene = _chromosome[j] == 1;
            gene = !gene;

            _chromosome[j] = Convert.ToInt32(gene);
        }

        public int GetTarget(int index)
        {
            return Target[index];
        }

        public void SetGene(int index, int gene)
        {
            _chromosome[index] = gene;
        }

        public int GetGene(int index)
        {
            return _chromosome[index];
        }

        public void SetFitness(double fitness)
        {
            Fitness = fitness;
        }

        public double GetFitness()
        {
            return Fitness;
        }

        public override string ToString()
        {
            return _chromosome.Aggregate("", (output, gene) => output + gene);
        }
    }
}