using System.Collections.Generic;
using UnityEngine;

//Evolution class that is responsible for initializing genes and evolving enemies.
public class Evolution
{

    List<Gene> genes;
    int geneChance = 7;

    string[] extraPossibleBehaviours = { "charge", "toxic", "rangeAttack", "flying", "dash", "thorns" };
    //Constructor for the evolution clas. Used to initialize the list of genes later assigned to enemies.
    public Evolution(int geneNumber, List<Gene> collectedGenes)
    {
        if (WavesCounter.wavesCounter == 1)
        {
            InitialiseGenes(geneNumber);
        }
        else
        {
            NewGeneration(collectedGenes);
        }
    }
    //A function that initializes genes on the first wave. The random values correspond to the minimal, maximal and all between statistic that an enemy can initially recieve in their gene.
    void InitialiseGenes(int geneNumber)
    {
        genes = new List<Gene>();
        for (int i = 0; i < geneNumber; i++)
        {
            int id = i;
            int attack = Random.Range(1, 15);
            int maxHealth = Random.Range(80, 140);
            float speed = Random.Range(2f, 3.5f);
            float attackSpeed = Random.Range(0.6f, 0.9f);
            float attackCooldown = Random.Range(2f, 4f);
            int bulletSpeed = Random.Range(9, 14);
            float attackRange = Random.Range(1f, 2f);
            float visionRange = Random.Range(9f, 14f);
            List<string> behaviours = RollBehaviours();
            Element element = Elements.elements[Random.Range(0, 8)];
            int damageDealt = 0;

            Gene newGene = new Gene(
                id,
                attack,
                maxHealth,
                speed,
                attackSpeed,
                attackCooldown,
                bulletSpeed,
                attackRange,
                visionRange,
                behaviours,
                element,
                damageDealt
                );
            genes.Add(newGene);
        }
    }
    //A function that creates a new generation from collected and already sorted in previous wave genes. It copies most of the best performed enemy genes, mutates them and adds to the new list.
    //For the missiing genes it combines them from two parents of best performers.
    void NewGeneration(List<Gene> collectedGenes)
    {
        genes = new List<Gene>();
        for (int i = 0; i < collectedGenes.Count; i++)
        {
            if (i >= (collectedGenes.Count - collectedGenes.Count / 6))
            {
                Gene combinedGene = CombineGenes(i, collectedGenes);
                MutateGene(combinedGene);
                combinedGene.attack += 1;
                combinedGene.maxHealth += 3;
                genes.Add(combinedGene);
            }
            else
            {
                Gene newGene = new Gene(
                   collectedGenes[i].id,
                   collectedGenes[i].attack,
                   collectedGenes[i].maxHealth,
                   collectedGenes[i].speed,
                   collectedGenes[i].attackSpeed,
                   collectedGenes[i].attackCooldown,
                   collectedGenes[i].bulletSpeed,
                   collectedGenes[i].attackRange,
                   collectedGenes[i].visionRange,
                   collectedGenes[i].behaviours,
                   collectedGenes[i].element,
                   0
                   );
                MutateGene(newGene);
                genes.Add(newGene);

            }
        }
    }
    //Function responsible for the mutation of a gene. 
    Gene MutateGene(Gene gene)
    {
        if (Random.Range(0, 20) == 0)
        {
            int rollStat = Random.Range(0, 8);
            int increaseOrDecrease = 1;

            if (Random.Range(0, WavesCounter.wavesCounter) == 0)
            {
                increaseOrDecrease = -1;
            }
            switch (rollStat)
            {
                case 0:
                    gene.attack += (gene.attack / 10 * increaseOrDecrease);
                    break;
                case 1:
                    gene.maxHealth += (gene.maxHealth / 10 * increaseOrDecrease);
                    break;
                case 2:
                    gene.speed += (gene.speed / 10 * increaseOrDecrease);
                    break;
                case 3:
                    gene.attackSpeed += (gene.attackSpeed / 10 * increaseOrDecrease);
                    break;
                case 4:
                    gene.attackCooldown += (gene.attackCooldown / 10 * increaseOrDecrease);
                    break;
                case 5:
                    gene.bulletSpeed += (gene.bulletSpeed / 10 * increaseOrDecrease);
                    break;
                case 6:
                    gene.attackRange += (gene.attackRange / 10 * increaseOrDecrease);
                    break;
                case 7:
                    gene.visionRange += (gene.visionRange / 10 * increaseOrDecrease);
                    break;
            }
        }
        return gene;
    }
    //Function that finds and returns two different parents genes that are later used for combinig.
    Gene[] RollParents(List<Gene> collectedGenes)
    {
        Gene[] array = new Gene[2];
        int firstParent = Random.Range(0, collectedGenes.Count - collectedGenes.Count / 6);
        int secondParent = Random.Range(0, collectedGenes.Count - collectedGenes.Count / 6);
        while (firstParent == secondParent)
        {
            secondParent = Random.Range(0, collectedGenes.Count - collectedGenes.Count / 6);
        }
        array[0] = collectedGenes[firstParent];
        array[1] = collectedGenes[secondParent];
        return array;
    }
    //Function used for combining two genes into one.
    Gene CombineGenes(int id, List<Gene> collectedGenes)
    {
        Gene[] parentGenes = RollParents(collectedGenes);
        Gene newGene = new Gene(
            id,
            parentGenes[Random.Range(0, 2)].attack,
            parentGenes[Random.Range(0, 2)].maxHealth,
            parentGenes[Random.Range(0, 2)].speed,
            parentGenes[Random.Range(0, 2)].attackSpeed,
            parentGenes[Random.Range(0, 2)].attackCooldown,
            parentGenes[Random.Range(0, 2)].bulletSpeed,
            parentGenes[Random.Range(0, 2)].attackRange,
            parentGenes[Random.Range(0, 2)].visionRange,
            InheritBehaviours(parentGenes),
            parentGenes[Random.Range(0, 2)].element,
            0
            );
        return newGene;
    }
    //Function that lets enemies inherit behaviours based on the parent's behaviour lists. If both parents have a behaviour, the child will have it. If one, there is a 50% chance. If none, the chance does not exist.
    List<string> InheritBehaviours(Gene[] parentGenes)
    {
        List<string> combinedBehaviours = new List<string>();
        for (int i = 0; i < extraPossibleBehaviours.Length; i++)
        {
            if (parentGenes[0].behaviours.Contains(extraPossibleBehaviours[i]) && parentGenes[1].behaviours.Contains(extraPossibleBehaviours[i]))
            {
                combinedBehaviours.Add(extraPossibleBehaviours[i]);
            }
            else if (parentGenes[0].behaviours.Contains(extraPossibleBehaviours[i]) || parentGenes[1].behaviours.Contains(extraPossibleBehaviours[i]))
            {
                if (Random.Range(0, 2) == 0)
                {
                    combinedBehaviours.Add(extraPossibleBehaviours[i]);
                }
            }
        }
        return combinedBehaviours;
    }
    //Function responsible for randomly initializing the behaviour list for a gene.
    List<string> RollBehaviours()
    {
        List<string> behaviours = new List<string>();
        if (Random.Range(0, geneChance) == 0)
        {
            behaviours.Add("rangeAttack");
        }
        if (Random.Range(0, geneChance) == 0)
        {
            behaviours.Add("charge");
        }
        if (Random.Range(0, geneChance) == 0)
        {
            behaviours.Add("toxic");
        }
        if (Random.Range(0, geneChance) == 0)
        {
            behaviours.Add("thorns");
        }
        if (Random.Range(0, geneChance) == 0)
        {
            behaviours.Add("flying");
        }
        if (Random.Range(0, geneChance) == 0)
        {
            behaviours.Add("dash");
        }
        return behaviours;
    }
    //Function that returns an i-th gene.
    public Gene GetNextGene(int i)
    {
        return genes[i];
    }
}
