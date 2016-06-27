```csharp
//Matri4x4.identity

//1 0 0 x
//0 1 0 y
//0 0 1 z
//0 0 0 1  
        (@t 位置控制)

void Start()
{
    transform.Translate(Vector3.forward * 2.0f, Space.World);
    transform.Rotate(Vector3.up, 20.0f, Space.World);
    Matri4x4 mt = transform.wordToLocalMatrix;//

    // new identity
    float fCos = Mathf.Cos(20 * Mathf.Deg2Rad);
    float fSin = Mathf.Sin(20 * Mathf.Deg2Rad);
    //print fCos、fSin

    mt = Matri4x4.identity
    mt.SetTRS(new Vector3(-5, 10 , 0), Quaternion.AngleAxis(20.0f, Vector3.up), Vector3.one);

    // assert mt.m00 == fCos;
    // assert mt.m02 == fSin;

    transform.position = mt.MultiplyPoint(transform.position);
}
```