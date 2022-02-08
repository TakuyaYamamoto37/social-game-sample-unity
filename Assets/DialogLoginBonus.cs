using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLoginBonus : MonoBehaviour
{
    [SerializeField]
    Image itemType;

    [SerializeField]
    Text amountLabel;

    // Start is called before the first frame update
    void Start()
    {
        //ログインデータ取得
        UserLoginModel userLoginModel = UserLogin.Get();
        if (string.IsNullOrEmpty(userLoginModel.user_id))
        {
            UnityEngine.Debug.LogError("ユーザーのログインデータがありません。");
            return;
        }

        //ログイン商品データ取得
        MasterLoginItemModel masterLoginItemModel = MasterLoginItem.GetMasterLoginItem(userLoginModel.login_day);
        if (masterLoginItemModel.login_day == 0)
        {
            Debug.LogError("ログインアイテムのマスターデータがありません。");
            return;
        }

        //商品Imageの設定
        Sprite sprite = null;
        if (masterLoginItemModel.item_type == (int)MasterLoginItem.ItemType.Crystal || masterLoginItemModel.item_type == (int)MasterLoginItem.ItemType.CrystalFree)
        {
            sprite = Resources.Load<Sprite>("Crystal");
        }
        else if (masterLoginItemModel.item_type == (int)MasterLoginItem.ItemType.FriendCoin)
        {
            sprite = Resources.Load<Sprite>("FriendCoin");
        }
        if (sprite == null)
        {
            Debug.LogError("通貨の画像がありません。");
            return;
        }
        itemType.sprite = sprite;

        amountLabel.text = "x " + masterLoginItemModel.item_count;
    }
}
