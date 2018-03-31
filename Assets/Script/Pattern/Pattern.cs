using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern
{
    public Unit owner;

    public abstract IEnumerator PatternFramework(Pattern prevPattern);
}
