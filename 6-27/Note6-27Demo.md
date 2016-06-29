```csharp
//Matri4x4 

//1 0 0 x
//0 1 0 y
//0 0 1 z
//0 0 0 1  
//      (@t 位置控制)

void Start()
{
    transform.Translate(Vector3.forward * 2.0f, Space.World);
    transform.Rotate(Vector3.up, 20.0f, Space.World);
    //transform.wordToLocalMatrix 获取本地矩阵
    //transform.LocalTowordMatrix 获取世界矩阵
    Matri4x4 currentMt = transform.wordToLocalMatrix;//

    // new identity
    Matri4x4 newMt = Matri4x4.identity
    newMt.SetTRS(new Vector3(0, 0 , 2), Quaternion.AngleAxis(20.0f, Vector3.up), Vector3.one);
   
    float fCos = Mathf.Cos(20 * Mathf.Deg2Rad);
    float fSin = Mathf.Sin(20 * Mathf.Deg2Rad);
    //print fCos、fSin
    Vector3 newPoint = newMt.MultiplyPoint(transform.position);
  
    // assert currentMt == newMt;
    // assert mt.m00 == fCos;
    // assert mt.m02 == fSin;
    // assert newPoint ==transform.position;

    ///修正渲染旋转
    MeshFilter pFilter = gameObject.GetComponent<MeshFilter>();
    Mesh pMesh = pFilter.mesh;
    Vector3[] ps = pMesh.vertices;
    for(int i =0; i< ps.length; ++i)
    {
        p[i] = newMt.MultiplyPoint(p[i]);
    }
    pMesh.vertices = ps;
}
```