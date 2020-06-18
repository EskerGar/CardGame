using UnityEngine;

public class CreateCard : MonoBehaviour
{
    public static GameObject Create(GameObject prefab, ConfigSo configs, int i, int j, Sprite sprite)
    {
        var startPos = configs.GetStartCord;
        var offset = configs.GetOffsetCord;
        var card = Instantiate(
            prefab, 
            startPos + offset * new Vector2(j, i), 
            Quaternion.identity).
            GetComponent<Card>();
        card.Initialize();
        card.ChangeSprite(sprite);
        return card.gameObject;
    }
}
