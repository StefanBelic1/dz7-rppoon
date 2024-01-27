using System;

//  LANAC ODGOVORNOSTI

public abstract class SkillHandler
{
    protected SkillHandler _nextHandler;

    public void SetNextHandler(SkillHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public abstract void ExecuteSkill(string enemyType);
}


public class FireSkillHandler : SkillHandler
{
    public override void ExecuteSkill(string enemyType)
    {
        if (enemyType == "IceEnemy")
        {
            Console.WriteLine("FireSkillHandler: Enemy is weak to fire!");
        }
        else if (_nextHandler != null)
        {
            _nextHandler.ExecuteSkill(enemyType);
        }
    }
}

public class IceSkillHandler : SkillHandler
{
    public override void ExecuteSkill(string enemyType)
    {
        if (enemyType == "FireEnemy")
        {
            Console.WriteLine("IceSkillHandler: Enemy is weak to ice!");
        }
        else if (_nextHandler != null)
        {
            _nextHandler.ExecuteSkill(enemyType);
        }
    }
}

public class DefaultSkillHandler : SkillHandler
{
    public override void ExecuteSkill(string enemyType)
    {
        Console.WriteLine("DefaultSkillHandler: No specific weakness found.");
    }
}

public class Player
{
    private SkillHandler _skillHandler;

    public Player()
    {
        
        _skillHandler = new FireSkillHandler();
        SkillHandler iceHandler = new IceSkillHandler();
        SkillHandler defaultHandler = new DefaultSkillHandler();

        _skillHandler.SetNextHandler(iceHandler);
        iceHandler.SetNextHandler(defaultHandler);
    }

    public void Attack(string enemyType)
    {
        _skillHandler.ExecuteSkill(enemyType);
    }
}

public class GameClientChain
{
    public static void Run()
    {
        Player player = new Player();

   
        player.Attack("FireEnemy");
        player.Attack("IceEnemy");
        player.Attack("RockEnemy");
    }
}




// ITERATOR


public interface IIterator
{
    bool HasNext();
    object Next();
}


public class Enemy
{
    public string Name { get; set; }

    public Enemy(string name)
    {
        Name = name;
    }
}

public interface IIterableCollection
{
    IIterator CreateIterator();
}


public class EnemyCollection : IIterableCollection
{
    private List<Enemy> _enemies;

    public EnemyCollection()
    {
        _enemies = new List<Enemy>
        {
            new Enemy("Goblin"),
            new Enemy("Skeleton"),
            new Enemy("Dragon")
        };
    }

    public IIterator CreateIterator()
    {
        return new EnemyIterator(_enemies);
    }
}


public class EnemyIterator : IIterator
{
    private List<Enemy> _enemies;
    private int _position = 0;

    public EnemyIterator(List<Enemy> enemies)
    {
        _enemies = enemies;
    }

    public bool HasNext()
    {
        return _position < _enemies.Count;
    }

    public object Next()
    {
        if (HasNext())
        {
            return _enemies[_position++];
        }
        else
        {
            return null;
        }
    }
}

public class GameClientIterator
{
    public static void Run()
    {
        EnemyCollection enemyCollection = new EnemyCollection();
        IIterator iterator = enemyCollection.CreateIterator();

        while (iterator.HasNext())
        {
            Enemy enemy = (Enemy)iterator.Next();
            Console.WriteLine($"Encountered enemy: {enemy.Name}");
        }
    }
}