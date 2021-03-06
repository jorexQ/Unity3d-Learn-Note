#Unity3D运动基础对象

1. ## 坐标系系统：
  * **世界坐标系（绝对坐标系）：** 物体在当前世界中的位置；
  * **模型坐标系（相对坐标系）：** 子物体在父物体中的位置；
  * **摄像机坐标系：** 观察坐标系；
  * **视口坐标系：** 将3D坐标系转化成2D平面坐标系，让物体有视觉误差，看似3D物体；
  * **惯性坐标系：** 用来将相对坐标系转化成绝对坐标系；

  **Unity**引擎支持所有的坐标系；

2. ## 变换组件(`Transform`)属性：
  *  坐标系 `Position`；
  *  旋转角度（三个轴向的） `Rotation`；
  *  三个轴向的缩放比例 `Scale`；

  变换组件主要是用来操作物体在世界或者在父物体中的运动和朝向变换，变换组件的所有操作主要借助_向量_。<br/>

  **向量：**具有大小和方向的矢量，结构体 `Vector3` ，方便我们任意的设置数据，并且帮我们集合了向量的所有计算方法。<br/>

  `Vector3` 的重要属性：<br/>
    * 方向属性：`forwad`(前方), `back`(后方), `up`（上放）,`down`(下方), `left`(左方), `right`(右方);<br/>
    >__注意：__ 这些方向指的世界原点的方向，表示以世界坐标系作为参考坐标系来确定方向；

    * 计算属性：<br/>
      `x`, `y`, `z` 表示他的三个轴向的分量。
      * `normalized` : <br/>
        用来计算当前向量的标准化；
      * `magnituble` : <br/>
        求当前向量的取模 _（这是没有最后开方）_；
      * `sqrMagnituble` : <br/>
        表示开方了的模长，提供了向量的静态计算方法；
      * `Lerp(Vector3 from, Vector3 to, float t)` : <br/> 
        线性差值计算，平滑的计算规定时间里运动的距离，一般我们在`t`上传递是没帧的间隔时间； 
      * `Cross(Vector3 a, Vector3 b)` : <br/> 
        两个向量的差乘，求两个向量组成的面的垂直线；
      * `Angle(Vector3 a, Vector3 b)` : <br/> 
        用来求解两个向量所成夹角，结果一定是一个角度；
      * `Dot(Vector3 a, Vector3 b)` : <br/> 
        用来求向量的点乘，他和两个向量的角度配合起来求 两个物体的朝向关系；
      * `Reflect(Vector3 direction, Vector3 normal)` : <br/> 
        用来求当前向量的法向量的反射线的反射方向，用于光学计算；
      * `Distance(Vector3 a, Vector3 b)` : <br/> 
        用来求解两个向量之间的距离；
      * `SmoothDamp(Vector3 current, Vector3 target, float smoothTime, float maxSpeed, float deltaTime)` : <br/> 
        用来进行阻尼平滑运动，需要你指定`maxSpeed`（就是我们的初始化速度，`Mathf.Infinity`表示无限大）对于时间我们需要告诉他`deltaTime`（当前这帧用了多少时间）；

