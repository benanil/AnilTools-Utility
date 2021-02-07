# AnilTools-Utility                                                                                     
this utils developed 4 months, it has a lot of helper functions witch can help you a lot                                  
if you want async tasks you need to download unitask via (Not Required)[GitHub Page](https://github.com/Cysharp/UniTask#install-via-git-url)                    
Thanks to @beinteractive for UrFairy [GitHub Page](https://github.com/beinteractive/UrFairy) not: if you saw issues just delete them they are probably unitask codes           

### Movement
> it allows you slowly translate between a to b with 1 line of code
```C#
transform.Move(position,speed);
transform.rotate(EulerAngleS, rotateSpeed);
transform.Translate(position, rotation , speed);
transform.MoveX(5);

// move a to b then move joined positions
transform.Move(position, speed).Join(vector3.right).JoinRotation(eulerAngle);
transform.Translate(vector3.up, speed).Join(Position);
transform.MoveX(5).Join(10, MoveDirection.Y);

var task = transform.Move(position,speed);
task.Join(Vector3.right * 5);
task.AddFinishEvent(() => Debug.Log("movement finish"));
```
### Basic Singleton
> create Singleton very fast
```C#
// write your own monobehaviour's name instead of GameMenu
// you can reach this by type GameMenu.instance
public class GameMenu : Singleton<GameMenu>
```
### Save Load
> save whatever you want classes structs scriptableobjects with json
```C#
// sync
JsonManager.Save<YourClass>(YourObj, Path , Name);
JsonManager.Load<YourClass>(Path, Name);
// async
JsonManager.SaveAsync<YourClass>(YourObj, Path , Name);
JsonManager.LoadAsync<YourClass>(Path, Name);
```
### Update
> Update While condition True
```C#
RegisterUpdate.UpdateWhile(
action: delegate
{
    headTime -= Time.deltaTime;
    if (headTime < 0){
	headTime = HeadShakeTime;
        animator.SetTrigger(HeadShake);
    }
},
endCondition: delegate
{
    return _horseState == HorseState.wondering;
});
```
> WaitUntil condition true and then do action
```C#
bool check;
public void Start()
{
    RegisterUpdate.WaitUntil(() => check == true, () => Debug.Log("checked"));
}
```
### Painting
> you can paint meshrenderer and skined mesh renderer
#### add AnilMaterial to your gameObject
#### and add a raycastPainter anywhere on hierarchy 
```C#
// or manualy
if (Mathmatic.RaycastFromCamera(out RaycastHit hit)
Painter.PaitCircale(hit, radius, color);
```
### Text Animations
> animate texts
```C#
StartCoroutine(Text.TextLoadingAnim("yourText"));
StartCoroutine(Text.TextFadeAnim("yourText"));
StartCoroutine(Text.TextDisolveAnim("yourText"));
```
### Colorful Debug

```C#
Debug2.Log("debug text",Color.cyan);
```

