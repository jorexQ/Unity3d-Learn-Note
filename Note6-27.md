回顾：
===============================
```C-sharp
GameObject.Find()
```
### 通过游戏对象名称查找 GameObject（不推荐使用，性能太低）
>在同一层级有多个重名游戏对象时查找的对象无明细规律

今天：
===============================

运动：
-------------------------------
	transform.GetChild(index)
### index依照的是编辑器里的游戏对象的顺序

	transform.localPosition
### 相当父级游戏对象的坐标系

	transform.parent
### 获取父级的transform对象
>当游戏对象transform.parent为空时，
>
>就表明这个游戏对象是根对象	

	transform.Translate()
### 可以改变朝向的运动函数

	Vector3.Lerp(fromPosition,toPosition, usedTime)
### 平滑运动<br/>
>（默认）会用动线性差值的阻尼运动

旋转：
------------------------------
>旋转方向依照的是摄像机坐标系

	Mathf.Deg2Rad
### 单位角弧度

	Quaternion 
### 四源数
* 线性旋转（一次只旋转一个方向）.
* 多轴旋转一定会有角度余值（万象锁）.
* 叠加旋转是没问题的.


	Quaternion.Euler()
### 获取欧拉角
>没有固定的算法<br/>
>在特殊角度时也会有角度余值（万象锁）

	Matri4x4
### 4x4矩阵
>优点：可以作偏移<br/>
> m12<br/>
>第一个数：行<br/>
>第二个数：列<br/>

	Matri4x4.identity
### 单位矩阵

	
	
	
	
	
	

	




	

	


	
	
