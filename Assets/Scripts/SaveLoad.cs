using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[Serializable]
public class SaveLoad
{
    List<HumanActionStructure> humanActions;
    List<List<CatReactionStructure>> catReactions;    

    public void Save(List<HumanActionStructure> currentHumanActions, List<List<CatReactionStructure>> currentCatReactions)
    {
        humanActions = currentHumanActions;
        catReactions = currentCatReactions;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Directory.GetCurrentDirectory() + "/HumanCatInteractions.sav");
        bf.Serialize(file, this);
        file.Close();
    }
    public void Load(out List<HumanActionStructure> newHumanActions, out List<List<CatReactionStructure>> newCatReactions)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Directory.GetCurrentDirectory() + "/HumanCatInteractions.sav", FileMode.Open);
            SaveLoad load = (SaveLoad)bf.Deserialize(file);
            newHumanActions = humanActions = load.humanActions;
            newCatReactions = catReactions = load.catReactions;
            file.Close();
        }
        catch 
        {
            newHumanActions = new List<HumanActionStructure>();
            newCatReactions = new List<List<CatReactionStructure>>();
            for (int i = 0; i < 3; i++)
                newCatReactions.Add(new List<CatReactionStructure>());
        }
    }
}
