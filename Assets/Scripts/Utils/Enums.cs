using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ATypeEntityStates //추격, 경계 x
{
    Indifference,
    Interaction
}
public enum BTypeEntityStates //추격 o, 경계 x
{
    Indifference, 
    Interaction,
    Aggressive, 
    Chase 
}
public enum CTypeEntityStates //추격 x, 경계 o
{
    Indifference,
    Watch,
    Interaction
}
public enum DTypeEntityStates //추격 o, 경계 o
{
    Indifference, 
    Watch,
    Interaction, 
    Aggressive, 
    Chase 
}