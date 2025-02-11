using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class SpinWheel : MonoBehaviour
{
    public AnimationCurve AnimationCurve;

    private bool m_spinning = false;

    public void Spin()
    {
        if (!m_spinning)
            StartCoroutine(DoSpin());
    }

    private IEnumerator DoSpin()
    {
        m_spinning = true;
        var timer = 0.0f;
        var startAngle = transform.eulerAngles.z;

        var time = 3.0f;
        var maxAngle = 270.0f;

        while (timer < time)
        {
            var angle = AnimationCurve.Evaluate(timer / time) * maxAngle;
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
        m_spinning = false;
    }
    
}
