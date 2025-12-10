using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TeamManagerApp.Models
{
    public class BasketballPlayer
    {    

        //auto increments ID for every new player
        private static int _nextId = 1; 
        public int Id {get; private set;}
        public string FirstName {get; private set;}
        public string LastName {get; private set;}
        public string Team {get; private set;}
        public string Position {get; private set;}
        public string FullName => $"{FirstName} {LastName}";


        public BasketballPlayer(string firstName, string lastName, string team, string position)
        {
            Id = _nextId++; //auto increments ID for every new player

            FirstName = firstName;
            LastName = lastName;
            Team = team;
            Position = position;
        }

        public override string ToString()
        {
            return $"[{Id}] {FirstName} {LastName} - {Position} ({Team})";
        }

    }
}