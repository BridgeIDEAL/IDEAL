using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInterfaces { }

public interface IPatrol{
    public IEnumerator PatrolCor();
    public void SeekNextRoute();
    public void OnOffPatrol(bool _isOnPatrol);
}