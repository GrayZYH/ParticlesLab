using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsController : MonoBehaviour
{
    public float DestroyTimes;
    public int index;
    private void Start()
    {
        Invoke("DestroyFuc", DestroyTimes);
    }

    private void DestroyFuc()
    {
        Destroy(this.gameObject);
        ResetParent();
        NameGenarater.AllNames.Remove(this.gameObject);
    }


    //public void ClearList()
    //{
    //    switch (index)
    //    {
    //        case 0:
    //            NameGenarater.Instance._nameObjs1.Remove(this.gameObject);
    //            break;
    //        case 1:
    //            NameGenarater.Instance._nameObjs2.Remove(this.gameObject);
    //            break;
    //        case 2:
    //            NameGenarater.Instance._nameObjs3.Remove(this.gameObject);
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //IEnumerator Des()
    //{
    //    yield return new  WaitForSeconds(0.5f);
    //    ClearList();
    //}

    public void ResetParent()
    {
        this.transform.parent.GetComponent<GetPos>().Ispos = false;
    }
}
