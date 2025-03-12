using System.Collections;
using UnityEngine;

public class DownTrap : MonoBehaviour
{
    public float minWaitTime = 0.1f;  
    public float maxWaitTime = 3f;    
    public float downDistance = 2f;   
    public float downSpeed = 10f;     
    public float upSpeed = 2f;       
    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos - new Vector3(0, downDistance, 0);
        StartCoroutine(TrapRoutine());
    }

    IEnumerator TrapRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            // Hýzlýca aþaðý in (Smooth)
            while (Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, downSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPos;

            // Yavaþça yukarý çýk (Smooth)
            while (Vector3.Distance(transform.position, startPos) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, startPos, upSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = startPos;
        }
    }
}