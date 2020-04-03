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
            bool g = _chromosome[j] == 1;
            g = !g;

            _chromosome[j] = Convert.ToInt32(g);
        }

        public int GetTarget(int offset)
        {
            return Target[offset];
        }

        public void SetGene(int offset, int gene)
        {
            _chromosome[offset] = gene;
        }

        public int GetGene(int offset)
        {
            return _chromosome[offset];
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