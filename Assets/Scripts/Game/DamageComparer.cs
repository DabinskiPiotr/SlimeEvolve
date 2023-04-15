using System.Collections.Generic;
//A damage Comparer class, neded for sorting the gene list based on the damage dealt variable.
public class DamageComparer : IComparer<Gene>
{
    public int Compare(Gene a, Gene b)
    {
        //int difference = a.damageDealt.CompareTo(b.damageDealt); sorting in ascending order
        int difference = b.damageDealt.CompareTo(a.damageDealt);// sorting in descending order
        if (a == null && b == null)
        {
            return 0;
        }
        return (difference != 0) ? difference : 1;
    }
}