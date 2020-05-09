using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GameFramework
{
    public class UIUtils
    {
        public static async Task ObjectAnim(GameObject target, EUIAnim anim,float time=0.5f)
        {
            if (anim == EUIAnim.None || target == null) return;
            //UI淡入淡出效果
            if (anim == EUIAnim.FadeIn || anim == EUIAnim.FadeOut)
            {
                Graphic[] comps = target.GetComponentsInChildren<Graphic>();
                for (int i = comps.Length; --i >= 0;)
                {
                    if (anim == EUIAnim.FadeIn)
                        comps[i].DOFade(0, time).From();
                    else
                        comps[i].DOFade(0, time);
                }
                await new WaitForSeconds(time);
            }
            else if (anim == EUIAnim.ScaleIn || anim == EUIAnim.ScaleOut)
            {
                if (anim == EUIAnim.ScaleIn)
                {
                    target.transform.DOScale(0, time).SetEase(Ease.OutBack).From();
                    await new WaitForSeconds(time);
                }
                else
                {
                    target.transform.DOScale(0, time).SetEase(Ease.InBack);
                    await new WaitForSeconds(time);
                }
            }
        }

        /// <summary>
        /// 刷新列表元素
        /// </summary>
        public static void RefreshList<T, T1>(List<T> ItemList, List<T1> Datas, Action<T, T1> RefreshItem,
            Action<T1> CreateItem) where T : BaseItem
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                if (i < Datas.Count)
                {
                    RefreshItem?.Invoke(ItemList[i], Datas[i]); //刷新
                    ItemList[i].gameObject.SetActive(true);
                }
                else
                {
                    ItemList[i].gameObject.SetActive(false); //隐藏多余的
                }
            }

            //Item比Datas小
            if (ItemList.Count < Datas.Count)
            {
                for (int i = ItemList.Count; i < Datas.Count; i++)
                {
                    CreateItem?.Invoke(Datas[i]); //创建
                }
            }
        }
    }
}
