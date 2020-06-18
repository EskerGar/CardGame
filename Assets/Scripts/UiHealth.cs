using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiHealth : MonoBehaviour
{
   [SerializeField] private GameObject heartPrefab;
   
   private readonly Stack<GameObject> _heartsStack = new Stack<GameObject>();
   private Health _health;

   private void Start()
   {
       _health = GameManager.Instance.GetHealthClass;
       _health.OnHealthDecrease += DecreaseHealth;
      GenerateHearts();
   }

   private void GenerateHearts()
   {
       var heartCount = _health.CurrentHealth;
       Vector3 offset = new Vector2(.55f, 0);
       var heart = CreateHeart(transform.position);
       for (int i = 0; i < heartCount - 1; i++)
           heart = CreateHeart( heart.transform.position + offset);
   }

   private GameObject CreateHeart(Vector2 pos)
   {
     var heart = Instantiate(heartPrefab, gameObject.transform);
     heart.transform.position = pos;
     _heartsStack.Push(heart);
     return heart;
   }

   private void DecreaseHealth() => _heartsStack.Pop().GetComponent<UiHeart>().DeleteHeart();
}
