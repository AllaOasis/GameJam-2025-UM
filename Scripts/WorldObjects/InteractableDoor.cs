using UnityEngine;
using System.Collections;

public class InteractableDoor : Interactable
{
    [SerializeField] private float rotateAmount = 110f;
    [SerializeField] private float rotateTime = 1f;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    private Coroutine rotateRoutine;
    private bool isOpen;

    private void Awake()
    {
        closedRotation = transform.localRotation;
        openRotation = Quaternion.Euler(0f, rotateAmount, 0f) * closedRotation;
    }

    public override bool Interact()
    {
        if (rotateRoutine != null)
            StopCoroutine(rotateRoutine);

        Quaternion target = isOpen ? closedRotation : openRotation;
        rotateRoutine = StartCoroutine(RotateDoor(target));

        isOpen = !isOpen;
        return true;
    }

    private IEnumerator RotateDoor(Quaternion target)
    {
        Quaternion start = transform.localRotation;
        float elapsed = 0f;

        while (elapsed < rotateTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / rotateTime;

            transform.localRotation = Quaternion.Slerp(start, target, t);
            yield return null;
        }

        transform.localRotation = target;
        rotateRoutine = null;
    }
}