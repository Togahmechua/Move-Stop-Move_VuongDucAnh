using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private Character parent;
    public List<Character> characterList;
    public bool isInRange;


    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetCharacter(other);
        if (character != null && character != parent && !characterList.Contains(character))
        {
            // Debug.Log(other.transform.name,gameObject);
            character.OnCharacterDeath += HandleCharacterDeath;
            characterList.Add(character);
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character character = Cache.GetCharacter(other);
        if (character != null && character != parent && characterList.Contains(character))
        {
            // Debug.Log(other.transform.name,gameObject);
            character.OnCharacterDeath -= HandleCharacterDeath;
            characterList.Remove(character);
            isInRange = false;
        }
    }

    private void HandleCharacterDeath(Character character)
    {
        if (characterList.Contains(character))
        {
            characterList.Remove(character);
            isInRange = false;
        }
    }
}
