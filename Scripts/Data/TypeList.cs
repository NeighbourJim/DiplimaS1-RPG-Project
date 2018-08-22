using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeList {    

    public MonType[] typeList = new MonType[]
    {
        new MonType{ typeID = (int)TypeNum.none, typeName = "?TYPE?" },
        new MonType
        {
            typeID = (int)TypeNum.normal,
            typeName = "Normal",
            weaknesses = new TypeNum[]{ TypeNum.fighting },
            resistances = new TypeNum[]{ },
            immunities = new TypeNum[] { TypeNum.ghost }
        },
        new MonType
        {
            typeID = (int)TypeNum.grass,
            typeName = "Grass",
            weaknesses = new TypeNum[]{ TypeNum.fire, TypeNum.flying, TypeNum.ice, TypeNum.bug, TypeNum.poison },
            resistances = new TypeNum[]{ TypeNum.grass, TypeNum.water, TypeNum.electric, TypeNum.ground },
            immunities = new TypeNum[] { }
        },
        new MonType
        {
            typeID = (int)TypeNum.water,
            typeName = "Water",
            weaknesses = new TypeNum[]{ TypeNum.grass, TypeNum.electric },
            resistances = new TypeNum[]{ TypeNum.fire, TypeNum.water, TypeNum.ice },
            immunities = new TypeNum[] { }
        },
        new MonType
        {
            typeID = (int)TypeNum.fire,
            typeName = "Fire",
            weaknesses = new TypeNum[]{ TypeNum.water, TypeNum.ground, TypeNum.rock },
            resistances = new TypeNum[]{ TypeNum.fire, TypeNum.grass, TypeNum.ice, TypeNum.bug },
            immunities = new TypeNum[] { }
        },
        new MonType
        {
            typeID = (int)TypeNum.electric,
            typeName = "Electric",
            weaknesses = new TypeNum[]{ TypeNum.ground },
            resistances = new TypeNum[]{ TypeNum.electric, TypeNum.flying },
            immunities = new TypeNum[] { }
        },
        new MonType
        {
            typeID = (int)TypeNum.psychic,
            typeName = "Psychic",
            weaknesses = new TypeNum[]{ TypeNum.bug, TypeNum.ghost },
            resistances = new TypeNum[]{ TypeNum.psychic, TypeNum.fighting},
            immunities = new TypeNum[] { }
        },
        new MonType
        {
            typeID = (int)TypeNum.ice,
            typeName = "Ice",
            weaknesses = new TypeNum[]{ TypeNum.fire, TypeNum.fighting, TypeNum.rock },
            resistances = new TypeNum[]{ TypeNum.ice },
            immunities = new TypeNum[] { }
        },
        new MonType
        {
            typeID = (int)TypeNum.dragon,
            typeName = "Dragon",
            weaknesses = new TypeNum[]{ TypeNum.ice, TypeNum.dragon },
            resistances = new TypeNum[]{ TypeNum.fire, TypeNum.water, TypeNum.grass, TypeNum.electric },
            immunities = new TypeNum[] { }
        },
        new MonType
        {
            typeID = (int)TypeNum.fighting,
            typeName = "Fighting",
            weaknesses = new TypeNum[]{ TypeNum.flying, TypeNum.psychic },
            resistances = new TypeNum[]{ TypeNum.bug, TypeNum.rock },
            immunities = new TypeNum[] { }
        },
        new MonType
        {
            typeID = (int)TypeNum.flying,
            typeName = "Flying",
            weaknesses = new TypeNum[]{ TypeNum.electric, TypeNum.rock, TypeNum.ice },
            resistances = new TypeNum[]{ TypeNum.grass, TypeNum.fighting, TypeNum.bug },
            immunities = new TypeNum[] { TypeNum.ground }
        },
        new MonType
        {
            typeID = (int)TypeNum.poison,
            typeName = "Poison",
            weaknesses = new TypeNum[]{ TypeNum.ground, TypeNum.psychic },
            resistances = new TypeNum[]{ TypeNum.grass, TypeNum.fighting, TypeNum.poison, TypeNum.bug },
            immunities = new TypeNum[] { }
        },
        new MonType
        {
            typeID = (int)TypeNum.ground,
            typeName = "Ground",
            weaknesses = new TypeNum[]{ TypeNum.water, TypeNum.grass, TypeNum.ice },
            resistances = new TypeNum[]{ TypeNum.poison, TypeNum.rock },
            immunities = new TypeNum[] { TypeNum.electric }
        },
        new MonType
        {
            typeID = (int)TypeNum.rock,
            typeName = "Rock",
            weaknesses = new TypeNum[]{ TypeNum.water, TypeNum.grass, TypeNum.fighting, TypeNum.ground },
            resistances = new TypeNum[]{ TypeNum.normal, TypeNum.fire, TypeNum.poison, TypeNum.flying },
            immunities = new TypeNum[] { }
        },
        new MonType
        {
            typeID = (int)TypeNum.bug,
            typeName = "Bug",
            weaknesses = new TypeNum[]{ TypeNum.fire, TypeNum.flying, TypeNum.rock },
            resistances = new TypeNum[]{ TypeNum.grass, TypeNum.ground, TypeNum.fighting },
            immunities = new TypeNum[] { }
        },
        new MonType
        {
            typeID = (int)TypeNum.ghost,
            typeName = "Ghost",
            weaknesses = new TypeNum[]{ TypeNum.ghost },
            resistances = new TypeNum[]{ TypeNum.poison, TypeNum.bug },
            immunities = new TypeNum[] { TypeNum.normal, TypeNum.fighting }
        }
    };
}
