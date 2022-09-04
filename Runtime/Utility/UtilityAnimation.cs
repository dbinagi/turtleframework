using UnityEngine;

namespace TurtleGames.Framework.Runtime.Utility
{
    public static class UtilityAnimation
    {
        string GetAnimatorName(){
            AnimatorClipInfo[] animatorClipInfo = anim.GetCurrentAnimatorClipInfo(0);
            if(animatorClipInfo.Length > 0){
                return animatorClipInfo[0].clip.name;
            }
            return "";
        }
    }
}
