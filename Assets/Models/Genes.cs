using System;
using UnityEngine;

public class Genes {

    private const double GENETIC_DIVISOR = 5.0;

    // 1 - 100
    public int pigment;
    public int hair;
    public int eyes;
    // 1-20
    public int beauty;

    private static System.Random randy = new System.Random();

    public Genes(Culture culture)
    {
        this.pigment = culture.randomPigment();
        this.hair = culture.randomHair();
        this.eyes = culture.randomEyes();
        this.pigment = randy.Next(1, 21);
    }

    public Genes(Person parent1, Person parent2)
    {
        this.pigment = randy.Next(geneRangeMin(parent1.genes.pigment, parent2.genes.pigment, 1), geneRangeMax(parent1.genes.pigment, parent2.genes.pigment, 100));
        this.hair = randy.Next(geneRangeMin(parent1.genes.hair, parent2.genes.hair, 1), geneRangeMax(parent1.genes.hair, parent2.genes.hair, 100));
        this.eyes = randy.Next(geneRangeMin(parent1.genes.eyes, parent2.genes.eyes, 1), geneRangeMax(parent1.genes.eyes, parent2.genes.eyes, 100));
        this.pigment = randy.Next(geneRangeMin(parent1.genes.beauty, parent2.genes.beauty, 1), geneRangeMax(parent1.genes.beauty, parent2.genes.beauty, 100));
    }

    public Genes(Person parent1, Culture culture)
    {
        Genes parent2Genes = new Genes(culture);
        Debug.Log(parent1.genes);
        this.pigment = randy.Next(geneRangeMin(parent1.genes.pigment, parent2Genes.pigment, 1), geneRangeMax(parent1.genes.pigment, parent2Genes.pigment, 100));
        this.hair = randy.Next(geneRangeMin(parent1.genes.hair, parent2Genes.hair, 1), geneRangeMax(parent1.genes.hair, parent2Genes.hair, 100));
        this.eyes = randy.Next(geneRangeMin(parent1.genes.eyes, parent2Genes.eyes, 1), geneRangeMax(parent1.genes.eyes, parent2Genes.eyes, 100));
        this.pigment = randy.Next(geneRangeMin(parent1.genes.beauty, parent2Genes.beauty, 1), geneRangeMax(parent1.genes.beauty, parent2Genes.beauty, 100));
    }

    private int geneRangeMin(int parent1, int parent2, int minValue)
    {
        int diff = Math.Abs(parent1 - parent2);
        return (int) Math.Round(Math.Max(Math.Min(parent1 - (diff / GENETIC_DIVISOR), parent2 - (diff / GENETIC_DIVISOR)), minValue), 0);
    }

    private int geneRangeMax(int parent1, int parent2, int maxValue)
    {
        int diff = Math.Abs(parent1 - parent2);
        return (int)Math.Round(Math.Min(Math.Max(parent1 + (diff / GENETIC_DIVISOR), parent2 + (diff / GENETIC_DIVISOR)), maxValue), 0);
    }

    public override string ToString()
    {
        return "pigment: " + pigment + "\thair: " + hair + "\teyes: " + eyes + "\tbeauty: " + beauty;
    }

}
