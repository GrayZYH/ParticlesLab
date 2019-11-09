using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameGenarater : MonoBehaviour
{
    public static NameGenarater Instance;

    public RenderTexture ScreenShortRT;

    public GameObject NameGroupPrefabs;
    public GameObject Canvas;

    public static int Index;

    public float PlayBigdustAnimTime;

    public float PointDistance;

    public float[] X1;
    public float[] Y1;

    public float[] X2;
    public float[] Y2;

    public float[] X3;
    public float[] Y3;

    //public Transform[] PointPos;
    public List<Transform> RandomRangePoints;
    public static List<GameObject> AllNames =new List<GameObject>();


    private RawImage _nameTexture;
    private Image _nameStarImage;
    private ParticleSystem _nameParticleSys;
    private Vector3[] _effectPos;


    //public List<GameObject> _nameObjs1 = new List<GameObject>();
    //public List<GameObject> _nameObjs2 = new List<GameObject>();
    //public List<GameObject> _nameObjs3 = new List<GameObject>();

    private int index;

    private void Start()
    {
        Instance = this;
        ScreenShortManager.Instance.ScreenShortPhotoEvent += ScreenShortPhotoCallBack;
    }

    private void OnDestroy()
    {
        ScreenShortManager.Instance.ScreenShortPhotoEvent -= ScreenShortPhotoCallBack;
    }

    private void ScreenShortPhotoCallBack(Texture2D photo)
    {
        if (AllNames.Count >= RandomRangePoints.Count)
        {
            return;
        }
        ///保存截图
        SvaeToLocalManager.Instance.SaveLocalPhoto(photo);



        GameObject nameEffectPrefabs = Instantiate(NameGroupPrefabs, RandomRangePoints[GetPosIndex()].transform,false) as GameObject;
        AllNames.Add(nameEffectPrefabs);

        ///姓名图片
        _nameTexture = nameEffectPrefabs.transform.Find("NameTexture").GetComponent<RawImage>();
        _nameTexture.texture = photo;

        ///限制三种粒子随机生成位置
        //_effectPos = new Vector3[6];
        //_effectPos[0] = new Vector3(Random.Range(X1[0], X1[1]), Random.Range(Y1[0], Y1[1]), 0f);
        //_effectPos[1] = new Vector3(Random.Range(X1[0], X1[1]), Random.Range(Y1[0], Y1[1]), 0f);
        //_effectPos[2] = new Vector3(Random.Range(X1[0], X1[1]), Random.Range(Y1[0], Y1[1]), 0f);
        //_effectPos[3] = new Vector3(Random.Range(X1[0], X1[1]), Random.Range(Y1[0], Y1[1]), 0f);
        //_effectPos[4] = new Vector3(Random.Range(X2[0], X2[1]), Random.Range(Y2[0], Y2[1]), 0f);
        //_effectPos[5] = new Vector3(Random.Range(X3[0], X3[1]), Random.Range(Y3[0], Y3[1]), 0f);
        //nameEffectPrefabs.transform.localPosition = _effectPos[Random.Range(0, 6)];

        ///姓名图片动画
        Animator NameTextureAnim = _nameTexture.GetComponent<Animator>();
        NameTextureAnim.enabled = true;
        NameTextureAnim.Play("NameTextureAnim", 0, 0.0f);
        NameTextureAnim.Update(0.0f);

        ///星星动画
        _nameStarImage = nameEffectPrefabs.transform.Find("NameStar").GetComponent<Image>();
        Animator NameStarAnim = _nameStarImage.GetComponent<Animator>();
        NameStarAnim.enabled = true;
        NameStarAnim.Play("NameStarAnim", 0, 0.0f);
        NameStarAnim.Update(0.0f);

        ///姓名粒子效果
        _nameParticleSys = _nameStarImage.transform.Find("NameEffect").GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shape = _nameParticleSys.shape;
        shape.texture = photo;

        Animator BigdustAnim = nameEffectPrefabs.transform.Find("Bigdust").GetComponent<Animator>();
        StartCoroutine(PlayBigdustAnim(BigdustAnim));

        //ScreenShortRT.Release();

        ///清除未使用的Assets
        Resources.UnloadUnusedAssets();
    }

    private IEnumerator PlayBigdustAnim(Animator bigdustAnim)
    {
        yield return new WaitForSeconds(PlayBigdustAnimTime);
        bigdustAnim.enabled = true;
        bigdustAnim.Play("BigdustAnim", 0, 0.0f);
        bigdustAnim.Update(0.0f);
    }

    public int GetPosIndex()
    {
        //int randomIndex = Random.Range(0, RandomRangePoints.Count);
        //if (RandomRangePoints[randomIndex].GetComponent<GetPos>().Ispos)
        //{
        //    GetPosIndex();
        //}
        //index = randomIndex;
        //Debug.LogError(randomIndex);
        //RandomRangePoints[randomIndex].GetComponent<GetPos>().Ispos = true;
        //return randomIndex;
        int randomIndex;
        do
        {
             randomIndex = Random.Range(0, RandomRangePoints.Count);
        } while (RandomRangePoints[randomIndex].GetComponent<GetPos>().Ispos);
        Debug.Log(randomIndex);
        RandomRangePoints[randomIndex].GetComponent<GetPos>().Ispos = true;
        return randomIndex;
    }

    #region 生成物体递归

    //public GameObject RandomRangePoint()
    //{
    //    GameObject obj =null;
    //    Index =Random.Range(0, 3);
    //    switch (Index)
    //    {
    //        case 0:
    //            obj = Instantiate(NameGroupPrefabs, Canvas.transform, false);
    //            _nameObjs1.Add(obj);
    //            obj.transform.localPosition = GetVec3(_nameObjs1);
    //            obj.transform.localScale = Vector3.one;
    //            obj.GetComponent<PrefabsController>().index = 0;
    //            break;
    //        case 1:
    //            obj = Instantiate(NameGroupPrefabs, Canvas.transform, false);
    //            _nameObjs1.Add(obj);
    //            obj.transform.localPosition = GetVec3(_nameObjs2);
    //            obj.transform.localScale = Vector3.one;
    //            obj.GetComponent<PrefabsController>().index = 1;
    //            break;
    //        case 2:
    //            obj = Instantiate(NameGroupPrefabs, Canvas.transform, false);
    //            _nameObjs2.Add(obj);
    //            obj.transform.localPosition = GetVec3(_nameObjs3);
    //            obj.transform.localScale = Vector3.one;
    //            obj.GetComponent<PrefabsController>().index = 2;
    //            break;
    //        default:
    //            break;
    //    }
    //    return obj;
    //}

    //public Vector3 RandomRangePos(int index)
    //{
    //    Vector3 pos = Vector3.zero;
    //    pos = new Vector3(Random.Range(PointPos[index*2].localPosition.x, PointPos[2*index+1].localPosition.x), Random.Range(PointPos[index * 2].localPosition.y, PointPos[index * 2+1].localPosition.y), 0);
    //    return pos;
    //}

    //public Vector3 GetVec3(List<GameObject> nameList)
    //{
    //    Vector3 pos = RandomRangePos(Index);
    //    if (nameList.Count > 0)
    //    {
    //        do
    //        {
    //            pos = RandomRangePos(Index);
    //        } while (IsPos(pos,nameList));
    //    }
    //    return pos;
    //}

    //private bool IsPos(Vector3 next, List<GameObject> p)
    //{
    //    if (p.Count > 0)
    //    {
    //        for (int i = 0; i < p.Count; i++)
    //        {
    //            if (p[i] != null)
    //            {
    //                if (Vector3.Distance(next,p[i].transform.localPosition) > -PointDistance || Vector3.Distance(next, p[i].transform.localPosition) < PointDistance)
    //                {
    //                    return true;
    //                }
    //            }
    //        }
    //    }
    //    return false;
    //}

    #endregion
}
