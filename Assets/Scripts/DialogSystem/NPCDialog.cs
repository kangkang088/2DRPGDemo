using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour {
    private List<DialogInfo[]> dialogInfoList;
    public int contentIndex;//每一段中的每一条对话的索引
    public Animator animator;
    
    private void Start() {
        dialogInfoList = new List<DialogInfo[]>() {
            //0 dialogInfoIndex
            new DialogInfo[] {
                new DialogInfo() { name = "Luna",content = "Hi~ o(*￣▽￣*)ブ，我是Luna！主人，您可以用WASD控制我的移动。空格键和其他角色\r\n对话。战斗的话Luna不是很擅长，但是！您可以通过鼠标点击操纵Luna战斗。"}
            },
            //1 dialogInfoIndex
            new DialogInfo[]{
                //0 contentIndex
                new DialogInfo() { name = "Nala",content = "好久不见！小猫咪o(*￣▽￣*)ブ，Luna~~"},
                //1 contentIndex
                new DialogInfo() { name = "Luna",content = "好久不见！Nala，你还是这么有活力呀！嘿嘿！"},
                new DialogInfo() { name = "Nala",content = "还好吧~~"},
                new DialogInfo() { name = "Nala",content = "我的狗一直在叫哎~，我现在有点忙哦~，你能帮给我安抚它一下嘛~~"},
                new DialogInfo() { name = "Luna",content = "啊？"},
                new DialogInfo() { name = "Nala",content = "O(∩_∩)O，摸摸它就可以啦！"},
                new DialogInfo() { name = "Nala",content = "(●'◡'●)(●'◡'●)，别看它这么叫的凶，其实就是想引起你的注意呢~"},
                new DialogInfo() { name = "Luna",content = "可是。。。。"},
                new DialogInfo() { name = "Luna",content = "我可是猫女郎啊！！！"},
                new DialogInfo() { name = "Nala",content = "安心啦！不会咬你哒！快去吧~~"},
            },
            //2 dialogInfoIndex
            new DialogInfo[] {
                new DialogInfo() { name = "Nala",content = "它还在叫呢！"}
            },
            //3 dialogInfoIndex
            new DialogInfo[] {
                new DialogInfo() { name = "Nala",content = "感谢你呐，Luna！你还是这么的可靠呢~"},
                new DialogInfo() { name = "Nala",content = "我想继续请你帮个忙好吗？"},
                new DialogInfo() { name = "Nala",content = "说起来，这事情还怪我。。"},
                new DialogInfo() { name = "Nala",content = "然后装蜡烛的袋子没有封好！（；´д｀）ゞ"},
                new DialogInfo() { name = "Nala",content = "今天睡过头了，出门比较匆忙。"},
                new DialogInfo() { name = "Nala",content = "结果就。。。蜡烛基本丢完了。。"},
                new DialogInfo() { name = "Luna",content = "你还是老样子，哈哈。。"},
                new DialogInfo() { name = "Nala",content = "所以，所以喽。你帮帮忙啦，帮我把蜡烛找回来吧，Luna~~"},
                new DialogInfo() { name = "Nala",content = "如果你能帮我把5个蜡烛全部找回来，我就给你一个神器作为回报！"},
                new DialogInfo() { name = "Nala",content = "神器？？？+.φ(゜▽゜*)♪"},
                new DialogInfo() { name = "Nala",content = "是的，我感觉很适合你，加油哟~"},
            },
            //4 dialogInfoIndex
            new DialogInfo[] {
                new DialogInfo() { name = "Nala",content = "你还没有帮我收集到所有蜡烛哦，Luna~"}
            },
            //5 dialogInfoIndex
            new DialogInfo[] {
                new DialogInfo() { name = "Nala",content = "可靠啊，竟然全都收集了！"},
                new DialogInfo() { name = "Luna",content = "你知道多累吗？"},
                new DialogInfo() { name = "Luna",content = "你到处跑，真的很难收集！"},
                new DialogInfo() { name = "Nala",content = "辛苦啦辛苦啦~"},
                new DialogInfo() { name = "Nala",content = "这是给你的奖励"},
                new DialogInfo() { name = "Nala",content = "蓝纹火锤，传说中的神器~"},
                new DialogInfo() { name = "Nala",content = "应该挺适合你的，Luna"},
                new DialogInfo() { name = "Luna",content = "~~获得蓝纹火锤~~（遇到怪物可触发战斗）"},
                new DialogInfo() { name = "Luna",content = "哇，谢谢你，Nala~"},
                new DialogInfo() { name = "Nala",content = "(●ˇ∀ˇ●)，不用客气啦~"},
                new DialogInfo() { name = "Nala",content = "正好，山里最近出现了一批怪物，你也算为民除害了！"},
                new DialogInfo() { name = "Luna",content = "啊？"},
                new DialogInfo() { name = "Luna",content = "这才是你真实的目的吧？！"},
                new DialogInfo() { name = "Nala",content = "拜托拜托啦，Luna~，否则真的很不方便我卖东西~"},
                new DialogInfo() { name = "Luna",content = "无语中。。。"},
                new DialogInfo() { name = "Nala",content = "求求你啦~~。啵啵~~"},
                new DialogInfo() { name = "Luna",content = "哎，行吧。。"},
                new DialogInfo() { name = "Nala",content = "嘻嘻，那辛苦Luna了~"},
            },
            //6 dialogInfoIndex
            new DialogInfo[] {
                new DialogInfo() { name = "Nala",content = "Luna,怪物还没有清理完哦~~"}
            },
            //7 dialogInfoIndex
            new DialogInfo[] {
                new DialogInfo() { name = "Nala",content = "真棒，Luna~，周围的居民都会很感谢你的！有机会来我家喝一杯~"},
                new DialogInfo() { name = "Luna",content = "我觉得可行，哈哈~"},
            },
            //8 dialogInfoIndex
            new DialogInfo[] {
                new DialogInfo() { name = "Nala",content = "改天再见喽~~"}
            }
        };
        GameManager.Instance.dialogInfoIndex = 0;
        contentIndex = 1;
    }
    //显示对话内容
    public void DisplayDialog() {
        //对话结束
        if(GameManager.Instance.dialogInfoIndex > 7) {
            return;
        }

        if(contentIndex >= dialogInfoList[GameManager.Instance.dialogInfoIndex].Length) {
            if(GameManager.Instance.dialogInfoIndex == 2 &&
               !GameManager.Instance.hasPetTheDog) {

            }
            else if(GameManager.Instance.dialogInfoIndex == 4 &&
                    GameManager.Instance.candleNum < 5) {

            }
            else if(GameManager.Instance.dialogInfoIndex == 6 &&
                    GameManager.Instance.killNum < 5) {

            }
            else {
                GameManager.Instance.dialogInfoIndex++;
            }
            if(GameManager.Instance.dialogInfoIndex == 6) {
               GameManager.Instance.ShowMonsters();
            }
            contentIndex = 0;
            UIManager.Instance.ShowDialog();
            GameManager.Instance.canControlLuna = true;
        }
        else {
            DialogInfo dialogInfo = dialogInfoList[GameManager.Instance.dialogInfoIndex][contentIndex];
            UIManager.Instance.ShowDialog(dialogInfo.content,dialogInfo.name);
            contentIndex++;
            animator.SetTrigger("Talk");
        }
    }
    public void SetContentIndex() {
        contentIndex = dialogInfoList[GameManager.Instance.dialogInfoIndex].Length;
    }
}
public struct DialogInfo {
    public string name;
    public string content;
}
