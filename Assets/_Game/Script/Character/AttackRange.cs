using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private Character parent;  
    public bool isInRange;
    public List<Character> characterList;


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

        Obstacle obstacle = Cache.GetObstacle(other);
        if (obstacle != null && parent is Player)
        {
            obstacle.Blur();
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

        Obstacle obstacle = Cache.GetObstacle(other);
        if (obstacle != null)
        {
            obstacle.DefaultColor();
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
