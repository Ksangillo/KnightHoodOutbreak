using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
internal class StringAnimations
{
    //seperate place to store the animation parameters

    //Bools
    internal static string isMove = "isMove";
    internal static string isRunning = "isRunning";
    internal static string isGroundCheck = "isGroundCheck";
    internal static string isWallCheck = "isWallCheck";
    internal static string isCeilingCheck = "isCeilingCheck";
    internal static string canMove = "canMove";
    internal static string isTarget = "isTarget";
    internal static string isAlive = "isAlive";
    internal static string isHit = "isHit";
    internal static string isAttack = "isAttack";
    //Trigger
    internal static string jump = "jump";
    internal static string attack = "attack";
    internal static string hitTrigger = "hit";
    internal static string isDash = "isDash";
    //Floats
    internal static string yVelocity = "yVelocity";
    internal static string lockVelocity = "lockVelocity";
    internal static string attackCoolDown = "attackCoolDown";
}
