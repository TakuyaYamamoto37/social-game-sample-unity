using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaResult : MonoBehaviour
{
    [SerializeField]
    Text characterNameLabel;

    [SerializeField]
    Image typeAsset;

    [SerializeField]
    GameObject rarity1;

    [SerializeField]
    GameObject rarity2;

    [SerializeField]
    GameObject rarity3;

    [SerializeField]
    Image characterAsset;

    //public UserCharacterModel userCharacterModel;

    // Start is called before the first frame update
    void Start()
    {
        UserCharacterModel userCharacterModel = UserCharacter.GetLatestUserCharacter();
        if (userCharacterModel == null)
        {
            Debug.LogError("キャラクターが存在しません");
            return;
        }

        MasterCharacterModel masterCharacterModel = MasterCharacter.GetMasterCharacter(userCharacterModel.character_id);
        if (masterCharacterModel.character_id == 0)
        {
            Debug.LogError("master_characterのデータが取得できません。マスタデータを確認してください");
            return;
        }

        characterNameLabel.text = masterCharacterModel.character_name;

        //レアリティの制御
        rarity1.SetActive(true);
        rarity2.SetActive(1 < masterCharacterModel.rarity);
        rarity3.SetActive(2 < masterCharacterModel.rarity);

        //Resourcesから画像を読み込む場合
        Sprite sprite = Resources.Load<Sprite>("big_" + masterCharacterModel.asset_id);
        if (sprite == null)
        {
            Debug.LogError("キャラクターの画像がありません");
            return;
        }
        characterAsset.sprite = sprite;

        Sprite typeSprite = Resources.Load<Sprite>("TypeIcon_" + masterCharacterModel.type);
        if (typeSprite == null)
        {
            Debug.LogError("タイプの画像がありません");
            return;
        }
        typeAsset.sprite = typeSprite;
    }
}
