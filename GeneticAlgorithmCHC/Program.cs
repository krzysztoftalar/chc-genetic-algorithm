using System;

namespace GeneticAlgorithmCHC
{
    class Program
    {
        static void Main(string[] args)
        {
            int chromosomeLength = Individual.Target.Length;
            
            var ga = new GeneticAlgorithm(100, chromosomeLength);
            var population = ga.InitPopulation();
            int generation = 1;

            ga.EvalPopulation(population);
            
            while (GeneticAlgorithm.IsTerminationConditionMet(population) == false)
            {
                Console.WriteLine($"Najlepsze rozwiązanie: {population.GetFittest(0)}");

                population = ga.CrossoverPopulation(population);

                generation++;
            }

            Console.WriteLine($"Znaleziono najlepsze rozwiązanie w {generation} generacjach");
            Console.WriteLine($"Rozwiązanie: {population.GetFittest(0)}");
        }
    }
}
