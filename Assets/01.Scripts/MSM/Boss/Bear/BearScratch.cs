using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearScratch : MonoBehaviour
{
    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(3f);
    }
}
