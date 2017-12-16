using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
private bool isGround = false;

private readonly float gravityScale = 0.98f;
private readonly float moveSpeed = 7.5f * 2;
private readonly float jumpPower = 30.0f;


private Vector3 velocity = Vector3.zero;
private Vector3 direction = Vector3.forward;

private Vector3 xVector = Vector3.right;
private Vector3 yVector = Vector3.up;
private Vector3 zVector = Vector3.forward;

private new Rigidbody rigidbody;
private Transform mainCamera;




private enum State
{
Wait,
Run,
Jump,
};
private State state = State.Wait;

private enum Kick
{
None,
Stay,
Go,
};
private Kick kick = Kick.None;





// 初期化
private void Start ()
{
rigidbody = GetComponent<Rigidbody>();
rigidbody.useGravity = false;

mainCamera = Camera.main.transform;
}

// 更新
private void Update ()
{
float delta = 1.0f / Time.deltaTime * Time.deltaTime;
float speed = moveSpeed * delta;

// カメラ更新
UpdateCamera();

// 速度リセット
velocity = Vector3.zero;

// 移動値の削除
rigidbody.velocity -= transform.forward * Vector3.Dot(transform.forward, rigidbody.velocity);
rigidbody.velocity -= transform.right * Vector3.Dot(transform.right, rigidbody.velocity);


// 入力移動
#if false
if (Input.GetKey(KeyCode.D)) { velocity.x += speed; }
if (Input.GetKey(KeyCode.A)) { velocity.x -= speed; }
if (Input.GetKey(KeyCode.W)) { velocity.z += speed; }
if (Input.GetKey(KeyCode.S)) { velocity.z -= speed; }
#else
velocity.x = Input.GetAxis("Horizontal") * speed;
velocity.z = Input.GetAxis("Vertical") * speed;
#endif

// 回転設定
if (velocity.x != 0.0f || velocity.z != 0.0f)
{
direction = velocity;
}
xVector = (Vector3.Cross(yVector, mainCamera.forward)).normalized;
zVector = (Vector3.Cross(xVector, yVector)).normalized;
transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(xVector * direction.x + zVector * direction.z, yVector), 0.2f);




// 重力
velocity.y = -gravityScale;

// 速度反映
rigidbody.velocity += xVector * velocity.x + yVector * velocity.y + zVector * velocity.z;


if (isGround)
{
// 重力を削除
rigidbody.velocity -= yVector * Vector3.Dot(transform.up, rigidbody.velocity);
}


// ジャンプ
if (Input.GetButtonDown("Jump") && isGround)
{
isGround = false;

// ジャンプ
rigidbody.velocity += yVector * jumpPower * delta;
}



// キックモード
switch (kick)
{
case Kick.None:
xzDistance = -6.0f;
if (Input.GetButtonDown("Kick"))
{
isGround = false;
kick = Kick.Stay;
}
break;

case Kick.Stay:
xzDistance = -10.0f;
rigidbody.velocity = Vector3.zero;
//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.forward, (targetPosition - mainCamera.position).normalized), 0.1f);

if (Input.GetButtonDown("Kick"))
{
isGround = false;
kick = Kick.Go;

// 重力変更
yVector = -(targetPosition - mainCamera.position).normalized;
}
break;

case Kick.Go:
if (Input.GetButtonDown("Kick"))
{
isGround = false;
kick = Kick.Stay;
}
break;
}



// 状態遷移
Animator animator = GetComponent<Animator>();
state = State.Wait;
if (velocity.x != 0 || velocity.z != 0)
{
state = State.Run;
}
if (!isGround)
{
state = State.Jump;
animator.Play("Jump");
}
// アニメーションの適応
animator.SetInteger("State", (int)state);

animator.speed = 1;
var info = animator.GetCurrentAnimatorStateInfo(0);
switch (state)
{
case State.Wait:
break;
case State.Run:
break;
case State.Jump:
//if (info.normalizedTime < 0.30f)
if (info.normalizedTime < 0.50f)
{
animator.speed = 5.0f;
}
else
{
animator.speed = 0;
}
break;
}
animator.Update(Time.deltaTime);
}



private bool isQuake = false;
private float quakeTimer = 0;
private float xzRadian = 0;
private float xzNextRadian = 0;
private float yzRadian = 0;
private float yzNextRadian = 0;
private float rotateSpeed = 0.025f;

private float xzDistance = -6.0f;
private float yDistance = 2.25f;

private Vector3 targetPosition = Vector3.zero;
private float targetPosDistMin = 0.25f;
private float targetPosDistMax = 0.5f;

private Quaternion baseRot = Quaternion.identity;


