using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceTalk : MonoBehaviour
{
    bool once = true;
    InteractionConditionConversation interaction = null;
    public void Setup(InteractionConditionConversation _interaction, Entity _entity)
    {
        if (_entity.speakIndex == -1)
        {
            Destroy(this);
            return;
        }
        interaction = _interaction;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && once)
        {
            interaction.ForceInteraction();
            once = false;
        }
    }
}
