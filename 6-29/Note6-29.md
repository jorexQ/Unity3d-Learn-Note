# Unity3D数学
* ## 三角函数
  * 求正函数，参数弧度，返回也是弧度
    ```csharp
    float Mathf.Cos(float f);
    float Mathf.Sin(float f);
    float Mathf.Tan(float f);
    ```
  * 求反函数
    ```csharp
    float Mathf.Acos(float f);
    float Mathf.Asin(float f);
    float Mathf.Atan(float f);
    float Mathf.Atan2(float y, float x); 
    ```
    >`Atan2` : 这个是针对直角三角形，y表示角的对边，x临边: 通过y / x得到相应的弧度来计算当前角的弧度
  * 角度转弧度  `Mathf.Deg2Rad`
  >(PI  * 2) / 360

  * 弧度转角度  `Mathf.Rad2Deg`
   > 360 / (PI * 2)

  * 求增量角的函数  `float Mathf.DeltaAngle(float current, float target)`
  > 求到目标角度你当前角度的差异
* ## 数字处理
  * 求趋近整数: `int Mathf.CeilToInt(float f)`;
    >当小数部分不为0的时候会自动向整数部分 ＋ 1，然后舍去小数部分
  * 求趋近整数: `float Mathf.Ceil(float f)`
    >和上面的函数一样，只是直接返回的是单精度浮点

  * 求四舍无入：`int Mathf.RoundToInt(float f)`
  * 求四舍五入: `float Mathf.Round(float f)`

  * 通过指定数的区间，固定传入数的范围：`float Mathf.Clamp (float value, float min, float max)`

  * 固定数在 0 到 1之间: `float Mathf.Clamp01 (float value)`

  * 对于数字的线性差值计算： `float Mathf.Lerp(float form, float to, float t)`

  * 到达指定数之后重复取值 : `float Mathf.Repeat(float current, float length)`
  >当current到达length后，再次返回0然后重新计数

* ## 普通数学计算
  * 次方数: `float Mathf.Pow(float x, float y)`
  >计算 x 的 y 次方
  * 开方: `float Mathf.Sqrt(float x)`
  * 求幂: `float Mathf.Exp(float x)`
  * 求指数: `float Mathf.Log(float a, float x)`
  >自己指定底数
  * 求已10为底的指数: `float Mathf.Log10(float x)`
  * 求绝对值：`float Mathf.Abs(float x)`
* ## 对于浮点数的认知
  浮点数绝对不要使用恒等的方式来判断，会出现永远无法到达的问题，所以我们想比较浮点数，一定使用区间比较方法<br/>
  无限小的浮点数：`Mathf.Epsilson` <br/>
  无穷数：`Mathf.Infinity` <br/>







































