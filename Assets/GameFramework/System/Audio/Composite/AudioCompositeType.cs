using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GameFramework
{
    public enum AudioCompositeType
    {
        //并列执行:所有action一起进行，直到单个action完成操作，就销毁此action
        Parallel = 1,
        //顺序执行:按顺序一个个执行，前一个执行完在执行后面一个<不可有循环播放音效>
        Sequence = 2,
        //单个执行:单个执行，只播放一个音效，如果增加一个，则顶掉前一个，播放添加的音效
        Single = 3,
    }
}
