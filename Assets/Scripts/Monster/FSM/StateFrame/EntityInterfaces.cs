using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInterfaces { }

public interface IPatrol{
    public void StartPatrol();
    public void Patrol();
    public void EndPatrol();
    public void SeekNextRoute();
}