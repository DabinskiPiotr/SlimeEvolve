using System.Collections.Generic;
//A class that handles Elements and their relations to each other.
public enum Element { Dark, Fire, Water, Air, Rock, Electric, Ice, Light, None };
public class Elements
{
    Dictionary<Element, Element> strongAgainst = new Dictionary<Element, Element>();
    Dictionary<Element, Element> weakAgainst = new Dictionary<Element, Element>();
    public static List<Element> elements;

    public Elements()
    {
        SetupElements();
    }
    //Function that handles setting up the dictionaries for elements.
    void SetupElements()
    {
        elements = new List<Element>()
        {
            Element.Dark,
            Element.Fire,
            Element.Water,
            Element.Air,
            Element.Rock,
            Element.Electric,
            Element.Ice,
            Element.Light,
            Element.None
        };

        strongAgainst.Add(Element.Water, Element.Fire);
        strongAgainst.Add(Element.Fire, Element.Ice);
        strongAgainst.Add(Element.Ice, Element.Air);
        strongAgainst.Add(Element.Air, Element.Rock);
        strongAgainst.Add(Element.Rock, Element.Electric);
        strongAgainst.Add(Element.Electric, Element.Water);
        strongAgainst.Add(Element.Dark, Element.Light);
        strongAgainst.Add(Element.Light, Element.Dark);

        weakAgainst.Add(Element.Water, Element.Electric);
        weakAgainst.Add(Element.Electric, Element.Rock);
        weakAgainst.Add(Element.Rock, Element.Air);
        weakAgainst.Add(Element.Air, Element.Ice);
        weakAgainst.Add(Element.Ice, Element.Fire);
        weakAgainst.Add(Element.Fire, Element.Water);
        weakAgainst.Add(Element.Dark, Element.Dark);
        weakAgainst.Add(Element.Light, Element.Light);

    }
    //Function that calculates the elemental damage multiplier based on provided element that deals and element that is recieving the damage.
    public float DamageMultiplier(Element elementDeal, Element elementRecieve)
    {
        if (elementDeal == Element.None || elementRecieve == Element.None)
        {
            return 1;
        }
        if (elementDeal == elementRecieve)
        {
            return 0.75f;
        }
        if (strongAgainst[elementDeal] == elementRecieve)
        {
            return 1.5f;
        }
        if (weakAgainst[elementDeal] == elementRecieve)
        {
            return 0.5f;
        }
        return 1;
    }
}
