using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using System.Xml.Linq;

// README.md를 읽고 코드를 작성하세요.

string s1 = "hello";
string s2 = "hello";
string s3 = new string("hello".ToCharArray());

Console.WriteLine(s1 == s2);
Console.WriteLine(s1 == s3);
Console.WriteLine(object.ReferenceEquals(s1, s2));
Console.WriteLine(object.ReferenceEquals(s1, s3));


Player p1 = new Player()
{
    Name = "name",
    Level = 1,
};

Player p2 = new Player()
{
    Name = "name",
    Level = 1,
}
;

Player p3 = p1;

Console.WriteLine($"p1 == p2: {p1 == p2}");
Console.WriteLine($"p1 == p3: {p1 == p3}");
Console.WriteLine($"p1.Equals(p2): {p1.Equals(p2)}");
Console.WriteLine($"p1.Equals(p3): {p1.Equals(p3)}");


Position pos1 = new Position(10, 20);
Position pos2 = new Position(10, 20);
Position pos3 = new Position(30, 40);

Console.WriteLine($"pos1.Equals(pos2): {pos1.Equals(pos2)}");
Console.WriteLine($"pos1.Equals(pos3): {pos1.Equals(pos3)}");

Item item1 = new Item("a", 1);
Item item2 = new Item("a", 1);
Item item3 = new Item("b", 2);

Console.WriteLine($"item1.Equals(item2): {item1.Equals(item2)}");
Console.WriteLine($"item1.Equals(item3): {item1.Equals(item3)}");

HashSet<Item> inventory = new HashSet<Item>();
inventory.Add(item1);
inventory.Add(item2);
inventory.Add(item3);
Console.WriteLine($"inventory.Contains(item2): {inventory.Contains(item2)}");


BadItem b1 = new BadItem("a");
BadItem b2 = new BadItem("a");

Dictionary<BadItem, int> stock = new Dictionary<BadItem, int>();

stock[b1] = 10;


Console.WriteLine($"b1.Equals(b2): {b1.Equals(b2)}");
Console.WriteLine($"stock.ContainsKey(b2): {stock.ContainsKey(b2)}"); // b1, b2 는 해쉬코드가 같아서 원랜 10이 나와야함


var monsters = new List<Monster>() 
{ 
    new Monster("Goblin", 30),
    new Monster("Dragon", 100),
    new Monster("Slime", 10),
    new Monster("Orc", 50),
};

Console.WriteLine("정렬 전:");
foreach (Monster monster in monsters)
{
    Console.WriteLine(monster);
}

monsters.Sort();

Console.WriteLine();
Console.WriteLine("정렬 후:");
foreach (Monster monster in monsters)
{
    Console.WriteLine(monster);
}

var students = new List<Student>()
{
    new Student("김철수", 2, 85),
    new Student("이영희", 1, 92),
    new Student("박민수", 2, 85),
    new Student("정수진", 1, 88),
    new Student("최동훈", 2, 90)
};

students.Sort();
Console.WriteLine("정렬 결과:");
foreach (Student student in students)
{
    Console.WriteLine(student);
}



Customer c1 = new Customer("abc", "xyz");
Customer c2 = new Customer("abc", "xyz");

CustomerNameComparer c3 = new CustomerNameComparer();

var d1 = new Dictionary<Customer, int>();
d1[c1] = 1;

var d2 = new Dictionary<Customer, int>(new CustomerNameComparer());
d2[c1] = 2;

Console.WriteLine($"기본 Dictionary - c2 검색: {d1.ContainsKey(c2)}");
Console.WriteLine($"커스텀 Dictionary - c2 검색: {d2.ContainsKey(c2)}");



var d3 = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

d3["Apple"] = 100;
d3["BANANA"] = 200;

Console.WriteLine($"apple 검색: {d3["apple"]}");
Console.WriteLine($"apple 검색: {d3["banana"]}");

var d4 = new Dictionary<string, int>();
d4["Apple"] = 100;
Console.WriteLine($"일반 Dictionary에서 'apple' 존재 여부: {d4.ContainsKey("apple")}");




class QuestRewardComparer : Comparer<Quest>
{
    public override int Compare(Quest x, Quest y)
    {
        throw new NotImplementedException();
    }


    public int GetHashCode([DisallowNull] Quest obj)
    {
        throw new NotImplementedException();
    }
}

class QuestPriorityComparer : Comparer<Quest>
{
    public override int Compare(Quest x, Quest y)
    {
        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] Quest obj)
    {
        throw new NotImplementedException();
    }
}

class Quest
{
    public int Priority;
    public int Reward;
    public string Name;

    public Quest(int priority, int reward, string name)
    {
        Priority = priority;
        Reward = reward;
        Name = name;
    }

    public override string ToString()
    {

        return $"[우선순위{Priority}] {Name} (보상: {Reward}골드)";
    }
}

class CustomerNameComparer : EqualityComparer<Customer>
{
    public override bool Equals(Customer x, Customer y)
    {
        return x.FirstName == y.FirstName && x.LastName == y.LastName;
    }

    public override int GetHashCode(Customer obj)
    {
        return HashCode.Combine(obj.FirstName, obj.LastName);
    }
}

class Customer
{
    public string FirstName;
    public string LastName;

    public Customer(string s1, string s2) 
    {
        FirstName = s1;
        LastName = s2;
    }
}

class Student : IComparable<Student>
{
    public string Name;
    public int Grade;
    public int Score;

    public Student(string name, int grade, int score)
    {
        Name = name;
        Grade = grade;
        Score = score;
    }

    public int CompareTo(Student other)
    {
        if (other.Grade != Grade)
        {
            return Grade.CompareTo(other.Grade);
        }
        else if(other.Score != Score)
        {
            return other.Score.CompareTo(Score);
        }
        else
        {
            return Name.CompareTo(other.Name);
        }
        
    }

    public override string ToString()
    {
        return $"{Grade}학년 {Name} ({Score}점)";
    }
}

class Monster : IComparable<Monster>
{
    public string Name;
    public int Power;

    public Monster(string name, int power)
    {
        Name = name;
        Power = power;
    }

    public int CompareTo(Monster other)
    {
        return Power.CompareTo(other.Power);
    }

    public override string ToString()
    {
        return $"{Name}(전투력:{Power})";
    }
}



class BadItem
{
    public string Name;

    public BadItem(string name)
    {
        Name = name;
    }


    public override bool Equals(object obj)
    {
        BadItem other = obj as BadItem;
        if(other == null) return false;


        return other.Name == Name;
    }
}

interface IEquatable<Item>
{
    bool Equals(Item other);
}

class Item : IEquatable<Item>
{
    public string Name { get; set; }
    public int Id { get; set; }

    public Item(string name, int id)
    {
        Name = name;
        Id = id;
    }

    public bool Equals(Item other)
    {
        if (other == null) return false;

        return other.Name == Name && other.Id == Id;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Item);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Id);
    }
}
struct Position
{
    public int X;
    public int Y;

    public Position(int x,int y)
    {
        X = x; Y = y;
    }
}

class Player
{
    public string Name { get; set; }    
    public int Level {  get; set; }


}