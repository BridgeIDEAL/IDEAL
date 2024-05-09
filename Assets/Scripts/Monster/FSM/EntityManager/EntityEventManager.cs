using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEventManager
{
    public string chaseEntityName { get; set; }
    private bool isChasePlayer = false;
    public bool IsChasePlayer
    {
        get
        {
            return isChasePlayer;
        }
        set
        {
            if (value == isChasePlayer)
                return;
            else
            {
                isChasePlayer = value;
                // �÷��̾ �������� ���ϵ��� ����
            }
        }
    }
    // Manage Entity
    public Func<string, BaseEntity> SearchEntity; // parameter = string, output = BaseEntity
    public Action<string> SpawnEntity;
    public Action<string> DespawnEntity;
    // SendMessage All Entity
    public Action BroadCastCalmDown;
    public Action<string> BroadCastStartConversation;
    public Action<string> BroadCastEndConversation;
    public Action<string> BroadCastChase;
    public Action<string> BroadCastPenalty;
}
