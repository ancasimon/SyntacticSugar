using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace SyntacticSugar
{
    //copied code from exercise to refactor!!!!
    public class Bug
    {
        /*
            You can declare a typed public property, make it read-only,
            and initialize it with a default value all on the same
            line of code in C#. Readonly properties can be set in the
            class' constructors, but not by external code.
        */
        //ANca note: the values below are called property initializers!!
        public string Name { get; set; } = "";
        public string Species { get; } = "";
        public List<string> Predators { get; } = new List<string>();
        public List<string> Prey { get; } = new List<string>();

        // Convert this readonly property to an expression member:
        //ANCA: initial format below:
        //public string FormalName
        //{
        //    get
        //    {
        //        return $"{this.Name} the {this.Species}";
        //    }
        //}

        //ANCA: new expression-bodied function member format added here:
        public string FormalName => $"{this.Name} the {this.Species}";

        // Class constructor
        public Bug(string name, string species, List<string> predators, List<string> prey)
        {
            this.Name = name;
            this.Species = species;
            this.Predators = predators;
            this.Prey = prey;
        }



        // Convert this method to an expression member:
        //ANCA: initial format below:
        //public string PreyList()
        //{
        //    var commaDelimitedPrey = string.Join(",", this.Prey);
        //    return commaDelimitedPrey;
        //}

        //ANCA: conversion below:
        //public string PreyList() => $"{Prey}";
        public string PreyList() => string.Join(",", this.Prey);  //ANCA: You put the string.Join at the beginning!!!!



        // Convert this method to an expression member:
        //public string PredatorList()
        //{
        //    var commaDelimitedPredators = string.Join(",", this.Predators);
        //    return commaDelimitedPredators;
        //}
        //ANCA: conversion below:
        //public string PredatorList() => $"{Predators}.Join(',')"; //Anca - tried this too but the right way is below:
        public string PredatorList() => string.Join(",", this.Predators);

        // Convert this to expression method:
        //public string Eat(string food)
        //{
        //    if (this.Prey.Contains(food))
        //    {
        //        return $"{this.Name} ate the {food}.";
        //    }
        //    else
        //    {
        //        return $"{this.Name} is still hungry.";
        //    }
        //}
        //ANCA: conversion below:
        public string Eat(string food) => Prey.Contains(food) ? $"{Name} ate the {food}." : $"{Name} is still hungry."; //ANCA notes: method name equals the ternary expression.

    } 

    class Program
    {
        static void Main(string[] args)
        {
            var predators = new List<string>();
            predators.Add("anteater");
            predators.Add("termites");
            var prey = new List<string>();
            prey.Add("carrion");
            prey.Add("sugar");

            var bugAnt = new Bug("ant", "ant species", predators, prey);
            Console.WriteLine(bugAnt.FormalName);
            bugAnt.Name = "steve";
            Console.WriteLine(bugAnt.FormalName);

            Console.WriteLine($"{bugAnt.FormalName} likes to prey on {bugAnt.PreyList()}.");
            Console.WriteLine($"{bugAnt.FormalName} hates {bugAnt.PredatorList()}.");

            //Anca note: I put in the list parameters when instantiating the object > but it would be better to do it in separate variables - like in the example for the bugAnt above!!
            var Bug1 = new Bug("Praying Mantis", "Praying Mantid", new List<string> { "frogs", "bats", "spiders", "snakes" }, new List<string> { "moths", "mosquitoes", "roaches", "flies" });
            //var Bug2 = new Bug("Ladybugs", "Ladybird Beetles", new List<string>....)
            Console.WriteLine(Bug1.Eat("moths"));

            Bug1.Eat("moths");
            Bug1.PreyList();


        }
    }
}
