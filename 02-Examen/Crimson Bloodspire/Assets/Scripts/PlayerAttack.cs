using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.1f; // Más largo para la espada
    public Vector2 attackArea = new Vector2(0.1f, 0.1f); // Área rectangular para el swing
    public LayerMask enemyLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Usar un área rectangular para la espada
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackArea, 0f, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyBehavior>().TakeDamage();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireCube(attackPoint.position, attackArea);
    }
}