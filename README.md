<h1 align="center">CHC Genetic Algorithm</h1>

<p align="center">
<img src="https://img.shields.io/badge/made%20by-krzysztoftalar-blue.svg" />

<img src="https://img.shields.io/badge/-C%23-blueviolet" />

<img src="https://img.shields.io/badge/license-MIT-green" />
</p>

## About The Project

_The project was created for [Artificial Intelligence Methods](https://www.wbmii.ath.bielsko.pl) classes._

A CHC is a non-traditional Genetic Algorithm which combines a conservative selection strategy (that always preserves the best individuals found so far) with a highly disruptive recombination (HUX) that produces offsprings that are maximally different from their two parents.

The traditional though of preferring a recombination operator with a low disrupting properties may not hold when such a conservative selection strategy is used. On the contrary, certain highly disruptive crossover operator provide more effective search in many problems, which represents the core idea behind the CHC search method.

This algorithm introduce a new bias against mating individuals who are too similar (incest prevention). Mutation is not performed, instead, a restart process re-introduces diversity whenever convergence is detected.

## License

This project is licensed under the MIT License.

## Contact

**Krzysztof Talar** - [Linkedin](https://www.linkedin.com/in/ktalar/) - krzysztoftalar@protonmail.com
