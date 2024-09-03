using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] float dissolveTime;

    List<Material> dissolveMats = new List<Material>();
    
    public void Init()
    {
        SkinnedMeshRenderer[] _renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        int _rendererCnt = _renderers.Length;
        for(int i=0; i<_rendererCnt; i++)
        {
            dissolveMats.AddRange(_renderers[i].materials);
        }
    }

    #region Dissolve
    public void Dissolve(UnityAction _endAction = null)
    {
        StartCoroutine(CDissolve(_endAction));
    }

    IEnumerator CDissolve(UnityAction _endAction)
    {
        float _timer = 0;
        int _matCnt = dissolveMats.Count;
        float _dissolveVal = 0f;

        while (_timer < dissolveTime)
        {
            _timer += Time.deltaTime;
            _dissolveVal = Mathf.Lerp(1, 0, _timer / dissolveTime);
            for(int i=0; i<_matCnt; i++)
            {
                dissolveMats[i].SetFloat("_Split", _dissolveVal);
            }
            yield return null;
        }

        for (int i = 0; i < _matCnt; i++)
        {
            dissolveMats[i].SetFloat("_Split", 0);
        }

        if (_endAction != null)
            _endAction();
    }
    #endregion

    #region Restore Dissolve
    public void RestoreDissolve(UnityAction _endAction = null)
    {
        StartCoroutine(CRestoreDissolve(_endAction));
    }

    IEnumerator CRestoreDissolve(UnityAction _endAction = null)
    {
        float _timer = 0;
        int _matCnt = dissolveMats.Count;
        float _dissolveVal = 0f;

        while (_timer < dissolveTime)
        {
            _timer += Time.deltaTime;
            _dissolveVal = Mathf.Lerp(0, 1, _timer / dissolveTime);
            for (int i = 0; i < _matCnt; i++)
            {
                dissolveMats[i].SetFloat("_Split", _dissolveVal);
            }
            yield return null;
        }

        for (int i = 0; i < _matCnt; i++)
        {
            dissolveMats[i].SetFloat("_Split", 1);
        }

        if (_endAction != null)
            _endAction();
    }
    #endregion
}
