using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    [Header("UI Refereces")]
    public Image iconImage;
    public Text nameText;
    public Text descriptionText;
    public Text progressText;
    public Slider progressSlider;

    public void SetAchievement(AchievementData achievement, float progress)
    {
        if(nameText != null)
            nameText.text = achievement.name;

        if (descriptionText != null)
            descriptionText.text = achievement.description;

        //아이콘 설정
        if (iconImage != null && achievement.icon != null)
            iconImage.sprite = achievement.icon;

        //진행도 표시
        if(progressSlider != null)
            progressSlider.value = achievement.isUnlocked ? 1 : progress;

        if(progressText != null)
        {
            if (achievement.isUnlocked)
                progressText.text = "완료!";
            else
            {
                int current = Mathf.FloorToInt(progress * achievement.requiredAmount);
                progressText.text = current + "/" + achievement.requiredAmount;
            }
        }
    }
}
