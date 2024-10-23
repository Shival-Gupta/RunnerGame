using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void TakeDamage()
    {
        GameManager.instance.ReduceLife();
    }
}
