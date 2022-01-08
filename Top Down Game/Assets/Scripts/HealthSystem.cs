public class HealthSystem
{
    int health;
    int maxHealth;
    public HealthSystem(int maxHealth){
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public int GetHealth(){
        return health;
    }

    public void Damage(int damage){
        health -= damage;
    }

    public void Heal(int heal){
        health += heal;
    }
}
