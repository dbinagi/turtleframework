using UnityEngine;

namespace TurtleGames.Framework.Runtime.Utility
{
    public static class UtilityAnimation
    {
        public static string GetAnimatorName(Animator anim){
            AnimatorClipInfo[] animatorClipInfo = anim.GetCurrentAnimatorClipInfo(0);
            if(animatorClipInfo.Length > 0){
                return animatorClipInfo[0].clip.name;
            }
            return "";
        }
    }
}
