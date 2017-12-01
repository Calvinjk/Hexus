using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace placeholder.Hexus {
    public class BasicSoldier : Unit {

        public enum UnitType { Hoplite, Toxotes }; // Add to this enum when creating a new unit type

        //Default Constructor will create a Hoplite
        public BasicSoldier() {
            name = "Hoplite";
            size = 1;
            maxHealth = 10;
            curHealth = 10;
            power = 10;
            speed = 2;
            range = 1;
            defense = 8;
            myStatus = GameStatus.Alive;
            // TODO - availableOrders
            // TODO - Player reference
        }

        // Copy Constructor based on another unit
        public BasicSoldier(BasicSoldier other) {
            name = other.name;
            size = other.size;
            maxHealth = other.maxHealth;
            curHealth = other.curHealth;
            power = other.power;
            speed = other.speed;
            range = other.range;
            defense = other.defense;
            myStatus = other.myStatus;
            // TODO - availableOrders
            // TODO - Player Reference
        }

        // Custom Constructor based on a unit type
        public BasicSoldier(UnitType unit) {
            switch (unit) {
                case UnitType.Hoplite:
                    name = "Hoplite";
                    size = 1;
                    maxHealth = 10;
                    curHealth = 10;
                    power = 10;
                    speed = 2;
                    range = 1;
                    defense = 8;
                    myStatus = GameStatus.Alive;
                    // TODO - availableOrders
                    // TODO - Player reference
                    break;
                case UnitType.Toxotes:
                    name = "Toxotes";
                    size = 1;
                    maxHealth = 8;
                    curHealth = 8;
                    power = 10;
                    speed = 2;
                    range = 3;
                    defense = 6;
                    myStatus = GameStatus.Alive;
                    // TODO - availableOrders
                    // TODO - Player reference
                    break;
                default:
                    Debug.Log("Tried to create invalid unit type");
                    break;
            }
        }


    }
}