using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perk : MonoBehaviour
{
     public abstract void Activate();
     public abstract void LevelUp();
}

public abstract class Ability : MonoBehaviour
{
     public abstract void Use();
     public abstract void LevelUp();
}
