回顾：
===============================
* **`GameObject.Find()`** : <br/>
  通过游戏对象名称查找 `GameObject`
  >不推荐使用，性能太低<br/>
  >在同一层级有多个重名游戏对象时查找的对象无明细规律

今天：
===============================

1. 常用的运动方式：

  * **`transform.GetChild(index)`** : <br/>
    index依照的是编辑器里的游戏对象的顺序.

  * **`transform.localPosition`** : <br/>
    相当父级游戏对象的坐标系.

  * **`transform.parent`** : <br/>
    获取父级的`transform`对象
    >当游戏对象`transform.parent`为空时，就表明这个游戏对象是根对象	

  * **`transform.Translate()`** : <br/>
    可以改变朝向的运动函数

  * **`Vector3.Lerp(fromPosition,toPosition, usedTime)`** : <br/>
    平滑运动
    >（默认）会用动线性差值的阻尼运动


2. 常用的旋转方式：

  >__注意：__ 旋转方向依照的是摄像机坐标系

  * **`Mathf.Deg2Rad`** : <br/>
    单位角弧度
    > (PI * 2)/360

  * **`Quaterniond`** : <br/>
    四源数
    * 线性旋转（一次只旋转一个方向）
    * 多轴旋转一定会有角度余值（万象锁）
    * 叠加旋转是没问题的

  * **`Quaternion.Euler()`** : <br/>
    获取欧拉角
    >没有固定的算法<br/>
    >在特殊角度时也会有角度余值（万象锁）

  * **`Matri4x4`** : <br/>
    4x4矩阵
    >也是线性变换<br/>
    >优点：可以作偏移<br/>
    > m12<br/>
    >第一个数：行<br/>
    >第二个数：列<br/>

  * **`Matri4x4.identity`** : <br/>
    单位矩阵

  * 获取自己的在世界坐标系的方向 <br/>
    ```csharp
    Vector3 localDirection;
    localDirection = transform.forward
    // or
    Quaternion q = transform.rotation;
    localDirection = q * Vector3.forword;
    ```






	
	
	
	
	
	

	




	

	


	
	