3. ## 操作`Transform`组件：
  * ###获取：<br/>
    * `gameObejct.GetComponent<Transform>()`；
    * `transfrom` 直接属性获取，当然必须在继承了`MonoBehaviour`之后；

  * ###特性：<br/>
    不能多次追加组件，只能有一个，这个组件不用你来添加，默认的任何游戏个体都会自动加入`Transfrom`组件。所以他也绝对不能删除，如果要删除那就请直接销毁游戏个体；

  * ###修改绝对坐标：<br/>
    对于`Transform`组件`Position`是他的属性，所以他一定会给我们提供这个属性的操作，那么我们可以直接通过`position`属性来获取或者更新你的世界坐标；
    >__注意：__ 因为`Vector3`是结构体，所以我们不能直接通过`get`方法来修改其内容，必须通过`set`设定新的结构体进入；

  * ###修改相对坐标：<br/>
    对于`Transform`组件提供了相对坐标的属性设定，`localPosition`他可以让当前物体以他的父对象作为参考坐标系，然后产生运动变化。<br/>
    `localPosition`提供了动态的添加绝对坐标系中的位置偏移，所以当前物体的父对象的位置偏移会自动追加进来，不用手动添加；

  * ###累计坐标变换函数：<br/>
    `Translate(Vector3 speed,  Space s)` , 我们给他提供指定方向上的运动速度和确定他的参考坐标系，然后产生坐标的累计增加；

  * ###修改当前物体的指定轴向的旋转角度：<br/>
    `rotation`属性提供了我们当前物体的旋转角度, 这个旋转角度主要是用四元数来表达</br>
    __四元数__（`Quaternion`） : 他是一个结构体，提供了 x, y, z, w四个元数，x, y, z表示三个轴的复数，w实数部分。<br/>
    __四元数的公式：__ `Q = cos(@/2) * 1, sin(@/2) * nx, sin(@/2) * ny, sin(@/2) * nz`;
    我们可以通过四元数计算的叉乘 *（叉乘我们的坐标）*产生所有模型点的位移；

    >__四元数 (`Quaterntion`)__ :<br/>
     里给我们提供了**欧拉角（）**，可同时旋转 一个以上的轴.<br/>
     但是需要注意,万向锁问题（角度出现了偏差）

    原始的四元数旋转 : 首先四元数的旋转是属于线性的，所以必须一次旋转一根轴。<br/>
    例:
          float fDeg = 20f * Mathf.Deg2Rad;
    >Mathf.Deg2Rad角度转弧度的常量

          float fCos = Mathf.Cos(fDeg / 2.0f);
          float fSin = Mathf.Sin(fDeg / 2.0f);

    旋转 x轴
          Quaternion qx = new Quaternion(1 * fSin, 0, 0, fCos);

    旋转 y轴
          Quaternion qy = new Quaternion(0, 1 * fSin, 0, fCos);

          transform.rotation = qx;
          transform.rotation = qy;

    使用欧拉角的旋转
          Quaternion q = Quaternion.Euler (20, 20, 20);
    表示我们同时让x，y，z旋转
          transform.rotation = q;
      
  * ###修改物体的相对坐标系旋转:
    `localRotation`属性就是相对坐标系的，他也会自动累加上父物体所成的角度，然后再旋转子物体的角度；

  * ###累加旋转：`Rotate(. . .)`:   
    两种大类：
    * 第一种使用欧拉角，我们只需要向其传递一个`Vector3`的向量，表示某个轴向上的角度；
    * 第二种使用指定轴然后指定旋转的角度，这个旋转用的是四元数旋转，所以不能同时旋转多个轴，一次旋转一个轴；
    使用`Rotate`会造成累加；

  * ###累加围绕指定的原点旋转:
    `RotateAround(Vector point, Vector axies, float angle, Space s)`;<br/>

    >__矩阵 (`Matrix4x4`)__:<br/>
    他是一个结构体，提供了4*4的方阵，一共16个元素，全部元素都是按照列来表示的，你可以人为的逻辑划分；
    通过矩阵我们可以带入矩阵公式：`X` `Y` `Z`轴向上的旋转，矩阵也是线性的变换，所以必须一步一步计算；<br/>
    矩阵提供了对`Vector3`向量的乘法计算 `MultiplyPoint(Vector3 pos)`:<br/>
    返回值是内部矩阵与向量左乘的结果；所以我们可以通过矩阵直接实现旋转后的坐标变换，当然他变换的时候是以世界原点为中心点旋转；
    还提供`SetTRS(Vector3 pos, Quaternion angle, Vector3 scale)`:<br/>
    快速的计算运动矩阵的累计矩阵结果然后去修改位置变化；
  
  * ###`Transform`组件可以给我们提供物体自身的方向:
    他与世界方向不一样，如果物体产生了角度变换，那么他的自身方向就一定与世界方向不同。所以我们在对物体的方向有要求的情况下一定要使用Transform组件给我们提供的方向;<br/>
    `transform.forward` : 当前物体的正前方<br/>
    `transform.right` : 当前物体的正右侧<br/>
    `transform.up` : : 当前物体的正上方<br/>
    其他三个方向：后、左、下，都可以对正方向进行求负，来得到；
  
  * ###`Transform`组件提供缩放功能：
    `localScale`，因为你自己的缩放据对不会影响到世界，所以你的缩放只有本地缩放。缩放的值一定是百分比，超过1表示放大，低于1表示缩小。他可以同时对三个轴上进行缩放











 









