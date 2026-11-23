using UnityEngine;

public static class MovementHelpers
{
    public static EDirection ToDirectionSideScroll(this Vector2 vec)
    {
        if (vec == Vector2.zero)
            return EDirection.Right;
        
        return vec.x > 0 ? EDirection.Right : EDirection.Left; 
    }
    
    public static EDirection ToDirectionTopDown(this Vector2 vec)
    {
        if (vec == Vector2.zero)
            return EDirection.Down;

        if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
            return vec.x > 0 ? EDirection.Right : EDirection.Left;
        return vec.y > 0 ? EDirection.Up : EDirection.Down;
    }

    public static float GetSpeed(bool isWalking, bool isRunning, bool isCrouching, float walkSpeed,
        float runMultiplier, float crouchMultiplier)
    {
        if (isWalking)
        {
            if (isCrouching)
            {
                if(!isRunning) { return walkSpeed * crouchMultiplier; }
                return walkSpeed * runMultiplier * crouchMultiplier; 
            }
            if(!isRunning) { return walkSpeed; }
            return walkSpeed * runMultiplier; 
        }
        return 0f;
    }
}
public enum EAreaType
{
    TopDown,
    SideScroller
}


public enum EDirection
{
    Down,   //0
    Up,     //1
    Right,  //2
    Left    //3
}

// public enum ESideScrollerDirection
// {
//     Right,
//     Left
// }