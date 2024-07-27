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
            character.OnCharacterDeath += HandleCharacterDeath;
            characterList.Add(character);
            isInRange = true;
        }
        
        Obstacle obstacle = Cache.GetObstacle(other);
        if (obstacle != null && parent is Player)
        {
            obstacle.Blur();
        }

        BotCtrl botCtrl = Cache.GetBotCtrl(other);
        if (botCtrl != null)
        {
            botCtrl.targetScr.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character character = Cache.GetCharacter(other);
        if (character != null && character != parent && characterList.Contains(character))
        {
            character.OnCharacterDeath -= HandleCharacterDeath;
            characterList.Remove(character);
            isInRange = characterList.Count > 0;
        }

        Obstacle obstacle = Cache.GetObstacle(other);
        if (obstacle != null)
        {
            obstacle.DefaultColor();
        }

        BotCtrl botCtrl = Cache.GetBotCtrl(other);
        if (botCtrl != null)
        {
            botCtrl.targetScr.enabled = true;
        }
    }

    private void HandleCharacterDeath(Character character)
    {
        if (characterList.Contains(character))
        {
            characterList.Remove(character);
            isInRange = characterList.Count > 0;
        }
    }

    // Method to clean up inactive or despawned characters
    private void CleanUpCharacterList()
    {
        characterList.RemoveAll(character => character == null || !character.gameObject.activeSelf);
        isInRange = characterList.Count > 0;
    }

    private void Update()
    {
        CleanUpCharacterList();
    }
}