// カメラ更新
private void UpdateCamera()
{
// 注視点更新
targetPosition = transform.position + transform.up * 2;


// 方向転換
#if false
if (Input.GetKey(KeyCode.RightArrow)) { xzNextRadian += Mathf.PI * rotateSpeed; }
if (Input.GetKey(KeyCode.LeftArrow)) { xzNextRadian -= Mathf.PI * rotateSpeed; }
#else
float axisX = Input.GetAxis("Horizontal2");
if (Mathf.Abs(axisX) > 0.1f) { xzNextRadian += axisX * Mathf.PI * rotateSpeed * Mathf.Rad2Deg; }
float axisY = Input.GetAxis("Vertical2");
if (Mathf.Abs(axisY) > 0.1f) { yzNextRadian += axisY * Mathf.PI * rotateSpeed * Mathf.Rad2Deg; }
#endif

// 角度制限
yzNextRadian = Mathf.Max(!isGround ? -60 : -10.0f, Mathf.Min(yzNextRadian, 60.0f));

// 角度補間
xzRadian = Mathf.Lerp(xzRadian, xzNextRadian, 0.1f);
yzRadian = Mathf.Lerp(yzRadian, yzNextRadian, 0.1f);

#if false
Quaternion upQuat = Quaternion.FromToRotation(Vector3.up, yVector);
Vector3 pos = transform.position + upQuat * new Vector3(Mathf.Sin(xzRadian) * xzDistance, yDistance, Mathf.Cos(xzRadian) * xzDistance);
#else
//Vector3 y = (mainCamera.position - transform.position).normalized;
//Vector3 x = Vector3.Cross(y, (targetPosition - mainCamera.position).normalized);
//Vector3 z = Vector3.Cross(x, y);

//float speed = 3;
//Vector3 d = new Vector3(Input.GetAxis("Horizontal2") * -speed, Input.GetAxis("Vertical2") * speed, 0);

//Quaternion rotToAxisY = Quaternion.FromToRotation(Vector3.up, y);
//Quaternion rotToAxisXZ = Quaternion.FromToRotation(rotToAxisY * Vector3.forward, z * d.y + x * d.x);
//Quaternion rot = rotToAxisXZ * rotToAxisY;

//baseRot = (Quaternion.AngleAxis(d.y, x) * Quaternion.AngleAxis(-d.x, z)) * baseRot;
//Vector3 pos = transform.position + baseRot * Vector3.up * xzDistance;


Vector3 pos = transform.position
+ Quaternion.AngleAxis(yzRadian, xVector)
* Quaternion.AngleAxis(xzRadian, yVector)
* Vector3.forward * xzDistance
+ yVector * 2;
#endif

// 座標更新
mainCamera.position = Vector3.Lerp(mainCamera.position, pos, 0.2f);



//// 距離補正
//if (Vector3.Distance(targetPosition, mainCamera.position) < xzDistance * 2)
//{
// mainCamera.position = targetPosition + (targetPosition - mainCamera.position).normalized * xzDistance * 2;
//}


//float dist = Vector3.Distance(transform.position, targetPosition);
//if (dist < targetPosDistMin)
//{
// targetPosition = transform.position + (targetPosition - transform.position).normalized * targetPosDistMin;
//}
//else if (dist > targetPosDistMax)
//{
// targetPosition = transform.position + (targetPosition - transform.position).normalized * targetPosDistMax;
//}


// 振動
if (isQuake)
{
float range = 0.25f;
mainCamera.position += new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
targetPosition += new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
quakeTimer += Time.deltaTime * 60;
if (quakeTimer > 5)
{
isQuake = false;
quakeTimer = 0;
}
}

// 角度更新
Vector3 vec = (targetPosition - mainCamera.position).normalized;
mainCamera.rotation = Quaternion.Slerp(mainCamera.rotation, Quaternion.LookRotation(vec, transform.up), 1f);
}





// ヒット関係
private void OnCollisionEnter(Collision collision)
{
if (Vector3.Dot(transform.up, rigidbody.velocity) < -10)
{
isQuake = true;
}

isGround = true;
kick = Kick.None;

// 法線設定
yVector = collision.contacts[0].normal;

// 再計算
xVector = (Vector3.Cross(yVector, mainCamera.forward)).normalized;
zVector = (Vector3.Cross(xVector, yVector)).normalized;

#if true
// 法線を上として立たせる
transform.rotation = Quaternion.LookRotation(zVector * direction.z + xVector * direction.x, yVector);
#endif

// 速度のリセット
rigidbody.velocity = Vector3.zero;

//System.Func<Vector3, Vector3, float> getAngle = (from, to) => { return Mathf.Atan2(to.x - from.x, to.z - from.z) * Mathf.Rad2Deg; };
//transform.rotation = Quaternion.AngleAxis(getAngle(Vector3.forward, direction), yVector) * Quaternion.FromToRotation(Vector3.up, yVector);
}
private void OnCollisionStay(Collision collision)
{
}
private void OnCollisionExit(Collision collision)
{
isGround = false;
}






// デバッグ描画
void OnDrawGizmos()
{

Gizmos.color = Color.green;
Gizmos.DrawRay(transform.position, xVector * 10);

Gizmos.color = Color.blue;
Gizmos.DrawRay(transform.position, zVector * 10);

Gizmos.color = Color.red;
Gizmos.DrawRay(transform.position, yVector * 10);
}
}