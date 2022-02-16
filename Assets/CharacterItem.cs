using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour
{
    public UserCharacterModel userCharacterModel;

    [SerializeField]
    Image characterAsset;

    [SerializeField]
    Image typeAsset;

    // Start is called before the first frame update
    void Start()
    {
        MasterCharacterModel masterCharacterModel = MasterCharacter.GetMasterCharacter(userCharacterModel.character_id);

        //Resourcesから画像を読み込む場合
        Sprite characterSprite = Resources.Load<Sprite>(masterCharacterModel.asset_id);
        characterAsset.sprite = characterSprite;
        Sprite typeSprite = Resources.Load<Sprite>("CharacterTypeFrame_" + masterCharacterModel.type);
        typeAsset.sprite = typeSprite;
    }
}
