using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;//获得角色
    public Vector2 Margin;//相机与角色的相对范围
    public Vector2 smoothing;//相机移动的平滑度
    public BoxCollider2D Bounds;//背景的边界
 
    private Vector3 _min;//边界最大值
    private Vector3 _max;//边界最小值
 
    public bool IsFollowing;//用来判断是否跟随
 
    void Start(){
        UpdateBounds();
    }
 
    void Update(){
        if (player == null) {
            return;
        }
        var x = transform.position.x;
        var y = transform.position.y;
        if (IsFollowing) {
            x = player.transform.position.x;
            y = player.transform.position.y;
        }
        float orthographicSize = GetComponent<Camera>().orthographicSize;//orthographicSize代表相机(或者称为游戏视窗)竖直方向一半的范围大小,且不随屏幕分辨率变化(水平方向会变)
        var cameraHalfWidth = orthographicSize * ((float)Screen.width / Screen.height);//的到视窗水平方向一半的大小
        x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x-cameraHalfWidth);//限定x值
        y = Mathf.Clamp (y, _min.y + orthographicSize, _max.y-orthographicSize);//限定y值
        transform.position = new Vector3(x, y, transform.position.z);//改变相机的位置
    }

    public void UpdateBounds() {
        _min = Bounds.bounds.min;//初始化边界最小值(边界左下角)
        _max = Bounds.bounds.max;//初始化边界最大值(边界右上角)
        IsFollowing = true;//默认为跟随
    }
}
