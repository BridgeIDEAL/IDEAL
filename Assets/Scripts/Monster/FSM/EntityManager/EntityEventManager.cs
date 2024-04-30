using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEventManager
{
    public Func<string, BaseEntity> SearchEntity;
    public Action<string> SpawnEntity;
    public Action<string> DespawnEntity;

}
