using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] private Material material;

    private float dissolveAmount;
    private float dissolveSpeed;
    private bool isDissolving;

    private void Update()
    {
        if (isDissolving)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount + dissolveSpeed * Time.deltaTime);
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }

    public float GetDissolveAmount() => material.GetFloat("_DissolveAmount");

    public void ResetDissolveAmount()
    {
        isDissolving = false;
        material.SetFloat("_DissolveAmount", 0);
    }

    public void StartDissolve(float speed = 1)
    {
        isDissolving = true;
        dissolveSpeed = speed;
    }

    public void StopDissolve(float speed)
    {
        isDissolving = false;
        dissolveSpeed = speed;
    }

}
