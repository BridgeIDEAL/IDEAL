using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ATypeEntityStates //�߰�, ��� x
{
    Indifference,
    Interaction
}
public enum BTypeEntityStates //�߰� o, ��� x
{
    Indifference, 
    Interaction,
    Aggressive, 
    Chase 
}
public enum CTypeEntityStates //�߰� x, ��� o
{
    Indifference,
    Watch,
    Interaction
}
public enum DTypeEntityStates //�߰� o, ��� o
{
    Indifference, 
    Watch,
    Interaction, 
    Aggressive, 
    Chase 
}