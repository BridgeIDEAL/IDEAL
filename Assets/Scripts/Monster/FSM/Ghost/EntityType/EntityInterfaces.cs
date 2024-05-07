using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInterfaces { }

public interface IPatrol{
    public void Patrol();
    public void SeekNextRoute();
    public void StopPatrol();
}