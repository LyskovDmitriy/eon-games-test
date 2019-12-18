using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Level level = default;

    private void Start()
    {
        level.Play();
    }
}
